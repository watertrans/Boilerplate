using AutoMapper;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Domain.ValueObjects;
using WaterTrans.Boilerplate.Persistence.SqlEntities;

namespace WaterTrans.Boilerplate.Persistence
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ForecastSqlEntity, Forecast>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => new City(src.CityCode)))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => new Country(src.CountryCode)));
            CreateMap<Forecast, ForecastSqlEntity>()
                .ForMember(dest => dest.CityCode, opt => opt.MapFrom(src => src.City.CityCode))
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.Country.CountryCode));
        }
    }
}
