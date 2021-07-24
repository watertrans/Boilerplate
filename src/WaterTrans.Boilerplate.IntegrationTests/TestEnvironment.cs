using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySqlConnector;
using System.IO;
using WaterTrans.Boilerplate.Application.Settings;
using WaterTrans.Boilerplate.CrossCuttingConcerns.Abstractions.OS;
using WaterTrans.Boilerplate.Infrastructure.OS;
using WaterTrans.Boilerplate.Persistence;

namespace WaterTrans.Boilerplate.IntegrationTests
{
    [TestClass]
    public class TestEnvironment
    {
        public static WebApplicationFactory<WaterTrans.Boilerplate.Web.Api.Startup> WebApiFactory { get; private set; }
        public static WebApplicationFactory<WaterTrans.Boilerplate.Web.Server.Startup> WebServerFactory { get; private set; }
        public static DBSettings DBSettings { get; } = new DBSettings();
        public static IDateTimeProvider DateTimeProvider { get; } = new DateTimeProvider();

        [AssemblyInitialize]
        public static void Initialize(TestContext testContext)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testsettings.json");

            var configuration = builder.Build();
            configuration.GetSection("DBSettings").Bind(DBSettings);
            DBSettings.SqlProviderFactory = MySqlConnectorFactory.Instance;

            DataConfiguration.Initialize();
            var setup = new DataSetup(DBSettings);
            setup.Initialize();
            setup.LoadTestData();

            /*
             * [Note]
             * Coverage report cannot be generated correctly when multiple Test Servers are started.
             * We work around this issue by filtering with TestCategory.
             */
            WebApiFactory = new WebApplicationFactory<WaterTrans.Boilerplate.Web.Api.Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.Configure<DBSettings>(options =>
                        {
                            options.StorageConnectionString = DBSettings.StorageConnectionString;
                            options.SqlConnectionString = DBSettings.SqlConnectionString;
                            options.ReplicaSqlConnectionString = DBSettings.ReplicaSqlConnectionString;
                        });
                    });
                });

            /*
             * [Note]
             * Coverage report cannot be generated correctly when multiple Test Servers are started.
             * We work around this issue by filtering with TestCategory.
             */
            WebServerFactory = new WebApplicationFactory<WaterTrans.Boilerplate.Web.Server.Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.Configure<DBSettings>(options =>
                        {
                            options.StorageConnectionString = DBSettings.StorageConnectionString;
                            options.SqlConnectionString = DBSettings.SqlConnectionString;
                            options.ReplicaSqlConnectionString = DBSettings.ReplicaSqlConnectionString;
                        });
                    });
                });
        }

        [AssemblyCleanup]
        public static void Cleanup()
        {
            var setup = new DataSetup(DBSettings);
            setup.Cleanup();
        }
    }
}
