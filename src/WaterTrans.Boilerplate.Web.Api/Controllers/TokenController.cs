using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using WaterTrans.Boilerplate.Application.Abstractions.UseCases;
using WaterTrans.Boilerplate.Application.Constants;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.Abstractions.Services;
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
            IAuthorizeService authorizeService,
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

                if (result == CreateTokenResult.Success)
                {
                    return new TokenResponse
                    {
                        AccessToken = _tokenUseCase.AccessToken.Token,
                        ExpiresIn = _appSetgings.AccessTokenExpiresIn,
                        Scope = string.Join(' ', _tokenUseCase.AccessToken.Scopes),
                        TokenType = "Bearer",
                    };
                }
                else
                {
                    return GetErrorResult(result);
                }
            }
            else if (request.GrantType == GrantTypes.ClientCredentials)
            {
                var result = _tokenUseCase.CreateTokenByClientCredentials(_mapper.Map<TokenCreateByClientCredentialsDto>(request));

                if (result == CreateTokenResult.Success)
                {
                    return new TokenResponse
                    {
                        AccessToken = _tokenUseCase.AccessToken.Token,
                        ExpiresIn = _appSetgings.AccessTokenExpiresIn,
                        Scope = string.Join(' ', _tokenUseCase.AccessToken.Scopes),
                        TokenType = "Bearer",
                    };
                }
                else
                {
                    return GetErrorResult(result);
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private ActionResult GetErrorResult(CreateTokenResult errorResult)
        {
            if (errorResult == CreateTokenResult.InvalidClient)
            {
                return ErrorObjectResultFactory.InvalidClient();
            }
            else if (errorResult == CreateTokenResult.InvalidCode)
            {
                return ErrorObjectResultFactory.InvalidCode();
            }
            else if (errorResult == CreateTokenResult.InvalidGrantType)
            {
                return ErrorObjectResultFactory.InvalidGrantType();
            }
            else if (errorResult == CreateTokenResult.InvalidScope)
            {
                return ErrorObjectResultFactory.InvalidScope();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
