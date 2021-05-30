using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WaterTrans.Boilerplate.Persistence;

namespace WaterTrans.Boilerplate.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DataConfiguration.Initialize();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
