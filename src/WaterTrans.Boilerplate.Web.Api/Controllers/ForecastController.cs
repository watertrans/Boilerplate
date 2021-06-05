using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WaterTrans.Boilerplate.Application.Abstractions.UseCases;
using WaterTrans.Boilerplate.Domain.DataTransferObjects;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Web.Api.RequestObjects;
using WaterTrans.Boilerplate.Web.Api.ResponseObjects;
using WaterTrans.Boilerplate.Web.DataAnnotations;

namespace WaterTrans.Boilerplate.Web.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    public class ForecastController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IForecastUseCase _usecase;

        public ForecastController(IMapper mapper, IForecastUseCase usecase)
        {
            _mapper = mapper;
            _usecase = usecase;
        }

        [HttpPost]
        [Route("api/v{version:apiVersion}/forecasts")]
        public ActionResult<ForecastResponse> Create([FromBody] ForecastCreateRequest request)
        {
            var dto = _mapper.Map<ForecastCreateRequest, ForecastCreateDto>(request);
            var forecast = _usecase.Create(dto);
            return _mapper.Map<Forecast, ForecastResponse>(forecast);
        }

        [HttpDelete]
        [Route("api/v{version:apiVersion}/forecasts/{forecastId}")]
        public ActionResult Delete(
            [FromRoute, Required(ErrorMessage = "DataAnnotationRequired"), Guid(ErrorMessage = "DataAnnotationGuid")] string forecastId)
        {
            _usecase.Delete(Guid.Parse(forecastId));
            return new OkResult();
        }

        [HttpGet]
        [Route("api/v{version:apiVersion}/forecasts/{forecastId}")]
        public ActionResult<ForecastResponse> Get(
            [FromRoute, Required(ErrorMessage = "DataAnnotationRequired"), Guid(ErrorMessage = "DataAnnotationGuid")] string forecastId)
        {
            var forecast = _usecase.GetById(Guid.Parse(forecastId));
            return _mapper.Map<Forecast, ForecastResponse>(forecast);
        }

        [HttpGet]
        [Route("api/v{version:apiVersion}/forecasts")]
        public PagedObject<ForecastResponse> Query([FromQuery]ForecastQueryRequest request)
        {
            var dto = _mapper.Map<ForecastQueryRequest, ForecastQueryDto>(request);
            var entities = _usecase.Query(dto);
            var result = new PagedObject<ForecastResponse>
            {
                Page = dto.Page,
                PageSize = dto.PageSize,
                Total = dto.TotalCount,
                Items = _mapper.Map<IList<Forecast>, List<ForecastResponse>>(entities),
            };
            return result;
        }

        [HttpPatch]
        [Route("api/v{version:apiVersion}/forecasts/{forecastId}")]
        public ActionResult<ForecastResponse> Update(
            [FromRoute, Required(ErrorMessage = "DataAnnotationRequired"), Guid(ErrorMessage = "DataAnnotationGuid")] string forecastId,
            [FromBody] ForecastUpdateRequest request)
        {
            var dto = _mapper.Map<ForecastUpdateRequest, ForecastUpdateDto>(request);
            dto.ForecastId = Guid.Parse(forecastId);
            var forecast = _usecase.Update(dto);
            return _mapper.Map<Forecast, ForecastResponse>(forecast);
        }
    }
}
