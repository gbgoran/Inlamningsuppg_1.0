using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;



// Inlämningsuppgift 1 - .Net EC-utbildning [Made by Göran Bäckström (gbgoran@outlook.com)]

namespace Inlamningsuppg_1._0 
{
    public class Program
    {
        public static void Main(string[] args)
        {
//  Set log and pathway for log file on a local Drive E:\
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.File(@"E:\workerservice\log\LogFile.txt")
            .CreateLogger();

            try
            {
                Log.Information("Starting WorkerService...");

                CreateHostBuilder(args).Build().Run();
                return;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"WorkerService terminated unexpectedly. Error:: {ex.Message}");
                return;
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
//  Using NuGet-packages to run as Service and create and write to log file                
                .UseSerilog()
                .UseWindowsService()
//  Use the Worker extension
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}
