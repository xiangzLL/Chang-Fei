using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Orleans;

namespace ChangFei.Console
{
    public class ShellHostedService:IHostedService
    {
        private readonly IClusterClient _client;
        private readonly IHost _host;

        public ShellHostedService(IClusterClient client, IHost host)
        {
            _client = client;
            _host = host;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            ShowHelp(true);
            while (true)
            {
                var command = System.Console.ReadLine();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private void ShowHelp(bool title = false)
        {

        }
    }
}
