using Log4NetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Log4NetCoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.AddConsole(opt => opt.LogToStandardErrorThreshold = LogLevel.Debug);
                    
                })
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .AddLog4NetCore("log4net.config")
                .CreateLogger<Program>();

            logger.LogInformation("Starting application");
            
            logger.LogWarning("All done!");

            logger.LogInformation("Starting application");

            Console.WriteLine("test");
        }
    }
}
