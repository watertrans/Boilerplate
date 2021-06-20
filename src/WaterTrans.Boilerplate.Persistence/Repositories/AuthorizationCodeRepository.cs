using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.Abstractions.Repositories;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Persistence.SqlEntities;
using WaterTrans.Boilerplate.Persistence.TableDataGateways;

namespace WaterTrans.Boilerplate.Persistence.Repositories
{
    public class AuthorizationCodeRepository : Repository, IAuthorizationCodeRepository
    {
        private readonly SqlTableDataGateway<AuthorizationCodeSqlEntity> _sqlTableDataGateway;

        public AuthorizationCodeRepository(IDBSettings dbSettings)
            : base(dbSettings)
        {
            _sqlTableDataGateway = new SqlTableDataGateway<AuthorizationCodeSqlEntity>(dbSettings);
        }

        public void Create(AuthorizationCode entity)
        {
            AuthorizationCodeSqlEntity sqlEntity = entity;
            _sqlTableDataGateway.Create(sqlEntity);
        }

        public bool Delete(string code)
        {
            return _sqlTableDataGateway.Delete(new AuthorizationCodeSqlEntity { Code = code });
        }

        public AuthorizationCode GetById(string code)
        {
            AuthorizationCode result = _sqlTableDataGateway.GetById(new AuthorizationCodeSqlEntity { Code = code });
            return result;
        }

        public bool Update(AuthorizationCode entity)
        {
            AuthorizationCodeSqlEntity sqlEntity = entity;
            return _sqlTableDataGateway.Update(sqlEntity);
        }
    }
}
