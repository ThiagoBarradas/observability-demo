using NewRelic.LogEnrichers.Serilog;
using Serilog;
using Serilog.Events;
using System;

namespace ObservabilityDemo.LoggerSetup
{
    public class SerilogStartup
    {
        public static void Init ()
        {
            var minLogLevel = LogEventLevel.Verbose;
            var logger = new LoggerConfiguration();

            logger.Enrich.FromLogContext();
            logger.Enrich.WithThreadName();
            logger.Enrich.WithThreadId();
            logger.Enrich.WithMachineName();
            logger.Enrich.WithEnvironmentUserName();
            logger.Enrich.WithProperty("Application", Environment.GetEnvironmentVariable("NEW_RELIC_APP_NAME"));
            logger.Enrich.WithProperty("Domain", "Observability Demo");
            logger.Enrich.WithNewRelicLogsInContext();

            logger.WriteTo.NewRelicLogs(
                    applicationName: Environment.GetEnvironmentVariable("NEW_RELIC_APP_NAME"),
                    licenseKey: Environment.GetEnvironmentVariable("NEW_RELIC_LICENSE_KEY"),
                    restrictedToMinimumLevel: minLogLevel);

            logger.WriteTo.Console(restrictedToMinimumLevel: minLogLevel);

            Log.Logger = logger.CreateLogger();
        }
    }
}
