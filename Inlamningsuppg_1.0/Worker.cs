using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// Inlämningsuppgift 1 - .Net EC-utbildning [Made by Göran Bäckström (gbgoran@outlook.com)]
  
namespace Inlamningsuppg_1._0
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        // Start and stop 
        public override Task StartAsync(CancellationToken cancellationToken)
        {
           _logger.LogInformation("The service has been started.");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("The service has been stopped.");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        { 



        
            while (!stoppingToken.IsCancellationRequested)
            {
 // Code for random numbers for a temperature, between 1 to 200 degrees.

                int numberTemperature;
                Random random = new Random();
                numberTemperature = random.Next(1, 200);

                try
                {
                   if (numberTemperature < 100) // The number betweenn 1 - 100 degrees, means it is OK, otherwise it is to high.
                         _logger.LogInformation($"========>The temperature is ({numberTemperature}). ********> Water is NOT boiling!");
                   else
                        _logger.LogInformation($"========>The temperature is ({numberTemperature}) ========> This is to HIGH. Water is boiling");
                }
            // Check for errors...
                catch (Exception ex)
                {
                    _logger.LogInformation($"Failed. The program has FAILED. Please contact support! - {ex.Message}");
                }


// Sleep 5 seconds.
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(5*1000, stoppingToken);
            }
        }
    }
}
