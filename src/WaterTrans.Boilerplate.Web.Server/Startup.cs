using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MySqlConnector;
using WaterTrans.Boilerplate.Application.Settings;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Persistence.Repositories;
using WaterTrans.Boilerplate.Web.AttributeAdapters;
using WaterTrans.Boilerplate.Web.Resources;

namespace WaterTrans.Boilerplate.Web.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(Configuration.GetSection("Kestrel"));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<EnvSettings>(Configuration.GetSection("EnvSettings"));
            services.Configure<DBSettings>(options =>
            {
                Configuration.GetSection("DBSettings").Bind(options);
                options.SqlProviderFactory = MySqlConnectorFactory.Instance;
            });

            // This is work around. See https://github.com/dotnet/aspnetcore/issues/4853 @zhurinvlad commented on 5 Sep 2018
            services.AddSingleton<IValidationAttributeAdapterProvider, CustomValidationAttributeAdapterProvider>();
            services.AddSingleton<IConfigureOptions<MvcOptions>, MvcConfiguration>();
            services.AddSingleton<IConfigureOptions<KeyManagementOptions>, ConfigureKeyManagementOptions>();
            services.AddSingleton<IXmlRepository, DataProtectionRepository>();

            services.AddTransient<IAppSettings>(x => x.GetService<IOptionsMonitor<AppSettings>>().CurrentValue);
            services.AddTransient<IEnvSettings>(x => x.GetService<IOptionsMonitor<EnvSettings>>().CurrentValue);
            services.AddTransient<IDBSettings>(x => x.GetService<IOptionsMonitor<DBSettings>>().CurrentValue);

            services.AddControllersWithViews()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(ErrorMessages));
                });
            services.AddDataProtection();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
