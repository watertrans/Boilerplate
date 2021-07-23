using System;
using System.Collections.Generic;
using WaterTrans.Boilerplate.Application.Abstractions.UseCases;
using WaterTrans.Boilerplate.Domain.Abstractions.QueryServices;
using WaterTrans.Boilerplate.Domain.Abstractions.Repositories;
using WaterTrans.Boilerplate.Domain.Abstractions.Services;
using WaterTrans.Boilerplate.Domain.DataTransferObjects;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Application.UseCases
{
    public class ForecastUseCase : IForecastUseCase
    {
        private readonly IForecastQueryService _forecastQueryService;
        private readonly IForecastRepository _forecastRepository;
        private readonly IForecastService _forecastService;

        public ForecastUseCase(
            IForecastQueryService forecastQueryService,
            IForecastRepository forecastRepository,
            IForecastService forecastService)
        {
            _forecastQueryService = forecastQueryService;
            _forecastRepository = forecastRepository;
            _forecastService = forecastService;
        }

        public Forecast Create(ForecastCreateDto dto)
        {
            return _forecastService.Create(dto);
        }

        public void Delete(Guid forecastId)
        {
            _forecastService.Delete(forecastId);
        }

        public Forecast GetById(Guid forecastId)
        {
            return _forecastService.GetById(forecastId);
        }

        public IList<Forecast> Query(ForecastQueryDto dto)
        {
            return _forecastQueryService.Query(dto.Query, dto.Sort, dto);
        }

        public Forecast Update(ForecastUpdateDto dto)
        {
            return _forecastService.Update(dto);
        }
    }
}
