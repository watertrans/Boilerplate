using System;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Domain.Abstractions.Services
{
    public interface IAccountService
    {
        Account GetAccount(Guid accountId);
        bool ExistsAccount(Guid accountId);
        void UpdateLastLoginTime(Guid accountId);
    }
}
