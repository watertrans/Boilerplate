using System;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Domain.Abstractions.Repositories
{
    public interface IAccountRepository
    {
        void Create(Account entity);
        Account GetById(Guid accountId);
        Account GetByLoginId(string loginId);
        bool Update(Account entity);
        bool Delete(Guid accountId);
    }
}
