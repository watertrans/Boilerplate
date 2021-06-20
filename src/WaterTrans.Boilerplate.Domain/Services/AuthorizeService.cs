using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using WaterTrans.Boilerplate.CrossCuttingConcerns.Abstractions.OS;
using WaterTrans.Boilerplate.CrossCuttingConcerns.ExtensionMethods;
using WaterTrans.Boilerplate.CrossCuttingConcerns.Utils;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.Abstractions.QueryServices;
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
        private readonly IAuthorizationCodeRepository _authorizationCodeRepository;
        private readonly IApplicationQueryService _applicationQueryService;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IAccountService _accountService;
        private readonly IDateTimeProvider _dateTimeProvider;

        public AuthorizeService(
            IAppSettings appSettings,
            IAccessTokenRepository accessTokenRepository,
            IAuthorizationCodeRepository authorizationCodeRepository,
            IApplicationQueryService applicationQueryService,
            IApplicationRepository applicationRepository,
            IAccountService accountService,
            IDateTimeProvider dateTimeProvider)
        {
            _appSettings = appSettings;
            _accessTokenRepository = accessTokenRepository;
            _authorizationCodeRepository = authorizationCodeRepository;
            _applicationQueryService = applicationQueryService;
            _applicationRepository = applicationRepository;
            _accountService = accountService;
            _dateTimeProvider = dateTimeProvider;
        }

        public AccessToken CreateAccessToken(AuthorizationCode authorizationCode, IEnumerable<string> scopes)
        {
            if (authorizationCode == null)
            {
                throw new ArgumentNullException(nameof(authorizationCode));
            }

            if (authorizationCode.Status != AuthorizationCodeStatus.NORMAL)
            {
                throw new InvalidOperationException($"AuthorizationCode '{authorizationCode.Code}' was not valid.");
            }

            var application = _applicationRepository.GetById(authorizationCode.ApplicationId);
            if (application == null)
            {
                throw new InvalidOperationException($"Application '{authorizationCode.ApplicationId}' was not found.");
            }

            if (application.Status != ApplicationStatus.NORMAL)
            {
                throw new InvalidOperationException($"Application '{authorizationCode.ApplicationId}' was not valid.");
            }

            var account = _accountService.GetAccount(authorizationCode.AccountId);
            if (account == null)
            {
                throw new InvalidOperationException($"Account '{authorizationCode.AccountId}' was not found.");
            }

            if (account.Status != AccountStatus.NORMAL)
            {
                throw new InvalidOperationException($"Account '{authorizationCode.AccountId}' was not valid.");
            }

            var roleScopes = GetAccountRoleScopes(account.Roles);
            List<string> accessTokenScopes = scopes.ToList();
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
                Name = authorizationCode.AccountId + " - " + now.ToISO8601(),
                Description = string.Empty,
                Roles = account.Roles,
                Scopes = accessTokenScopes,
                Status = AccessTokenStatus.NORMAL,
                ExpiryTime = now.AddSeconds(_appSettings.AccessTokenExpiresIn),
                CreateTime = now,
                UpdateTime = now,
            };

            authorizationCode.Status = AuthorizationCodeStatus.USED;
            authorizationCode.UpdateTime = _dateTimeProvider.Now;

            using (var scope = new TransactionScope())
            {
                _accessTokenRepository.Create(accessToken);
                _authorizationCodeRepository.Update(authorizationCode);
            }

            return accessToken;
        }

        public AccessToken CreateAccessToken(Guid applicationId, IEnumerable<string> scopes)
        {
            if (applicationId == null || applicationId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(applicationId));
            }

            var application = _applicationRepository.GetById(applicationId);
            if (application == null)
            {
                throw new InvalidOperationException($"Application '{applicationId}' was not found.");
            }

            if (application.Status != ApplicationStatus.NORMAL)
            {
                throw new InvalidOperationException($"Application '{applicationId}' was not valid.");
            }

            List<string> accessTokenScopes = scopes.ToList();
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
                Name = application.Name + " - " + now.ToISO8601(),
                Description = string.Empty,
                Roles = application.Roles,
                Scopes = accessTokenScopes,
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
            if (application == null || application.Status != ApplicationStatus.NORMAL)
            {
                throw new InvalidOperationException($"Application '{applicationId}' was not found.");
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
            if (accessToken == null || accessToken.Status != AccessTokenStatus.NORMAL)
            {
                return null;
            }

            if (accessToken.PrincipalType != PrincipalType.Application.ToString() &&
                accessToken.PrincipalType != PrincipalType.User.ToString())
            {
                throw new NotImplementedException();
            }

            var application = _applicationRepository.GetById(accessToken.ApplicationId);
            if (application == null || application.Status != ApplicationStatus.NORMAL)
            {
                return null;
            }

            var roles = application.Roles;
            if (accessToken.PrincipalType == PrincipalType.User.ToString())
            {
                var account = _accountService.GetAccount(accessToken.PrincipalId);
                if (account == null)
                {
                    return null;
                }

                if (account.Status != AccountStatus.NORMAL)
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

            var application = _applicationQueryService.GetByClientId(clientId);
            if (application == null || application.Status != ApplicationStatus.NORMAL)
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
            if (authorizationCode == null || authorizationCode.Status != AuthorizationCodeStatus.NORMAL)
            {
                return null;
            }

            if (authorizationCode.ExpiryTime < _dateTimeProvider.Now)
            {
                return null;
            }

            return authorizationCode;
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
