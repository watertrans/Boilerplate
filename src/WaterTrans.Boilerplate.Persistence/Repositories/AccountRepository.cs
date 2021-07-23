using System;
using System.Linq;
using System.Text;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.Abstractions.Repositories;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Persistence.SqlEntities;
using WaterTrans.Boilerplate.Persistence.TableDataGateways;

namespace WaterTrans.Boilerplate.Persistence.Repositories
{
    public class AccountRepository : Repository, IAccountRepository
    {
        private readonly SqlTableDataGateway<AccountSqlEntity> _sqlTableDataGateway;

        public AccountRepository(IDBSettings dbSettings)
            : base(dbSettings)
        {
            _sqlTableDataGateway = new SqlTableDataGateway<AccountSqlEntity>(dbSettings);
        }

        public void Create(Account entity)
        {
            AccountSqlEntity sqlEntity = entity;
            _sqlTableDataGateway.Create(sqlEntity);
        }

        public bool Delete(Guid accountId)
        {
            return _sqlTableDataGateway.Delete(new AccountSqlEntity { AccountId = accountId });
        }

        public Account GetById(Guid accountId)
        {
            Account result = _sqlTableDataGateway.GetById(new AccountSqlEntity { AccountId = accountId });
            return result;
        }

        public Account GetByLoginId(string loginId)
        {
            var sql = new StringBuilder();
            sql.AppendLine(" SELECT * ");
            sql.AppendLine("   FROM `Account` ");
            sql.AppendLine("  WHERE `LoginId` = @LoginId ");

            var param = new
            {
                LoginId = loginId,
            };

            Account result = _sqlTableDataGateway.ExecuteQuery(sql.ToString(), param).SingleOrDefault();
            return result;
        }

        public bool Update(Account entity)
        {
            AccountSqlEntity sqlEntity = entity;
            return _sqlTableDataGateway.Update(sqlEntity);
        }
    }
}
