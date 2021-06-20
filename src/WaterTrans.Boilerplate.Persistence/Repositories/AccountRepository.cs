using System;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.Abstractions.Repositories;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Persistence.SqlEntities;
using WaterTrans.Boilerplate.Persistence.TableDataGateways;

namespace WaterTrans.Boilerplate.Persistence.Repositories
{
    public class AccountRepository : Repository, IAccountRepository
    {
        private readonly SqlTableDataGateway<AccountSqlEntity> _sqlRepository;

        public AccountRepository(IDBSettings dbSettings)
            : base(dbSettings)
        {
            _sqlRepository = new SqlTableDataGateway<AccountSqlEntity>(dbSettings);
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
