using WaterTrans.Boilerplate.Application.UseCaseResults;

namespace WaterTrans.Boilerplate.Application.Abstractions.UseCases
{
    public interface ILoginUseCase
    {
        LoginResult Login(string clientId, string loginId, string password);
    }
}
