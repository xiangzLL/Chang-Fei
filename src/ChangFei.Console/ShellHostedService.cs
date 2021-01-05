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
        private Task _execution;

        public ShellHostedService(IClusterClient client, IHost host)
        {
            _client = client;
            _host = host;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _execution = RunAsync();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task RunAsync()
        {
            ShowHelp(true);
            while (true)
            {
                var command = System.Console.ReadLine();
                if (command == "/help")
                {
                    ShowHelp();
                }
                else if (command == "/quit")
                {
                    await _host.StopAsync();
                }
                else if (command.StartsWith("/user"))
                {

                }
            }
        }

        private void ShowHelp(bool title = false)
        {
            if (title)
            {
                System.Console.WriteLine();
                System.Console.WriteLine("Welcome to the ChangFei!");
                System.Console.WriteLine("These are the available commands:");
            }
            System.Console.WriteLine("/help: Shows this list.");
        }
    }
}
