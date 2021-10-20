using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Log4NetCore
{
    public class Log4NetCoreProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, Log4NetCoreLogger> loggers = new ConcurrentDictionary<string, Log4NetCoreLogger>();

        private bool disposedValue = false;

        private ILoggerRepository loggerRepository;

        public Log4NetCoreProvider()
        {
            this.loggerRepository = LogManager.GetRepository();
            string fileNamePath  = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "log4net.config");
            var configXml = ParseConfig(fileNamePath);
            XmlConfigurator.Configure(this.loggerRepository, configXml.DocumentElement);
        }

        public Log4NetCoreProvider(string configFile)
        {
            this.loggerRepository = LogManager.GetRepository();
            string fileNamePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), configFile);
            var configXml = ParseConfig(fileNamePath);
            XmlConfigurator.Configure(this.loggerRepository, configXml.DocumentElement);
        }

        private static XmlDocument ParseConfig(string filename)
        {
            using (FileStream stream = File.OpenRead(filename))
            {
                var settings = new XmlReaderSettings
                {
                    DtdProcessing = DtdProcessing.Prohibit
                };

                var log4netConfig = new XmlDocument();
                using (var reader = XmlReader.Create(stream, settings))
                {
                    log4netConfig.Load(reader);
                }

                return log4netConfig;
            }
        }

        public ILogger CreateLogger(string name)
        {
            return loggers.GetOrAdd(name, CreateLoggerImplementation);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.loggerRepository.Shutdown();
                    this.loggers.Clear();
                }

                disposedValue = true;
            }
        }

        private Log4NetCoreLogger CreateLoggerImplementation(string name)
        {
            return new Log4NetCoreLogger(name);
        }
    }
}
