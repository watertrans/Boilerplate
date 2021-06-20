using WaterTrans.Boilerplate.Application.UseCaseResults;
using WaterTrans.Boilerplate.Domain.DataTransferObjects;

namespace WaterTrans.Boilerplate.Application.Abstractions.UseCases
{
    public interface ITokenUseCase
    {
        CreateTokenResult CreateTokenByClientCredentials(TokenCreateByClientCredentialsDto dto);
        CreateTokenResult CreateTokenByAuthorizationCode(TokenCreateByAuthorizationCodeDto dto);
        CreateTokenResult CreateTokenByRefreshToken(string token);
    }
}
