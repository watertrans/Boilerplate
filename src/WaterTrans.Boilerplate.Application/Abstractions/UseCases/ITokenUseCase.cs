using WaterTrans.Boilerplate.Application.Constants;
using WaterTrans.Boilerplate.Domain.DataTransferObjects;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Application.Abstractions.UseCases
{
    public interface ITokenUseCase
    {
        CreateTokenResult CreateTokenByClientCredentials(TokenCreateByClientCredentialsDto dto);
        CreateTokenResult CreateTokenByAuthorizationCode(TokenCreateByAuthorizationCodeDto dto);
        AccessToken AccessToken { get; }
    }
}
