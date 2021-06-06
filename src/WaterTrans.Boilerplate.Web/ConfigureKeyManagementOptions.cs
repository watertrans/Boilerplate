﻿using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace WaterTrans.Boilerplate.Web
{
    public class ConfigureKeyManagementOptions : IConfigureOptions<KeyManagementOptions>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public ConfigureKeyManagementOptions(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Configure(KeyManagementOptions options)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var provider = scope.ServiceProvider;
                options.XmlRepository = provider.GetRequiredService<IXmlRepository>();
            }
        }
    }
}
