using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using WaterTrans.Boilerplate.CrossCuttingConcerns.Abstractions.OS;
using WaterTrans.Boilerplate.CrossCuttingConcerns.ExtensionMethods;
using WaterTrans.Boilerplate.CrossCuttingConcerns.Utils;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.Abstractions.Repositories;
using WaterTrans.Boilerplate.Domain.Abstractions.Services;
using WaterTrans.Boilerplate.Domain.Constants;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Domain.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IAppSettings _appSettings;
        private readonly IAccessTokenRepository _accessTokenRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IAuthorizationCodeRepository _authorizationCodeRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IAccountService _accountService;
        private readonly IDateTimeProvider _dateTimeProvider;

        public AuthorizeService(
            IAppSettings appSettings,
            IAccessTokenRepository accessTokenRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IAuthorizationCodeRepository authorizationCodeRepository,
            IApplicationRepository applicationRepository,
            IAccountService accountService,
            IDateTimeProvider dateTimeProvider)
        {
            _appSettings = appSettings;
            _accessTokenRepository = accessTokenRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _authorizationCodeRepository = authorizationCodeRepository;
            _applicationRepository = applicationRepository;
            _accountService = accountService;
            _dateTimeProvider = dateTimeProvider;
        }

        public (AccessToken accessToken, RefreshToken refreshToken) CreateAccessToken(AuthorizationCode authorizationCode, IEnumerable<string> scopes)
        {
            if (authorizationCode == null)
            {
                throw new ArgumentNullException(nameof(authorizationCode));
            }

            if (!authorizationCode.IsEnabled(_dateTimeProvider.Now))
            {
                throw new InvalidOperationException($"AuthorizationCode '{authorizationCode.Code}' was not valid.");
            }

            var application = _applicationRepository.GetById(authorizationCode.ApplicationId);
            if (application == null || !application.IsEnabled())
            {
                throw new InvalidOperationException($"Application '{authorizationCode.ApplicationId}' was not valid.");
            }

            var account = _accountService.GetAccount(authorizationCode.AccountId);
            if (account == null || !account.IsEnabled())
            {
                throw new InvalidOperationException($"Account '{authorizationCode.AccountId}' was not valid.");
            }

            var roleScopes = GetAccountRoleScopes(account.Roles);
            List<string> accessTokenScopes = scopes?.ToList();
            if (scopes == null || scopes.Count() == 0)
            {
                accessTokenScopes = roleScopes;
            }
            else
            {
                foreach (string scope in scopes)
                {
                    if (!roleScopes.Contains(scope))
                    {
                        throw new InvalidOperationException($"Scope '{scope}' was not included in the account role scope.");
                    }
                }
            }

            var now = _dateTimeProvider.Now;
            var accessToken = new AccessToken
            {
                Token = StringUtil.CreateCode(),
                ApplicationId = authorizationCode.ApplicationId,
                PrincipalType = PrincipalType.User.ToString(),
                PrincipalId = authorizationCode.AccountId,
                Name = "AuthorizationCode - " + now.ToISO8601(),
                Description = string.Empty,
                Roles = account.Roles,
                Scopes = accessTokenScopes,
                Status = AccessTokenStatus.NORMAL,
                ExpiryTime = now.AddSeconds(_appSettings.AccessTokenExpiresIn),
                CreateTime = now,
                UpdateTime = now,
            };

            var refreshToken = new RefreshToken
            {
                Token = StringUtil.CreateCode(),
                ApplicationId = authorizationCode.ApplicationId,
                PrincipalType = PrincipalType.User.ToString(),
                PrincipalId = authorizationCode.AccountId,
                Name = "AuthorizationCode - " + now.ToISO8601(),
                Description = string.Empty,
                Roles = account.Roles,
                Scopes = accessTokenScopes,
                Status = RefreshTokenStatus.NORMAL,
                ExpiryTime = now.AddSeconds(_appSettings.RefreshTokenExpiresIn),
                CreateTime = now,
                UpdateTime = now,
            };

            authorizationCode.Status = AuthorizationCodeStatus.USED;
            authorizationCode.UpdateTime = _dateTimeProvider.Now;

            using (var scope = new TransactionScope())
            {
                _accessTokenRepository.Create(accessToken);
                _refreshTokenRepository.Create(refreshToken);
                _authorizationCodeRepository.Update(authorizationCode);
                scope.Complete();
            }

            return (accessToken, refreshToken);
        }

        public (AccessToken accessToken, RefreshToken refreshToken) CreateAccessToken(Guid applicationId, IEnumerable<string> scopes)
        {
            if (applicationId == null || applicationId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(applicationId));
            }

            var application = _applicationRepository.GetById(applicationId);
            if (application == null || !application.IsEnabled())
            {
                throw new InvalidOperationException($"Application '{applicationId}' was not valid.");
            }

            List<string> accessTokenScopes = scopes?.ToList();
            if (scopes == null || scopes.Count() == 0)
            {
                accessTokenScopes = application.Scopes;
            }
            else
            {
                foreach (string scope in scopes)
                {
                    if (!application.Scopes.Contains(scope))
                    {
                        throw new InvalidOperationException($"Scope '{scope}' was not included in the application scope.");
                    }
                }
            }

            var now = _dateTimeProvider.Now;
            var accessToken = new AccessToken
            {
                Token = StringUtil.CreateCode(),
                ApplicationId = applicationId,
                PrincipalType = PrincipalType.Application.ToString(),
                PrincipalId = applicationId,
                Name = "ClientCredentials - " + now.ToISO8601(),
                Description = string.Empty,
                Roles = application.Roles,
                Scopes = accessTokenScopes,
                Status = AccessTokenStatus.NORMAL,
                ExpiryTime = now.AddSeconds(_appSettings.AccessTokenExpiresIn),
                CreateTime = now,
                UpdateTime = now,
            };

            var refreshToken = new RefreshToken
            {
                Token = StringUtil.CreateCode(),
                ApplicationId = applicationId,
                PrincipalType = PrincipalType.Application.ToString(),
                PrincipalId = applicationId,
                Name = "ClientCredentials - " + now.ToISO8601(),
                Description = string.Empty,
                Roles = application.Roles,
                Scopes = accessTokenScopes,
                Status = RefreshTokenStatus.NORMAL,
                ExpiryTime = now.AddSeconds(_appSettings.RefreshTokenExpiresIn),
                CreateTime = now,
                UpdateTime = now,
            };

            using (var scope = new TransactionScope())
            {
                _accessTokenRepository.Create(accessToken);
                _refreshTokenRepository.Create(refreshToken);
                scope.Complete();
            }
            
            return (accessToken, refreshToken);
        }

        public AccessToken CreateAccessToken(RefreshToken refreshToken)
        {
            if (refreshToken == null)
            {
                throw new ArgumentNullException(nameof(refreshToken));
            }

            if (!refreshToken.IsEnabled(_dateTimeProvider.Now))
            {
                throw new InvalidOperationException($"RefreshToken '{refreshToken.Token}' was not valid.");
            }

            var application = _applicationRepository.GetById(refreshToken.ApplicationId);
            if (application == null || !application.IsEnabled())
            {
                throw new InvalidOperationException($"Application '{refreshToken.ApplicationId}' was not valid.");
            }

            var now = _dateTimeProvider.Now;
            var accessToken = new AccessToken
            {
                Token = StringUtil.CreateCode(),
                ApplicationId = refreshToken.ApplicationId,
                PrincipalType = refreshToken.PrincipalType,
                PrincipalId = refreshToken.PrincipalId,
                Name = "RefreshToken - " + now.ToISO8601(),
                Description = string.Empty,
                Roles = refreshToken.Roles,
                Scopes = refreshToken.Scopes,
                Status = AccessTokenStatus.NORMAL,
                ExpiryTime = now.AddSeconds(_appSettings.AccessTokenExpiresIn),
                CreateTime = now,
                UpdateTime = now,
            };

            _accessTokenRepository.Create(accessToken);
            return accessToken;
        }

        public AuthorizationCode CreateAuthorizationCode(Guid applicationId, Guid accountId)
        {
            if (applicationId == null || applicationId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(applicationId));
            }

            if (accountId == null || accountId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(accountId));
            }

            var application = _applicationRepository.GetById(applicationId);
            if (application == null || !application.IsEnabled())
            {
                throw new InvalidOperationException($"Application '{applicationId}' was not valid.");
            }

            var account = _accountService.GetAccount(accountId);
            if (account == null || !account.IsEnabled())
            {
                throw new InvalidOperationException($"Account '{accountId}' was not valid.");
            }

            List<string> accessTokenScopes = application.Scopes;

            var now = _dateTimeProvider.Now;
            var authorizationCode = new AuthorizationCode
            {
                Code = StringUtil.CreateCode(),
                ApplicationId = applicationId,
                AccountId = accountId,
                Status = AuthorizationCodeStatus.NORMAL,
                ExpiryTime = now.AddSeconds(_appSettings.AuthorizationCodeExpiresIn),
                CreateTime = now,
                UpdateTime = now,
            };

            _authorizationCodeRepository.Create(authorizationCode);
            return authorizationCode;
        }

        public AccessToken GetAccessToken(string token)
        {
            if (token == null || token == string.Empty)
            {
                throw new ArgumentNullException(nameof(token));
            }

            var accessToken = _accessTokenRepository.GetById(token);
            if (accessToken == null)
            {
                return null;
            }

            if (accessToken.PrincipalType != PrincipalType.Application.ToString() &&
                accessToken.PrincipalType != PrincipalType.User.ToString())
            {
                throw new NotImplementedException();
            }

            var application = _applicationRepository.GetById(accessToken.ApplicationId);
            if (application == null || !application.IsEnabled())
            {
                return null;
            }

            var roles = application.Roles;
            if (accessToken.PrincipalType == PrincipalType.User.ToString())
            {
                var account = _accountService.GetAccount(accessToken.PrincipalId);
                if (account == null || !account.IsEnabled())
                {
                    return null;
                }

                roles = account.Roles;
            }

            accessToken.Roles = roles;

            return accessToken;
        }

        public Domain.Entities.Application GetApplication(string clientId)
        {
            if (clientId == null || clientId == string.Empty)
            {
                throw new ArgumentNullException(nameof(clientId));
            }

            var application = _applicationRepository.GetByClientId(clientId);
            if (application == null)
            {
                return null;
            }

            return application;
        }

        public Domain.Entities.AuthorizationCode GetAuthorizationCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException(nameof(code));
            }

            var authorizationCode = _authorizationCodeRepository.GetById(code);
            if (authorizationCode == null)
            {
                return null;
            }

            return authorizationCode;
        }

        public RefreshToken GetRefreshToken(string token)
        {
            if (token == null || token == string.Empty)
            {
                throw new ArgumentNullException(nameof(token));
            }

            var refreshToken = _refreshTokenRepository.GetById(token);
            if (refreshToken == null)
            {
                return null;
            }

            if (refreshToken.PrincipalType != PrincipalType.Application.ToString() &&
                refreshToken.PrincipalType != PrincipalType.User.ToString())
            {
                throw new NotImplementedException();
            }

            var application = _applicationRepository.GetById(refreshToken.ApplicationId);
            if (application == null || !application.IsEnabled())
            {
                return null;
            }

            var roles = application.Roles;
            if (refreshToken.PrincipalType == PrincipalType.User.ToString())
            {
                var account = _accountService.GetAccount(refreshToken.PrincipalId);
                if (account == null || !account.IsEnabled())
                {
                    return null;
                }

                roles = account.Roles;
            }

            refreshToken.Roles = roles;

            return refreshToken;
        }

        private List<string> GetAccountRoleScopes(List<string> roles)
        {
            var result = new List<string>();
            if (roles.Contains(Roles.Owner))
            {
                result.Add(Scopes.FullControl);
                result.Add(Scopes.Write);
                result.Add(Scopes.Read);
                result.Add(Scopes.User);
            }
            else if (roles.Contains(Roles.Contributor))
            {
                result.Add(Scopes.Write);
                result.Add(Scopes.Read);
                result.Add(Scopes.User);
            }
            else if (roles.Contains(Roles.Reader))
            {
                result.Add(Scopes.Read);
                result.Add(Scopes.User);
            }
            else if (roles.Contains(Roles.User))
            {
                result.Add(Scopes.User);
            }

            return result;
        }
    }
}
