using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using WaterTrans.Boilerplate.Application.Abstractions.UseCases;
using WaterTrans.Boilerplate.Application.Constants;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.Constants;
using WaterTrans.Boilerplate.Domain.DataTransferObjects;
using WaterTrans.Boilerplate.Web.Api.Filters;
using WaterTrans.Boilerplate.Web.Api.ObjectResults;
using WaterTrans.Boilerplate.Web.Api.RequestObjects;
using WaterTrans.Boilerplate.Web.Api.ResponseObjects;

namespace WaterTrans.Boilerplate.Web.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    public class TokenController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAppSettings _appSetgings;
        private readonly ITokenUseCase _tokenUseCase;
        
        public TokenController(
            IMapper mapper,
            IAppSettings appSetgings,
            ITokenUseCase tokenUseCase)
        {
            _mapper = mapper;
            _appSetgings = appSetgings;
            _tokenUseCase = tokenUseCase;
        }

        [HttpPost]
        [Route("api/v{version:apiVersion}/token")]
        [Consumes("application/x-www-form-urlencoded")]
        [SwaggerOperationFilter(typeof(AnonymousOperationFilter))]
        public ActionResult<TokenResponse> CreateToken([FromForm] TokenCreateRequest request)
        {
            if (request.GrantType == GrantTypes.AuthorizationCode)
            {
                var result = _tokenUseCase.CreateTokenByAuthorizationCode(_mapper.Map<TokenCreateByAuthorizationCodeDto>(request));

                if (result.Result == CreateTokenValidationResult.Success)
                {
                    AppendRefreshTokenCookie(result.RefreshToken.Token);
                    return new TokenResponse
                    {
                        AccessToken = result.AccessToken.Token,
                        ExpiresIn = _appSetgings.AccessTokenExpiresIn,
                        Scope = string.Join(' ', result.AccessToken.Scopes),
                        TokenType = "Bearer",
                    };
                }
                else
                {
                    return GetErrorResult(result.Result);
                }
            }
            else if (request.GrantType == GrantTypes.ClientCredentials)
            {
                var result = _tokenUseCase.CreateTokenByClientCredentials(_mapper.Map<TokenCreateByClientCredentialsDto>(request));

                if (result.Result == CreateTokenValidationResult.Success)
                {
                    AppendRefreshTokenCookie(result.RefreshToken.Token);
                    return new TokenResponse
                    {
                        AccessToken = result.AccessToken.Token,
                        ExpiresIn = _appSetgings.AccessTokenExpiresIn,
                        Scope = string.Join(' ', result.AccessToken.Scopes),
                        TokenType = "Bearer",
                    };
                }
                else
                {
                    return GetErrorResult(result.Result);
                }
            }
            else
            {
                return ErrorObjectResultFactory.InvalidGrantType();
            }
        }

        [HttpPost]
        [Route("api/v{version:apiVersion}/refresh_token")]
        [Consumes("application/x-www-form-urlencoded")]
        [SwaggerOperationFilter(typeof(AnonymousOperationFilter))]
        public ActionResult<TokenResponse> RefreshToken([FromForm][ModelBinder(Name = "grant_type")] string grantType)
        {
            if (grantType != GrantTypes.RefreshToken)
            {
                return ErrorObjectResultFactory.InvalidGrantType();
            }

            string token = Request.Cookies["refresh_token"];
            if (string.IsNullOrEmpty(token))
            {
                return ErrorObjectResultFactory.InvalidRefreshToken();
            }

            var result = _tokenUseCase.CreateTokenByRefreshToken(token);

            if (result.Result == CreateTokenValidationResult.Success)
            {
                return new TokenResponse
                {
                    AccessToken = result.AccessToken.Token,
                    ExpiresIn = _appSetgings.AccessTokenExpiresIn,
                    Scope = string.Join(' ', result.AccessToken.Scopes),
                    TokenType = "Bearer",
                };
            }
            else
            {
                return GetErrorResult(result.Result);
            }
        }

        private void AppendRefreshTokenCookie(string refreshToken)
        {
            var options = new CookieOptions();
            options.MaxAge = TimeSpan.FromHours(12);
            options.HttpOnly = true;
            options.Secure = true;
            options.IsEssential = true;
            options.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
            options.Path = Request.Path.Value.Replace("/token", "/refresh_token", StringComparison.OrdinalIgnoreCase);

            HttpContext.Response.Cookies.Append("refresh_token", refreshToken, options);
        }

        private ActionResult GetErrorResult(CreateTokenValidationResult errorResult)
        {
            if (errorResult == CreateTokenValidationResult.InvalidClient)
            {
                return ErrorObjectResultFactory.InvalidClient();
            }
            else if (errorResult == CreateTokenValidationResult.InvalidCode)
            {
                return ErrorObjectResultFactory.InvalidCode();
            }
            else if (errorResult == CreateTokenValidationResult.InvalidGrantType)
            {
                return ErrorObjectResultFactory.InvalidGrantType();
            }
            else if (errorResult == CreateTokenValidationResult.InvalidScope)
            {
                return ErrorObjectResultFactory.InvalidScope();
            }
            else if (errorResult == CreateTokenValidationResult.InvalidRefreshToken)
            {
                return ErrorObjectResultFactory.InvalidRefreshToken();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
