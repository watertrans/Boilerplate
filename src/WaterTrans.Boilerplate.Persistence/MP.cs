using AutoMapper;
using System;

namespace WaterTrans.Boilerplate.Persistence
{
    internal static class MP
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            return config.CreateMapper();
        });

        internal static IMapper Mapper => Lazy.Value;
    }
}
