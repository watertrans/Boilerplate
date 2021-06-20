using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.Abstractions.Repositories;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Persistence.SqlEntities;
using WaterTrans.Boilerplate.Persistence.TableDataGateways;

namespace WaterTrans.Boilerplate.Persistence.Repositories
{
    public class AccessTokenRepository : Repository, IAccessTokenRepository
    {
        private readonly SqlTableDataGateway<AccessTokenSqlEntity> _sqlTableDataGateway;

        public AccessTokenRepository(IDBSettings dbSettings)
            : base(dbSettings)
        {
            _sqlTableDataGateway = new SqlTableDataGateway<AccessTokenSqlEntity>(dbSettings);
        }

        public void Create(AccessToken entity)
        {
            AccessTokenSqlEntity sqlEntity = entity;
            _sqlTableDataGateway.Create(sqlEntity);
        }

        public bool Delete(string token)
        {
            return _sqlTableDataGateway.Delete(new AccessTokenSqlEntity { Token = token });
        }

        public AccessToken GetById(string token)
        {
            AccessToken result = _sqlTableDataGateway.GetById(new AccessTokenSqlEntity { Token = token });
            return result;
        }

        public bool Update(AccessToken entity)
        {
            AccessTokenSqlEntity sqlEntity = entity;
            return _sqlTableDataGateway.Update(sqlEntity);
        }
    }
}
