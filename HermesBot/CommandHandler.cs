using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace HermesBot
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _services;

        // Retrieve client and CommandService instance via ctor
        public CommandHandler(DiscordSocketClient client, CommandService commands, IServiceProvider serviceProvider)
        {
            _commands = commands;
            _client = client;
            _services = serviceProvider;
        }

        public async Task InstallCommandsAsync()
        {
            await _commands.AddModulesAsync(assembly:
                Assembly.GetEntryAssembly(),
                services: _services);
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            // Bail out if it's a System Message.
            if (!(messageParam is SocketUserMessage message)) return;

            // We don't want the bot to respond to itself 
            if (message.Author.Id == _client.CurrentUser.Id || message.Author.IsBot) return;

            // Create a number to track where the prefix ends and the command begins
            var argPos = 0;

            var msgString = message.ToString();


            // Create a WebSocket-based command context based on the message
            var context = new SocketCommandContext(_client, message);


            // Determine if the message is a command based on the prefix and make sure no bots trigger commands
            if (!(message.HasMentionPrefix(_client.CurrentUser, ref argPos)) &&
                !(message.HasCharPrefix('!', ref argPos)))
                return;


            // Execute the command with the command context we just
            // created, along with the service provider for precondition checks.
            await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: _services);
        }
    }
}