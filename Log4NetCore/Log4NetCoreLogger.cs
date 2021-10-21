using log4net;
using log4net.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Log4NetCore
{
    public class Log4NetCoreLogger : Microsoft.Extensions.Logging.ILogger
    {
        private readonly log4net.Core.ILogger logger;

        public Log4NetCoreLogger()
        {
            this.logger = LogManager.GetLogger("default").Logger;
        }

        public Log4NetCoreLogger(string name)
        {
            this.logger = LogManager.GetLogger(name).Logger;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            //throw new NotImplementedException();
            //return ;
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //throw new NotImplementedException();
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Type callerStackBoundaryDeclaringType = typeof(LoggerExtensions);
            string message = state.ToString();
            
            Level logLevelLocal = Level.Debug;
            switch (logLevel)
            {
                case LogLevel.Debug:
                    logLevelLocal = Level.Debug;
                    break;

                case LogLevel.Information:
                    logLevelLocal = Level.Info;
                    break;

                case LogLevel.Error:
                    logLevelLocal = Level.Error;
                    break;

                case LogLevel.Warning:
                    logLevelLocal = Level.Warn;
                    break;

                case LogLevel.Critical:
                    logLevelLocal = Level.Critical;
                    break;

                case LogLevel.Trace:
                    logLevelLocal = Level.Verbose;
                    break;

                case LogLevel.None:
                    logLevelLocal = Level.All;
                    return;
            }

            var temp = new LoggingEvent(
                callerStackBoundaryDeclaringType: callerStackBoundaryDeclaringType,
                repository: logger.Repository,
                loggerName: logger.Name,
                level: logLevelLocal,
                message: message,
                exception: exception);

            this.logger.Log(temp);
        }
    }
}
