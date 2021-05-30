using System;
using WaterTrans.Boilerplate.Domain.DataTransferObjects;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Domain.Abstractions.Services
{
    public interface IForecastService
    {
        Forecast Create(ForecastCreateDto dto);
        Forecast Update(ForecastUpdateDto dto);
        void Delete(Guid forecastId);
        Forecast GetById(Guid forecastId);
    }
}
