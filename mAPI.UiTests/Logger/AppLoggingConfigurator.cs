using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace mAPI.UiTests.Logger;

internal static class AppLoggingConfigurator
{
    public static void AddAppLogging(this ILoggingBuilder loggingBuilder)
    {
        var config = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                                               .AddJsonFile("loggingConfig.json", optional: false, reloadOnChange: true)
                                               .Build();

        loggingBuilder.AddAppLogging(config);
    }

    public static ILoggingBuilder AddAppLogging(this ILoggingBuilder loggingBuilder, IConfiguration configuration)
    {
        loggingBuilder.AddSerilog(CreateLogger(configuration), dispose: true);
        return loggingBuilder;
    }

    private static Serilog.ILogger CreateLogger(IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.With<ClassNameLogEventEnricher>()
            .CreateLogger();

        return Log.Logger;
    }
}