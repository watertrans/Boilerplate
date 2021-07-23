using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;
using MySqlConnector;
using System;
using System.Threading.Tasks;

namespace WaterTrans.Boilerplate.DBAdmin
{
    internal class Program
    {
        internal static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder().RunConsoleAppFrameworkAsync(args, new ConsoleAppOptions
            {
                StrictOption = true,
                ShowDefaultCommand = true,
            });
        }

        internal static string CreateConnectionString(string host, string database, string user, string password, uint port)
        {
            var builder = (MySqlConnectionStringBuilder)MySqlConnectorFactory.Instance.CreateConnectionStringBuilder();
            builder.Server = host;
            if (database != null)
            {
                builder.Database = database;
            }
            builder.UserID = user;
            builder.Password = password;
            builder.Port = (uint)port;
            return builder.ConnectionString;
        }

        internal static bool Confirm(string title)
        {
            Console.Write($"{title} [y/N] ");
            ConsoleKey response = Console.ReadKey(false).Key;
            Console.WriteLine();
            return response == ConsoleKey.Y;
        }
    }
}
