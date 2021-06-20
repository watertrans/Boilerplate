using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.Abstractions.Repositories;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Persistence.SqlEntities;
using WaterTrans.Boilerplate.Persistence.TableDataGateways;

namespace WaterTrans.Boilerplate.Persistence.Repositories
{
    public class RefreshTokenRepository : Repository, IRefreshTokenRepository
    {
        private readonly SqlTableDataGateway<RefreshTokenSqlEntity> _sqlRepository;

        public RefreshTokenRepository(IDBSettings dbSettings)
            : base(dbSettings)
        {
            _sqlRepository = new SqlTableDataGateway<RefreshTokenSqlEntity>(dbSettings);
        }

        public void Create(RefreshToken entity)
        {
            RefreshTokenSqlEntity sqlEntity = entity;
            _sqlRepository.Create(sqlEntity);
        }

        public bool Delete(string token)
        {
            return _sqlRepository.Delete(new RefreshTokenSqlEntity { Token = token });
        }

        public RefreshToken GetById(string token)
        {
            RefreshToken result = _sqlRepository.GetById(new RefreshTokenSqlEntity { Token = token });
            return result;
        }

        public bool Update(RefreshToken entity)
        {
            RefreshTokenSqlEntity sqlEntity = entity;
            return _sqlRepository.Update(sqlEntity);
        }
    }
}
