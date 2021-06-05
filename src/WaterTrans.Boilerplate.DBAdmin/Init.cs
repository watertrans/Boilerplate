using ConsoleAppFramework;
using MySqlConnector;
using System;
using WaterTrans.Boilerplate.Application.Settings;
using WaterTrans.Boilerplate.Persistence;

namespace WaterTrans.Boilerplate.DBAdmin
{
    public class Init : ConsoleAppBase
    {
        [Command("server", "Initialize your server.")]
        public int Server(
            [Option("h", "Connect to the server on the given host.")] string host = "localhost",
            [Option("u", "The user name of the account to use for connecting to the server.")] string user = "root",
            [Option("p", "The password of the account use for connecting to the server.")] string password = "root",
            [Option(null, "The port number to use.")] uint port = 3306,
            [Option("s", "No prompt for confirmation.")] bool silent = false)
        {
            if (!silent && !Program.Confirm("Are you sure you want to initialize server?"))
            {
                Console.Error.WriteLine("You have chosen to cancel.");
                return 1;
            }

            var dbSettings = new DBSettings();
            dbSettings.SqlProviderFactory = MySqlConnectorFactory.Instance;
            dbSettings.SqlConnectionString = Program.CreateConnectionString(host, null, user, password, port);

            DataConfiguration.Initialize();

            try
            {
                var setup = new DataSetup(dbSettings);

                Console.Write("Creating database...");
                setup.CreateDatabase();
                Console.WriteLine("done");


                Console.Write("Creating test database...");
                setup.CreateTestDatabase();
                Console.WriteLine("done");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return 1;
            }

            return 0;
        }

        [Command("database", "Initialize your database.")]
        public int Database(
            [Option("h", "Connect to the server on the given host.")] string host = "localhost",
            [Option("D", "The database to use.")] string database = "Boilerplate",
            [Option("u", "The user name of the account to use for connecting to the server.")] string user = "user",
            [Option("p", "The password of the account use for connecting to the server.")] string password = "user",
            [Option(null, "The port number to use.")] uint port = 3306,
            [Option("s", "No prompt for confirmation.")] bool silent = false)
        {
            if (!silent && !Program.Confirm("The current database will be permanently deleted. Are you sure you want to initialize database?"))
            {
                Console.Error.WriteLine("You have chosen to cancel.");
                return 1;
            }

            var dbSettings = new DBSettings();
            dbSettings.SqlProviderFactory = MySqlConnectorFactory.Instance;
            dbSettings.SqlConnectionString = Program.CreateConnectionString(host, database, user, password, port);

            DataConfiguration.Initialize();

            try
            {
                var setup = new DataSetup(dbSettings);

                Console.Write("Cleaning up...");
                setup.Cleanup();
                Console.WriteLine("done");

                Console.Write("Initializing database...");
                setup.Initialize();
                Console.WriteLine("done");

                Console.Write("Creating tables...");
                setup.CreateTables();
                Console.WriteLine("done");

                Console.Write("Loading initial data...");
                setup.LoadInitialData();
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
