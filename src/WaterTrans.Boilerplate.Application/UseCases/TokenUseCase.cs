using System.Collections.Generic;
using WaterTrans.Boilerplate.Application.Abstractions.UseCases;
using WaterTrans.Boilerplate.Application.Constants;
using WaterTrans.Boilerplate.Application.UseCaseResults;
using WaterTrans.Boilerplate.Domain.Abstractions.Services;
using WaterTrans.Boilerplate.Domain.Constants;
using WaterTrans.Boilerplate.Domain.DataTransferObjects;

namespace WaterTrans.Boilerplate.Application.UseCases
{
    public class TokenUseCase : ITokenUseCase
    {
        private readonly IAuthorizeService _authorizeService;

        public TokenUseCase(IAuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }

        public CreateTokenResult CreateTokenByAuthorizationCode(TokenCreateByAuthorizationCodeDto dto)
        {
            if (string.IsNullOrEmpty(dto.ClientId))
            {
                return new CreateTokenResult(CreateTokenState.InvalidClient);
            }

            var application = _authorizeService.GetApplication(dto.ClientId);

            if (application == null)
            {
                return new CreateTokenResult(CreateTokenState.InvalidClient);
            }

            if (!application.GrantTypes.Contains(GrantTypes.AuthorizationCode))
            {
                return new CreateTokenResult(CreateTokenState.InvalidGrantType);
            }

            if (string.IsNullOrEmpty(dto.Code))
            {
                return new CreateTokenResult(CreateTokenState.InvalidCode);
            }

            var authorizationCode = _authorizeService.GetAuthorizationCode(dto.Code);

            if (authorizationCode == null)
            {
                return new CreateTokenResult(CreateTokenState.InvalidCode);
            }

            if (application.ApplicationId != authorizationCode.ApplicationId)
            {
                return new CreateTokenResult(CreateTokenState.InvalidCode);
            }

            var (accessToken, refreshToken) = _authorizeService.CreateAccessToken(authorizationCode, null);

            return new CreateTokenResult(CreateTokenState.Success, accessToken, refreshToken);
        }

        public CreateTokenResult CreateTokenByClientCredentials(TokenCreateByClientCredentialsDto dto)
        {
            if (string.IsNullOrEmpty(dto.ClientId))
            {
                return new CreateTokenResult(CreateTokenState.InvalidClient);
            }

            var application = _authorizeService.GetApplication(dto.ClientId);

            if (application == null || application.ClientSecret != dto.ClientSecret)
            {
                return new CreateTokenResult(CreateTokenState.InvalidClient);
            }

            if (!application.GrantTypes.Contains(GrantTypes.ClientCredentials))
            {
                return new CreateTokenResult(CreateTokenState.InvalidGrantType);
            }

            List<string> scopes = new List<string>();

            if (!string.IsNullOrEmpty(dto.Scope))
            {
                scopes.AddRange(dto.Scope.Split(' ', ','));

                foreach (string scope in scopes)
                {
                    if (!application.Scopes.Contains(scope))
                    {
                        return new CreateTokenResult(CreateTokenState.InvalidScope);
                    }
                }
            }

            var (accessToken, refreshToken) = _authorizeService.CreateAccessToken(application.ApplicationId, scopes);

            return new CreateTokenResult(CreateTokenState.Success, accessToken, refreshToken);
        }

        public CreateTokenResult CreateTokenByRefreshToken(string token)
        {
            var refreshToken = _authorizeService.GetRefreshToken(token);

            if (refreshToken == null)
            {
                return new CreateTokenResult(CreateTokenState.InvalidRefreshToken);
            }

            var accessToken = _authorizeService.CreateAccessToken(refreshToken);

            return new CreateTokenResult(CreateTokenState.Success, accessToken);
        }
    }
}
