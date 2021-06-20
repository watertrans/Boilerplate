using WaterTrans.Boilerplate.Application.Constants;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Application.UseCaseResults
{
    public class CreateTokenResult
    {
        public CreateTokenResult(CreateTokenValidationResult result)
        {
            Result = result;
        }

        public CreateTokenResult(CreateTokenValidationResult result, AccessToken access)
        {
            Result = result;
            AccessToken = access;
        }

        public CreateTokenResult(CreateTokenValidationResult result, AccessToken access, RefreshToken refreshToken)
        {
            Result = result;
            AccessToken = access;
            RefreshToken = refreshToken;
        }

        public CreateTokenValidationResult Result { get; }
        public AccessToken AccessToken { get; }
        public RefreshToken RefreshToken { get; }
    }
}
