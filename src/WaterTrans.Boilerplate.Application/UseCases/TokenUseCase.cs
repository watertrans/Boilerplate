using System.Collections.Generic;
using System.Transactions;
using WaterTrans.Boilerplate.Application.Abstractions.UseCases;
using WaterTrans.Boilerplate.Application.Constants;
using WaterTrans.Boilerplate.Domain.Abstractions.Services;
using WaterTrans.Boilerplate.Domain.Constants;
using WaterTrans.Boilerplate.Domain.DataTransferObjects;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Application.UseCases
{
    public class TokenUseCase : ITokenUseCase
    {
        private readonly IAuthorizeService _authorizeService;

        public TokenUseCase(IAuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }

        public AccessToken AccessToken { get; private set; }

        public CreateTokenResult CreateTokenByAuthorizationCode(TokenCreateByAuthorizationCodeDto dto)
        {
            if (string.IsNullOrEmpty(dto.ClientId))
            {
                return CreateTokenResult.InvalidClient;
            }

            var application = _authorizeService.GetApplication(dto.ClientId);

            if (application == null)
            {
                return CreateTokenResult.InvalidClient;
            }

            if (!application.GrantTypes.Contains(GrantTypes.AuthorizationCode))
            {
                return CreateTokenResult.InvalidGrantType;
            }

            if (string.IsNullOrEmpty(dto.Code))
            {
                return CreateTokenResult.InvalidCode;
            }

            var authorizationCode = _authorizeService.GetAuthorizationCode(dto.Code);

            if (authorizationCode == null)
            {
                return CreateTokenResult.InvalidCode;
            }

            if (application.ApplicationId != authorizationCode.ApplicationId)
            {
                return CreateTokenResult.InvalidCode;
            }

            AccessToken = _authorizeService.CreateAccessToken(authorizationCode, null);

            return CreateTokenResult.Success;
        }

        public CreateTokenResult CreateTokenByClientCredentials(TokenCreateByClientCredentialsDto dto)
        {
            if (string.IsNullOrEmpty(dto.ClientId))
            {
                return CreateTokenResult.InvalidClient;
            }

            var application = _authorizeService.GetApplication(dto.ClientId);

            if (application == null || application.ClientSecret != dto.ClientSecret)
            {
                return CreateTokenResult.InvalidClient;
            }

            if (!application.GrantTypes.Contains(GrantTypes.ClientCredentials))
            {
                return CreateTokenResult.InvalidGrantType;
            }

            List<string> scopes = new List<string>();

            if (!string.IsNullOrEmpty(dto.Scope))
            {
                scopes.AddRange(dto.Scope.Split(' ', ','));

                foreach (string scope in scopes)
                {
                    if (!application.Scopes.Contains(scope))
                    {
                        return CreateTokenResult.InvalidScope;
                    }
                }
            }

            AccessToken = _authorizeService.CreateAccessToken(application.ApplicationId, scopes);

            return CreateTokenResult.Success;
        }
    }
}
