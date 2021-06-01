using System;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.Abstractions.Repositories;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Persistence.SqlEntities;

namespace WaterTrans.Boilerplate.Persistence.Repositories
{
    public class AccountRepository : Repository, IAccountRepository
    {
        private readonly SqlRepository<AccountSqlEntity> _sqlRepository;

        public AccountRepository(IDBSettings dbSettings)
            : base(dbSettings)
        {
            _sqlRepository = new SqlRepository<AccountSqlEntity>(dbSettings);
        }

        public void Create(Account entity)
        {
            AccountSqlEntity sqlEntity = entity;
            _sqlRepository.Create(sqlEntity);
        }

        public bool Delete(Guid accountId)
        {
            return _sqlRepository.Delete(new AccountSqlEntity { AccountId = accountId });
        }

        public Account GetById(Guid accountId)
        {
            Account result = _sqlRepository.GetById(new AccountSqlEntity { AccountId = accountId });
            return result;
        }

        public bool Update(Account entity)
        {
            AccountSqlEntity sqlEntity = entity;
            return _sqlRepository.Update(sqlEntity);
        }
    }
}
