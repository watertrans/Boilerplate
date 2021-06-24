using WaterTrans.Boilerplate.Application.Abstractions.UseCases;
using WaterTrans.Boilerplate.Application.Constants;
using WaterTrans.Boilerplate.Application.UseCaseResults;
using WaterTrans.Boilerplate.Domain.Abstractions.Services;

namespace WaterTrans.Boilerplate.Application.UseCases
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IAccountService _accountService;
        private readonly IAuthorizeService _authorizeService;

        public LoginUseCase(
            IAccountService accountService,
            IAuthorizeService authorizeService)
        {
            _accountService = accountService;
            _authorizeService = authorizeService;
        }

        public LoginResult Login(string clientId, string loginId, string password)
        {
            var application = _authorizeService.GetApplication(clientId);

            if (application == null || !application.IsEnabled())
            {
                return new LoginResult(LoginState.InvalidClientId);
            }

            var account = _accountService.GetAccountByLoginId(loginId);

            if (account == null || !account.IsEnabled())
            {
                return new LoginResult(LoginState.InvalidLoginId);
            }

            if (!_accountService.VerifyPassword(password, account))
            {
                return new LoginResult(LoginState.InvalidPassword);
            }

            _accountService.UpdateLastLoginTime(account.AccountId);

            var authorizationCode = _authorizeService.CreateAuthorizationCode(application.ApplicationId, account.AccountId);

            return new LoginResult(LoginState.Success, authorizationCode);
        }
    }
}
