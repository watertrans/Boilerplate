using System;
using WaterTrans.Boilerplate.Domain.Abstractions.Repositories;
using WaterTrans.Boilerplate.Domain.Abstractions.Services;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Domain.Utils;

namespace WaterTrans.Boilerplate.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void UpdateLastLoginTime(Guid accountId)
        {
            var account = _accountRepository.GetById(accountId);
            account.LastLoginTime = DateUtil.Now;
            _accountRepository.Update(account);
        }

        public bool ExistsAccount(Guid accountId)
        {
            return _accountRepository.GetById(accountId) != null;
        }

        public Account GetAccount(Guid accountId)
        {
            return _accountRepository.GetById(accountId);
        }
    }
}
