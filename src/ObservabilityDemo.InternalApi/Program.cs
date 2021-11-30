using AspNetSerilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NewRelic.LogEnrichers.Serilog;
using ObservabilityDemo.LoggerSetup;
using Serilog;
using Serilog.Events;
using System;

namespace ObservabilityDemo.InternalApi
{
    public static class Startup
    {
        public static void ConfigureHealthcheck(IHealthChecksBuilder builder, IServiceProvider provider)
        {
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            SerilogStartup.Init();
        }

        public static void Configure(IApplicationBuilder app)
        {
        }
    }
}
