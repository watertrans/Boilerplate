using System;
using WaterTrans.Boilerplate.CrossCuttingConcerns.Abstractions.Cryptography;
using WaterTrans.Boilerplate.CrossCuttingConcerns.Abstractions.OS;
using WaterTrans.Boilerplate.Domain.Abstractions.Repositories;
using WaterTrans.Boilerplate.Domain.Abstractions.Services;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHashProvider _passwordHashProvider;
        private readonly IDateTimeProvider _dateTimeProvider;

        public AccountService(IAccountRepository accountRepository, IPasswordHashProvider passwordHashProvider, IDateTimeProvider dateTimeProvider)
        {
            _accountRepository = accountRepository;
            _passwordHashProvider = passwordHashProvider;
            _dateTimeProvider = dateTimeProvider;
        }

        public void UpdateLastLoginTime(Guid accountId)
        {
            var account = _accountRepository.GetById(accountId);
            account.LastLoginTime = _dateTimeProvider.Now;
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

        public bool VerifyPassword(string password, Account account)
        {
            return _passwordHashProvider.Verify(password, account.Salt, account.Iterations, account.Password);
        }
    }
}
