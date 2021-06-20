using System;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.Abstractions.Repositories;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Persistence.SqlEntities;
using WaterTrans.Boilerplate.Persistence.TableDataGateways;

namespace WaterTrans.Boilerplate.Persistence.Repositories
{
    public class ForecastRepository : Repository, IForecastRepository
    {
        private readonly SqlTableDataGateway<ForecastSqlEntity> _sqlTableDataGateway;

        public ForecastRepository(IDBSettings dbSettings)
            : base(dbSettings)
        {
            _sqlTableDataGateway = new SqlTableDataGateway<ForecastSqlEntity>(dbSettings);
        }

        public void Create(Forecast entity)
        {
            ForecastSqlEntity sqlEntity = entity;
            _sqlTableDataGateway.Create(sqlEntity);
        }

        public bool Delete(Guid forecastId)
        {
            return _sqlTableDataGateway.Delete(new ForecastSqlEntity { ForecastId = forecastId });
        }

        public Forecast GetById(Guid forecastId)
        {
            Forecast result = _sqlTableDataGateway.GetById(new ForecastSqlEntity { ForecastId = forecastId });
            return result;
        }

        public bool Update(Forecast entity)
        {
            ForecastSqlEntity sqlEntity = entity;
            return _sqlTableDataGateway.Update(sqlEntity);
        }
    }
}
