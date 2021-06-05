using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySqlConnector;
using System.IO;
using WaterTrans.Boilerplate.Application.Settings;
using WaterTrans.Boilerplate.Persistence;
using WaterTrans.Boilerplate.Web.Api;

namespace WaterTrans.Boilerplate.Tests
{
    [TestClass]
    public class TestEnvironment
    {
        public static WebApplicationFactory<Startup> WebApiFactory;
        public static DBSettings DBSettings { get; } = new DBSettings();

        [AssemblyInitialize]
        public static void Initialize(TestContext _)
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

            WebApiFactory = new WebApplicationFactory<Startup>()
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
