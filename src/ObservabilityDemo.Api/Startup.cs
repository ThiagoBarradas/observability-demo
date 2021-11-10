using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ObservabilityDemo.Api
{
    public static class Startup
    {
        public static void ConfigureHealthcheck(IHealthChecksBuilder builder, IServiceProvider provider)
        {
        }

        public static void ConfigureServices(IServiceCollection services)
        {
        }

        public static void Configure(IApplicationBuilder app)
        {
        }
    }
}
