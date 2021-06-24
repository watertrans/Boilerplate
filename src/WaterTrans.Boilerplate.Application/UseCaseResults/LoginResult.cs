using WaterTrans.Boilerplate.Application.Constants;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Application.UseCaseResults
{
    public class LoginResult
    {
        public LoginResult(LoginState state)
        {
            State = state;
        }

        public LoginResult(LoginState state, AuthorizationCode authorizationCode)
        {
            State = state;
            AuthorizationCode = authorizationCode;
        }

        public LoginState State { get; }
        public AuthorizationCode AuthorizationCode { get; }
    }
}
