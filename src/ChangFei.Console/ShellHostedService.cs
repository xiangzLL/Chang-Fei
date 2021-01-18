﻿using System;
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
        private bool _inChat;

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
                    _targetUserId = string.Empty;
                    _inChat = false;
                    System.Console.WriteLine($"=====================================================");
                }
                else if (command == "/chat")
                {
                    var match = Regex.Match(command, @"/login (?<userId>\w{1,100})");
                    if (match.Success)
                    {
                        _targetUserId = match.Groups["userId"].Value;
                        _inChat = true;
                        System.Console.WriteLine($"=====================================================");
                    }
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
                        var unReadMessages = await _userGrain.GetUnReadMessages(100);
                        System.Console.WriteLine($"Receive offline messages: {unReadMessages.Count}");
                        foreach (var message in unReadMessages)
                        {
                            System.Console.WriteLine($"Receive {message.TargetId}: {message.Content}");
                        }
                    }
                }
                else if (command.StartsWith("/send"))
                {
                    var match = Regex.Match(command, @"/send (?<message>.+)");
                    if (match.Success)
                    {
                        var message = match.Groups["message"].Value;
                        await _userGrain.SendMessageAsync(new TextMessage(_userId, _targetUserId, message));
                        System.Console.ForegroundColor = ConsoleColor.Blue;
                        System.Console.WriteLine($"{_userId}: {message}");
                    }
                    else
                    {
                        System.Console.WriteLine("Invalid send. Try again or type /help for a list of commands.");
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
            System.Console.WriteLine("/login <userId>: Login a active account.");
            System.Console.WriteLine("/chat <userId>: Start chat with account.");
            System.Console.WriteLine("/exit: Exit account chat.");
            System.Console.WriteLine("/send <message>: Send a message to the active account.");
        }
    }
}
