using System;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.Abstractions.Repositories;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Persistence.SqlEntities;

namespace WaterTrans.Boilerplate.Persistence.Repositories
{
    public class ForecastRepository : Repository, IForecastRepository
    {
        private readonly SqlRepository<ForecastSqlEntity> _sqlRepository;

        public ForecastRepository(IDBSettings dbSettings)
            : base(dbSettings)
        {
            _sqlRepository = new SqlRepository<ForecastSqlEntity>(dbSettings);
        }

        public void Create(Forecast entity)
        {
            ForecastSqlEntity sqlEntity = entity;
            _sqlRepository.Create(sqlEntity);
        }

        public bool Delete(Guid forecastId)
        {
            return _sqlRepository.Delete(new ForecastSqlEntity { ForecastId = forecastId });
        }

        public Forecast Read(Guid forecastId)
        {
            Forecast result = _sqlRepository.Read(new ForecastSqlEntity { ForecastId = forecastId });
            return result;
        }

        public bool Update(Forecast entity)
        {
            ForecastSqlEntity sqlEntity = entity;
            return _sqlRepository.Update(sqlEntity);
        }
    }
}
