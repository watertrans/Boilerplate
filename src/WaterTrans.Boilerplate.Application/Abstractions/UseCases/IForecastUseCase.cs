using System;
using System.Collections.Generic;
using WaterTrans.Boilerplate.Domain.DataTransferObjects;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Application.Abstractions.UseCases
{
    public interface IForecastUseCase
    {
        Forecast Create(ForecastCreateDto dto);
        void Delete(Guid forecastId);
        Forecast GetById(Guid forecastId);
        Forecast Update(ForecastUpdateDto dto);
        IList<Forecast> Query(ForecastQueryDto dto);
    }
}
