using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Log4NetCore
{
    public static class Log4NetCoreExtensions
    {
        public static ILoggerFactory AddLog4NetCore(this ILoggerFactory factory)
        {
            factory.AddProvider(new Log4NetCoreProvider());
            return factory;
        }

        public static ILoggerFactory AddLog4NetCore(this ILoggerFactory factory, string configFile)
        {
            factory.AddProvider(new Log4NetCoreProvider(configFile));
            return factory;
        }
    }
}
