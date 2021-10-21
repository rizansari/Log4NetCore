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
                    loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    loggingBuilder.AddConsole();

                })
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()                
                .AddLog4NetCore("log4net.config")
                .CreateLogger<Program>();

            logger.LogDebug("debug");

            logger.LogDebug("debug param1:{0}, param2:{1}", "One", "Two");

            logger.LogInformation("information");
            
            logger.LogWarning("warning");

            logger.LogError(new Exception("exception message"), "error");

            Console.WriteLine("test");
        }
    }
}
