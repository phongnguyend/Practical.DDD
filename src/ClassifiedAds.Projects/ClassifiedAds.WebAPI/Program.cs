using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace ClassifiedAds.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    //logging.AddEventLog(new EventLogSettings
                    //{
                    //    LogName = "ClassifiedAds",
                    //    SourceName = "WebAPI",
                    //    Filter = (a, b) => b >= LogLevel.Information
                    //});
                })
                .UseSerilog();
    }
}
