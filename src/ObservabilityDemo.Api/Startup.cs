using AspNetScaffolding.Extensions.Configuration;
using AspNetScaffolding.Extensions.RequestKey;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Mongo.CRUD;
using Mongo.CRUD.Models;
using ObservabilityDemo.Api.External;
using ObservabilityDemo.Api.Models.Request;
using ObservabilityDemo.Api.Models.Settings;
using ObservabilityDemo.LoggerSetup;
using RestSharp.Easy;
using RestSharp.Easy.Models;
using System;

namespace ObservabilityDemo.Api
{
    public static class Startup
    {
        public static void ConfigureHealthcheck(IHealthChecksBuilder builder, IServiceProvider provider)
        {
            var mongoConfiguration = provider.GetService<MongoConfiguration>();
            builder.AddMongoDb(mongoConfiguration.ConnectionString, mongoConfiguration.Database, name: "mongo_db", timeout: TimeSpan.FromSeconds(2));

            var internalApiSettings = provider.GetService<InternalApiSettings>();
            builder.AddUrlGroup(new Uri(internalApiSettings.Url), "internal_api", timeout: TimeSpan.FromSeconds(2));
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.SetupRepositories();
            services.SetupInternalApiClient();
            SerilogStartup.Init();
        }

        private static void SetupRepositories(this IServiceCollection services)
        {
            MongoCRUD.RegisterDefaultConventionPack(r => true);

            services.AddSingletonConfiguration<MongoConfiguration>("DbSettings");

            var dbSettings = services.BuildServiceProvider().GetService<MongoConfiguration>();

            services.AddSingleton<IMongoCRUD<TestRequest>>(new MongoCRUD<TestRequest>(dbSettings));
        }

        private static void SetupInternalApiClient(this IServiceCollection services)
        {
            services.AddSingletonConfiguration<InternalApiSettings>("InternalApiSettings");
            services.AddScoped<IInternalApiClient>(provider =>
            {
                var settings = provider.GetService<InternalApiSettings>();
                var requestKey = provider.GetService<RequestKey>().Value;

                var easyRestClient = new EasyRestClient(
                    baseUrl: settings.Url,
                    serializeStrategy: SerializeStrategyEnum.SnakeCase,
                    timeoutInMs: settings.TimeoutInSeconds * 1000,
                    requestKey: requestKey,
                    enableLog: true);

                return new InternalApiClient(easyRestClient);
            });
        }

        public static void Configure(IApplicationBuilder app)
        {
        }
    }
}
