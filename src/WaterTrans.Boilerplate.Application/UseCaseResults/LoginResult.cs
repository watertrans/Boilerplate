using WaterTrans.Boilerplate.Application.Constants;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Application.UseCaseResults
{
    public class LoginResult
    {
        public LoginResult(LoginValidationResult result)
        {
            Result = result;
        }

        public LoginResult(LoginValidationResult result, AuthorizationCode authorizationCode)
        {
            Result = result;
            AuthorizationCode = authorizationCode;
        }

        public LoginValidationResult Result { get; }
        public AuthorizationCode AuthorizationCode { get; }
    }
}
