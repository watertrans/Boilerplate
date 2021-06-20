using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.Abstractions.Repositories;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Persistence.SqlEntities;
using WaterTrans.Boilerplate.Persistence.TableDataGateways;

namespace WaterTrans.Boilerplate.Persistence.Repositories
{
    public class RefreshTokenRepository : Repository, IRefreshTokenRepository
    {
        private readonly SqlTableDataGateway<RefreshTokenSqlEntity> _sqlTableDataGateway;

        public RefreshTokenRepository(IDBSettings dbSettings)
            : base(dbSettings)
        {
            _sqlTableDataGateway = new SqlTableDataGateway<RefreshTokenSqlEntity>(dbSettings);
        }

        public void Create(RefreshToken entity)
        {
            RefreshTokenSqlEntity sqlEntity = entity;
            _sqlTableDataGateway.Create(sqlEntity);
        }

        public bool Delete(string token)
        {
            return _sqlTableDataGateway.Delete(new RefreshTokenSqlEntity { Token = token });
        }

        public RefreshToken GetById(string token)
        {
            RefreshToken result = _sqlTableDataGateway.GetById(new RefreshTokenSqlEntity { Token = token });
            return result;
        }

        public bool Update(RefreshToken entity)
        {
            RefreshTokenSqlEntity sqlEntity = entity;
            return _sqlTableDataGateway.Update(sqlEntity);
        }
    }
}
