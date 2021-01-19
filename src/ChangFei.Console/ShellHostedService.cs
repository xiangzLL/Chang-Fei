using System;
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
        private string _targetUserId;

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
                else if (command == "/exit")
                {
                    System.Console.WriteLine($"Exit chat with {_targetUserId} =====================================================");
                    _targetUserId = string.Empty;
                    System.Console.ForegroundColor = ConsoleColor.White;
                }
                else if (command.StartsWith("/chat"))
                {
                    if (!IsLogin())
                    {
                        System.Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("You need to login first");
                        System.Console.ForegroundColor = ConsoleColor.White;
                    }
                    var match = Regex.Match(command, @"/chat (?<userId>\w{1,100})");
                    if (match.Success)
                    {
                        _targetUserId = match.Groups["userId"].Value;
                        System.Console.WriteLine($"Start chat with {_targetUserId} =====================================================");
                    }
                }
                else if (command.StartsWith("/login"))
                {
                    if (IsLogin())
                    {
                        System.Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("You have already login");
                        System.Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
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
                        System.Console.Title = _userId;
                        System.Console.WriteLine($"The current user is now [{_userId}]");
                    }
                }
                else
                {
                    if (IsInChat())
                    {
                        await _userGrain.SendMessageAsync(Message.CreateText(_userId, _targetUserId, command));
                        System.Console.ForegroundColor = ConsoleColor.Yellow;
                        System.Console.WriteLine($"{_userId}: {command}");
                        System.Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }

        private bool IsLogin()
        {
            if (string.IsNullOrEmpty(_userId))
            {
                return false;
            }

            return true;
        }

        private bool IsInChat()
        {
            if (string.IsNullOrEmpty(_targetUserId))
            {
                return false;
            }

            return true;
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
            System.Console.WriteLine("/login <userId>: Login a active account.");
            System.Console.WriteLine("/logout: Logout current account.");
            System.Console.WriteLine("/chat <userId>: Start chat with account.");
            System.Console.WriteLine("/exit: Exit account chat.");
        }
    }
}
