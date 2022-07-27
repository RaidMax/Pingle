using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pingle.Connections.ICMP.Extensions;
using Pingle.Logic.Extensions;
using Serilog;
using Serilog.Core;

namespace Pingle.Bootstrapper;

public static class Builder
{
    private static Logger? _panicLogger;
    
    public static void WritePanicLog(Exception? exception, string message)
    {
        _panicLogger?.Fatal(exception, "Error: {Message}", message);
    }
    
    public static IServiceProvider BuildDependencies()
    {
        // logging
        var defaultLogger = new LoggerConfiguration()
            .WriteTo.Console()
            // todo: appsettings configuration based
            .WriteTo.File("Pingle.Application.log")
            .CreateLogger();

        _panicLogger = defaultLogger;
        
        // config
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build();
        
        // services
        var serviceCollection = new ServiceCollection()
            .WithICMPSupport(config)
            .AddQualityMonitoring(config);
        
        serviceCollection.AddLogging(builder =>
        {
            builder.AddSerilog(defaultLogger, true);
        });

        return serviceCollection.BuildServiceProvider();
    }
}
