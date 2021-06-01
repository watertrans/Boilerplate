using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Reflection;
using WaterTrans.Boilerplate.Domain.Abstractions;

namespace WaterTrans.Boilerplate.Persistence
{
    public class DataSetup
    {
        private readonly IDBSettings _dbSettings;
        private readonly IDbConnection _connection;
        private readonly DbProviderFactory _factory;
        private readonly Dictionary<string, string> _resources;

        public DataSetup(IDBSettings dbSettings)
        {
            _dbSettings = dbSettings;
            _factory = _dbSettings.SqlProviderFactory;
            _connection = _factory.CreateConnection();
            _connection.ConnectionString = _dbSettings.SqlConnectionString;
            _resources = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            var asm = Assembly.GetExecutingAssembly();
            foreach (string name in asm.GetManifestResourceNames())
            {
                using (var stream = asm.GetManifestResourceStream(name))
                using (var sr = new StreamReader(stream))
                {
                    _resources[name.Replace(asm.GetName().Name + ".SqlResources.", string.Empty)] = sr.ReadToEnd();
                }
            }
        }

        public void Initialize()
        {
            CreateTables();
        }

        public void Cleanup()
        {
            DropTables();
        }

        public void CreateDatabase()
        {
            _connection.Execute(_resources["CreateDatabase.sql"]);
        }

        public void CreateTestDatabase()
        {
            _connection.Execute(_resources["CreateTestDatabse.sql"]);
        }

        public void CreateTables()
        {
            _connection.Execute(_resources["CreateAccessToken.sql"]);
            _connection.Execute(_resources["CreateAccount.sql"]);
            _connection.Execute(_resources["CreateApplication.sql"]);
            _connection.Execute(_resources["CreateAuthorizationCode.sql"]);
            _connection.Execute(_resources["CreateForecast.sql"]);
        }

        public void LoadInitialData()
        {
            _connection.Execute(_resources["LoadInitialData.sql"]);
        }

        public void LoadTestData()
        {
            _connection.Execute(_resources["LoadTestData.sql"]);
        }

        public void DropTables()
        {
            _connection.Execute("DROP TABLE IF EXISTS `AccessToken`");
            _connection.Execute("DROP TABLE IF EXISTS `Account`");
            _connection.Execute("DROP TABLE IF EXISTS `Application`");
            _connection.Execute("DROP TABLE IF EXISTS `AuthorizationCode`");
            _connection.Execute("DROP TABLE IF EXISTS `Forecast`");
        }
    }
}
