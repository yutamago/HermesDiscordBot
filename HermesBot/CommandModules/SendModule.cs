using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace HermesBot
{
    public class SendModule : ModuleBase<SocketCommandContext>
    {
        public DiscordSocketClient _client { get; set; }

        public SendModule(DiscordSocketClient client)
        {
            _client = client;
        }

        [Command("send")]
        [Summary("Sends an anonymous message in a specified channel.")]
        [Alias("sent")]
        public async Task Send(
            [Summary("The channel to send the message to.")] ISocketMessageChannel receivingChannel,
            [Remainder] [Summary("The message to send.")] string message)
        {
            await receivingChannel.SendMessageAsync(
                Context.Channel.Name + ": " + message,
                false,
                null,
                null,
                new AllowedMentions
                {
                    AllowedTypes = AllowedMentionTypes.Users
                },
                null);

            await Context.Message.AddReactionAsync(new Emoji("📨"));
        }
        
        [Command("secret")]
        [Summary("Sends an anonymous message in a specified channel.")]
        public async Task SendSecret(
            [Summary("The channel to send the message to.")] ISocketMessageChannel receivingChannel,
            [Remainder] [Summary("The message to send.")] string message)
        {
            await receivingChannel.SendMessageAsync(
                message,
                false,
                null,
                null,
                new AllowedMentions
                {
                    AllowedTypes = AllowedMentionTypes.Users
                },
                null);

            await Context.Message.AddReactionAsync(new Emoji("📨"));
            await Context.Message.AddReactionAsync(new Emoji("🕵️"));
        }
    }
}