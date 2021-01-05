using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ChangFei.Core.Message;
using ChangFei.Interfaces;
using ChangFei.Interfaces.Grains;
using Microsoft.Extensions.Hosting;
using Orleans;

namespace ChangFei.Console
{
    public class ShellHostedService:IHostedService
    {
        private readonly IClusterClient _client;
        private readonly IHost _host;
        private Task _execution;
        private IUserGrain _userGrain;
        private IMessageViewer _viewer;
        private string _userId;

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
                if (command == null)
                {
                    continue;
                }
                if (command == "/help")
                {
                    ShowHelp();
                }
                else if (command == "/quit")
                {
                    await _host.StopAsync();
                }
                else if (command.StartsWith("/login"))
                {
                    var match = Regex.Match(command, @"/login (?<userId>\w{1,100})");
                    if (match.Success)
                    {
                        _userId = match.Groups["userId"].Value;
                        _userGrain = _client.GetGrain<IUserGrain>(_userId);
                        if (_viewer == null)
                        {
                            _viewer = await _client.CreateObjectReference<IMessageViewer>(new MessageConsoleViewer());
                        }

                        await _userGrain.LoginAsync(_viewer);
                        System.Console.WriteLine($"The current user is now [{_userId}]");
                    }
                }
                else if (command.StartsWith("/send"))
                {
                    var match = Regex.Match(command, @"/send (?<userId>\w{1,100})");
                    if (match.Success)
                    {
                        var targetUserId = match.Groups["userId"].Value;
                       
                        await _userGrain.SendMessageAsync(new TextMessage(_userId,targetUserId));
                    }
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
