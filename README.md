# Log4NetCore library to easily integrate log4net in .net core applications

Library Version: v1.0.3

now support .net5.0

## Installation

```powershell
Install-Package Log4NetCoreEx
```

## Using the library

*Logging Builder*
```
var serviceProvider = new ServiceCollection()
    .AddLogging(loggingBuilder =>
    {
        loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        loggingBuilder.AddLog4Net();
        loggingBuilder.AddConsole();

    })
    .BuildServiceProvider();
```

*Services Collection*
```
var logger = serviceProvider.GetService<ILoggerFactory>()                
  .AddLog4NetCore("log4net.config")
  .CreateLogger<Program>();
```
