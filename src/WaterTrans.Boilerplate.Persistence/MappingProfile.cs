using AutoMapper;
using System;
using System.Collections.Generic;
using WaterTrans.Boilerplate.Domain.Constants;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Domain.Utils;
using WaterTrans.Boilerplate.Domain.ValueObjects;
using WaterTrans.Boilerplate.Persistence.SqlEntities;

namespace WaterTrans.Boilerplate.Persistence
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccessTokenSqlEntity, AccessToken>()
                .ForMember(dest => dest.Scopes, opt => opt.MapFrom(src => JsonUtil.Deserialize<List<string>>(src.Scopes)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (AccessTokenStatus)Enum.Parse(typeof(AccessTokenStatus), src.Status)));
            CreateMap<AccessToken, AccessTokenSqlEntity>()
                .ForMember(dest => dest.Scopes, opt => opt.MapFrom(src => JsonUtil.Serialize(src.Scopes)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            CreateMap<AccountSqlEntity, Account>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => JsonUtil.Deserialize<List<string>>(src.Roles)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (AccountStatus)Enum.Parse(typeof(AccountStatus), src.Status)));
            CreateMap<Account, AccountSqlEntity>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => JsonUtil.Serialize(src.Roles)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            CreateMap<ApplicationSqlEntity, Domain.Entities.Application>()
                .ForMember(dest => dest.GrantTypes, opt => opt.MapFrom(src => JsonUtil.Deserialize<List<string>>(src.GrantTypes)))
                .ForMember(dest => dest.RedirectUris, opt => opt.MapFrom(src => JsonUtil.Deserialize<List<string>>(src.RedirectUris)))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => JsonUtil.Deserialize<List<string>>(src.Roles)))
                .ForMember(dest => dest.PostLogoutRedirectUris, opt => opt.MapFrom(src => JsonUtil.Deserialize<List<string>>(src.PostLogoutRedirectUris)))
                .ForMember(dest => dest.Scopes, opt => opt.MapFrom(src => JsonUtil.Deserialize<List<string>>(src.Scopes)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (ApplicationStatus)Enum.Parse(typeof(ApplicationStatus), src.Status)));
            CreateMap<Domain.Entities.Application, ApplicationSqlEntity>()
                .ForMember(dest => dest.GrantTypes, opt => opt.MapFrom(src => JsonUtil.Serialize(src.GrantTypes)))
                .ForMember(dest => dest.RedirectUris, opt => opt.MapFrom(src => JsonUtil.Serialize(src.RedirectUris)))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => JsonUtil.Serialize(src.Roles)))
                .ForMember(dest => dest.PostLogoutRedirectUris, opt => opt.MapFrom(src => JsonUtil.Serialize(src.PostLogoutRedirectUris)))
                .ForMember(dest => dest.Scopes, opt => opt.MapFrom(src => JsonUtil.Serialize(src.Scopes)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            CreateMap<AuthorizationCodeSqlEntity, AuthorizationCode>();
            CreateMap<AuthorizationCode, AuthorizationCodeSqlEntity>();
            CreateMap<ForecastSqlEntity, Forecast>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => new City(src.CityCode)))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => new Country(src.CountryCode)));
            CreateMap<Forecast, ForecastSqlEntity>()
                .ForMember(dest => dest.CityCode, opt => opt.MapFrom(src => src.City.CityCode))
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.Country.CountryCode));
        }
    }
}
