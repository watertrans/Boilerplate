using System;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Domain.Abstractions.Services
{
    public interface IAccountService
    {
        Account GetAccount(Guid accountId);
        Account GetAccountByLoginId(string loginId);
        bool ExistsAccount(Guid accountId);
        void UpdateLastLoginTime(Guid accountId);
        bool VerifyPassword(string password, Account account);
    }
}
