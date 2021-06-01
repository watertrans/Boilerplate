using System;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Domain.Abstractions.Repositories
{
    public interface IForecastRepository
    {
        void Create(Forecast entity);
        Forecast GetById(Guid forecastId);
        bool Update(Forecast entity);
        bool Delete(Guid forecastId);
    }
}
