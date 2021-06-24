using WaterTrans.Boilerplate.Application.Constants;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Application.UseCaseResults
{
    public class CreateTokenResult
    {
        public CreateTokenResult(CreateTokenState state)
        {
            State = state;
        }

        public CreateTokenResult(CreateTokenState state, AccessToken access)
        {
            State = state;
            AccessToken = access;
        }

        public CreateTokenResult(CreateTokenState state, AccessToken accessToken, RefreshToken refreshToken)
        {
            State = state;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public CreateTokenState State { get; }
        public AccessToken AccessToken { get; }
        public RefreshToken RefreshToken { get; }
    }
}
