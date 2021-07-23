using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
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
using Microsoft.OpenApi.Models;
using MySqlConnector;
using System;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using WaterTrans.Boilerplate.Application.Abstractions.UseCases;
using WaterTrans.Boilerplate.Application.Settings;
using WaterTrans.Boilerplate.Application.UseCases;
using WaterTrans.Boilerplate.CrossCuttingConcerns.Abstractions.Cryptography;
using WaterTrans.Boilerplate.CrossCuttingConcerns.Abstractions.OS;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.Abstractions.QueryServices;
using WaterTrans.Boilerplate.Domain.Abstractions.Repositories;
using WaterTrans.Boilerplate.Domain.Abstractions.Services;
using WaterTrans.Boilerplate.Domain.Services;
using WaterTrans.Boilerplate.Infrastructure.Cryptography;
using WaterTrans.Boilerplate.Infrastructure.OS;
using WaterTrans.Boilerplate.Persistence.QueryServices;
using WaterTrans.Boilerplate.Persistence.Repositories;
using WaterTrans.Boilerplate.Web.Api.Filters;
using WaterTrans.Boilerplate.Web.Api.ObjectResults;
using WaterTrans.Boilerplate.Web.Api.Security;
using WaterTrans.Boilerplate.Web.AttributeAdapters;
using WaterTrans.Boilerplate.Web.Resources;

namespace WaterTrans.Boilerplate.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            WebHostEnvironment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }
        public IServiceProvider ServiceProvider { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
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
            services.AddSingleton<IConfigureOptions<MvcOptions>, ModelBindingMessageConfiguration>();
            services.AddSingleton<IConfigureOptions<KeyManagementOptions>, KeyManagementConfiguration>();
            services.AddSingleton<IXmlRepository, DataProtectionRepository>();

            services.AddTransient<IAppSettings>(x => x.GetService<IOptionsMonitor<AppSettings>>().CurrentValue);
            services.AddTransient<IEnvSettings>(x => x.GetService<IOptionsMonitor<EnvSettings>>().CurrentValue);
            services.AddTransient<IDBSettings>(x => x.GetService<IOptionsMonitor<DBSettings>>().CurrentValue);

            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IPasswordHashProvider, PasswordHashProvider>();

            services.AddTransient<IAccessTokenRepository, AccessTokenRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IApplicationRepository, ApplicationRepository>();
            services.AddTransient<IAuthorizationCodeRepository, AuthorizationCodeRepository>();
            services.AddTransient<IAuthorizeService, AuthorizeService>();
            services.AddTransient<IForecastQueryService, ForecastQueryService>();
            services.AddTransient<IForecastRepository, ForecastRepository>();
            services.AddTransient<IForecastService, ForecastService>();
            services.AddTransient<IForecastUseCase, ForecastUseCase>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddTransient<ITokenUseCase, TokenUseCase>();

            if (!WebHostEnvironment.IsProduction())
            {
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(builder =>
                    {
                        builder.WithOrigins("https://editor.swagger.io")
                            .AllowCredentials()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
                });
            }

            services.AddAuthentication(BearerAuthenticationHandler.SchemeName)
                .AddScheme<AuthenticationSchemeOptions, BearerAuthenticationHandler>(BearerAuthenticationHandler.SchemeName, null);

            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.SwaggerGeneratorOptions.DescribeAllParametersInCamelCase = true;
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "WaterTrans.Boilerplate.Web", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        },
                        new string[] { }
                    },
                });
            });
            services.AddApiVersioning();
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddControllers(options =>
                {
                    options.Filters.Add(typeof(CatchAllExceptionFilter));
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        return ErrorObjectResultFactory.ValidationError(context.ModelState);
                    };
                })
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

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = string.Empty;
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "WaterTrans.Boilerplate.Web v1");
            });
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
