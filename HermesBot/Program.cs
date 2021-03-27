using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace HermesBot
{
    class Program
    {
        private static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }
        
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _services;
        private readonly LogHandler _logHandler;
        private readonly CommandHandler _commandHandler;
        private RuntimeConfig _runtimeConfig;

        private Program()
        {
            _runtimeConfig = new RuntimeConfig();
            
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info,
                MessageCacheSize = 50,
                //WebSocketProvider = WS4NetProvider.Instance
            });
        
            _commands = new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Info,
                CaseSensitiveCommands = false,
            });

            _logHandler = new LogHandler(_client, _commands);
            _services = ConfigureServices(_client, _commands);
            _commandHandler = new CommandHandler(_client, _commands, _services);
            
            _client.SetStatusAsync(UserStatus.Online);
        }

        // If any services require the client, or the CommandService, or something else you keep on hand,
        // pass them as parameters into this method as needed.
        // If this method is getting pretty long, you can seperate it out into another file using partials.
        private static IServiceProvider ConfigureServices(DiscordSocketClient client, CommandService commands)
        {
            var map = new ServiceCollection()
                .AddSingleton(client)
                .AddSingleton(commands);
                // Repeat this for all the service classes
                // and other dependencies that your commands might need.
                // .AddSingleton(new NoUService());
            
            // When all your required services are in the collection, build the container.
            // Tip: There's an overload taking in a 'validateScopes' bool to make sure
            // you haven't made any mistakes in your dependency graph.
            return map.BuildServiceProvider();
        }

        private async Task MainAsync()
        {
            // Centralize the logic for commands into a separate method.
            await _commandHandler.InstallCommandsAsync();

            
            // const string token = "";
            // Some alternative options would be to keep your token in an Environment Variable or a standalone file.
            // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
            // var token = File.ReadAllText("token.txt");
            var token = _runtimeConfig.BotConfig.Token;
            
            // Login and connect.
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Wait infinitely so your bot actually stays connected.
            await Task.Delay(Timeout.Infinite);
        }
        
    }
}