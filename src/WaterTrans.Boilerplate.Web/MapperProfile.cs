using AutoMapper;
using System;
using WaterTrans.Boilerplate.Domain;
using WaterTrans.Boilerplate.Domain.DataTransferObjects;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Domain.Utils;
using WaterTrans.Boilerplate.Web.RequestObjects;
using WaterTrans.Boilerplate.Web.ResponseObjects;

namespace WaterTrans.Boilerplate.Web
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            AllowNullCollections = true;
            CreateMap<ForecastCreateRequest, ForecastCreateDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateUtil.ParseISO8601Date(src.Date)));
            CreateMap<ForecastQueryRequest, ForecastQueryDto>()
                .ForMember(dest => dest.Page, opt => opt.MapFrom(src => src.Page ?? PagingQuery.DefaultPage))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize ?? PagingQuery.DefaultPageSize))
                .ForMember(dest => dest.Sort, opt => opt.MapFrom(src => SortOrder.Parse(src.Sort)));
            CreateMap<ForecastUpdateRequest, ForecastUpdateDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date == null ? (DateTime?)null : DateUtil.ParseISO8601Date(src.Date)))
                .ForMember(dest => dest.ConcurrencyToken, opt => opt.MapFrom(src => DateUtil.ParseISO8601(src.ConcurrencyToken)));
            CreateMap<Forecast, ForecastResponse>()
                .ForMember(dest => dest.ForecastId, opt => opt.MapFrom(src => src.ForecastId.ToString()))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToISO8601Date()))
                .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime.ToISO8601()))
                .ForMember(dest => dest.UpdateTime, opt => opt.MapFrom(src => src.UpdateTime.ToISO8601()));
            CreateMap<TokenCreateRequest, TokenCreateByClientCredentialsDto>();
            CreateMap<TokenCreateRequest, TokenCreateByAuthorizationCodeDto>();
        }
    }
}
