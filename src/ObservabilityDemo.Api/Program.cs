using AspNetScaffolding.Models;
using System.Reflection;

namespace ObservabilityDemo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ApiBasicConfiguration
            {
                ApiName = "Observabiity Demo API ",
                ApiPort = 8700,
                EnvironmentVariablesPrefix = "API_",
                ConfigureHealthcheck = Startup.ConfigureHealthcheck,
                ConfigureServices = Startup.ConfigureServices,
                Configure = Startup.Configure,
                AutoRegisterAssemblies = new Assembly[]
                    { Assembly.GetExecutingAssembly() }
            };

            AspNetScaffolding.Api.Run(config);
        }
    }
}
