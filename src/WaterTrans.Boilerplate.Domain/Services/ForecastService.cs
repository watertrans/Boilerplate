using System;
using WaterTrans.Boilerplate.Domain.Abstractions.QueryServices;
using WaterTrans.Boilerplate.Domain.Abstractions.Repositories;
using WaterTrans.Boilerplate.Domain.Abstractions.Services;
using WaterTrans.Boilerplate.Domain.DataTransferObjects;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Domain.Exceptions;
using WaterTrans.Boilerplate.Domain.Utils;
using WaterTrans.Boilerplate.Domain.ValueObjects;

namespace WaterTrans.Boilerplate.Domain.Services
{
    public class ForecastService : IForecastService
    {
        private readonly IForecastQueryService _forecastQueryService;
        private readonly IForecastRepository _forecastRepository;

        public ForecastService(
            IForecastQueryService forecastQueryService,
            IForecastRepository forecastRepository)
        {
            _forecastQueryService = forecastQueryService;
            _forecastRepository = forecastRepository;
        }

        public Forecast Create(ForecastCreateDto dto)
        {
            var entity = new Forecast(dto);
            entity.ValidateAndThrow();
            _forecastRepository.Create(entity);
            return entity;
        }

        public void Delete(Guid forecastId)
        {
            if (!_forecastRepository.Delete(forecastId))
            {
                throw new EntityNotFoundException(nameof(Forecast), forecastId.ToString());
            }
        }

        public Forecast GetById(Guid forecastId)
        {
            var result = _forecastRepository.GetById(forecastId);

            if (result == null)
            {
                throw new EntityNotFoundException(nameof(Forecast), forecastId.ToString());
            }

            return result;
        }

        public Forecast Update(ForecastUpdateDto dto)
        {
            var entity = _forecastRepository.GetById(dto.ForecastId);

            if (entity == null)
            {
                throw new EntityNotFoundException(nameof(Forecast), dto.ForecastId.ToString());
            }

            if (entity.UpdateTime != dto.ConcurrencyToken)
            {
                throw new EntityConcurrencyException(nameof(Forecast), dto.ForecastId.ToString());
            }

            if (dto.ForecastCode != null)
            {
                entity.ForecastCode = dto.ForecastCode;
            }

            if (dto.CityCode != null)
            {
                entity.City = new City(dto.CityCode);
            }

            if (dto.CountryCode != null)
            {
                entity.Country = new Country(dto.CountryCode);
            }

            if (dto.Date != null)
            {
                entity.Date = dto.Date.Value;
            }

            if (dto.Summary != null)
            {
                entity.Summary = dto.Summary;
            }

            if (dto.Temperature != null)
            {
                entity.Temperature = dto.Temperature.Value;
            }

            entity.UpdateTime = DateUtil.Now;
            entity.ValidateAndThrow();

            if (!_forecastRepository.Update(entity))
            {
                throw new EntityNotFoundException(nameof(Forecast), dto.ForecastId.ToString());
            }

            return entity;
        }
    }
}
