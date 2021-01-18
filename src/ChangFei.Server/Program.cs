using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Orleans;
using Orleans.ApplicationParts;
using Orleans.Hosting;

namespace ChangFei.Silo
{
    class Program
    {
        static Task Main(string[] args)
        {
            Console.Title = nameof(Silo);
            return new HostBuilder()
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices(ConfigureServices)
                .UseOrleans((context,builder) => builder
                    .UseLocalhostClustering()
                    .ConfigureApplicationParts(ConfigureApplicationParts)
                    .UseMongoDBClient(context.Configuration.GetSection("persistenceOptions")["connectionString"])
                    .AddMongoDBGrainStorage("MessageStore", options =>
                    {
                        options.DatabaseName = context.Configuration.GetSection("persistenceOptions")["databaseName"];
                        options.ConfigureJsonSerializerSettings = settings =>
                        {
                            settings.NullValueHandling = NullValueHandling.Include;
                            settings.ObjectCreationHandling = ObjectCreationHandling.Replace;
                            settings.DefaultValueHandling = DefaultValueHandling.Populate;
                        };
                    }))
                .ConfigureLogging(builder =>builder
                    .AddFilter("Orleans.Runtime.Management.ManagementGrain", LogLevel.Warning)
                    .AddFilter("Orleans.Runtime.SiloControl", LogLevel.Warning)
                    .AddConsole())
                .RunConsoleAsync();
        }

        private static void ConfigureApplicationParts(IApplicationPartManager parts)
        {
            parts.AddFromApplicationBaseDirectory().WithReferences();
        }

        private static void ConfigureAppConfiguration(IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, false);
        }

        private static void ConfigureServices(Microsoft.Extensions.Hosting.HostBuilderContext context, IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<PersistenceOptions>(context.Configuration.GetSection("persistenceOptions"));
            services.Configure<ServerOptions>(context.Configuration.GetSection("serverOptions"));
        }
    }
}
