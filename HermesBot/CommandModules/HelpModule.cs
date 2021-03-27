using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Discord.Commands;

namespace HermesBot
{
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        public CommandService _commands { get; set; }
        
        [Command("help")]
        [Summary("")]
        public async Task Help([Optional] string cmd)
        {
            var reply = "Commands:";
            foreach (var command in _commands.Commands)
            {
                reply += "\n`" + command.Name;
                foreach (var parameter in command.Parameters)
                {
                    reply += " [" + parameter.Name + "]";
                }
                reply += "`";
                
                if(command.Aliases.Count > 1)
                {
                    reply += " (Aliases: "
                             + string.Join(", ", 
                                 command.Aliases
                                     .Where(x => x != command.Name)
                                     .Select(x => "`" + x + "`")) + ")";
                }

                reply += "\n   __Summary__: " + command.Summary;
                if (command.Parameters.Any())
                {
                    reply += "\n   __Parameters__:";
                    foreach (var parameter in command.Parameters)
                    {
                        reply += "\n   - `" + parameter.Name + "`" + (parameter.IsOptional ? " (optional)" : "") + ": ";
                        reply += parameter.Summary;
                    }
                }

                reply += "\n";

            }
            await ReplyAsync(reply);
        }

    }
}