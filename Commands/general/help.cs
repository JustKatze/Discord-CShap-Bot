using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Nao_Discord_Bot.Commands.general
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task help()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle($"Nao - Help");
            embed.WithColor(Color.Blue);
            embed.WithTimestamp(DateTime.Now);
            embed.AddField($"General", $"`n+help`");
            embed.AddField($"Utilities", $"`n+serverinfo`, `n+userinfo`");
            embed.AddField($"Moderation", $"`n+ban`, n+kick");

            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }
        
    }
    
}