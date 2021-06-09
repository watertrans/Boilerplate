using ConsoleAppFramework;
using MySqlConnector;
using System;
using WaterTrans.Boilerplate.Application.Settings;
using WaterTrans.Boilerplate.Persistence;

namespace WaterTrans.Boilerplate.DBAdmin
{
    public class Apply : ConsoleAppBase
    {
        [Command("database", "Create schemas if it does not exist.")]
        public int Database(
            [Option("h", "Connect to the server on the given host.")] string host = "localhost",
            [Option("D", "The database to use.")] string database = "Boilerplate",
            [Option("u", "The user name of the account to use for connecting to the server.")] string user = "user",
            [Option("p", "The password of the account use for connecting to the server.")] string password = "user",
            [Option(null, "The port number to use.")] uint port = 3306)
        {

            var dbSettings = new DBSettings();
            dbSettings.SqlProviderFactory = MySqlConnectorFactory.Instance;
            dbSettings.SqlConnectionString = Program.CreateConnectionString(host, database, user, password, port);

            DataConfiguration.Initialize();

            try
            {
                var setup = new DataSetup(dbSettings);

                Console.Write("Creating schemas that do not exist....");
                setup.CreateTables();
                Console.WriteLine("done");

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return 1;
            }

            return 0;
        }
    }
}
