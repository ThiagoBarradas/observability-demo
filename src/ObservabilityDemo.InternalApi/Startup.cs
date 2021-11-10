using AspNetScaffolding.Models;
using System.Reflection;

namespace ObservabilityDemo.InternalApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ApiBasicConfiguration
            {
                ApiName = "Observabiity Demo Internal API ",
                ApiPort = 8701,
                EnvironmentVariablesPrefix = "InternalAPI_",
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
