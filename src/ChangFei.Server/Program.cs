using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
                .UseOrleans(builder => builder
                    .UseLocalhostClustering()
                    .ConfigureApplicationParts(ConfigureApplicationParts)
                    .AddMemoryGrainStorageAsDefault()
                    .AddMemoryGrainStorage("MessageStore"))
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
    }
}
