using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Nao_Discord_Bot.Commands.Utilities
{
    public class Serverinfo : ModuleBase<SocketCommandContext>
    {
        [Command("serverinfo")]
        [Alias("si")]
        public async Task serverinfo()
        {
            var rolelist = Context.Guild.Roles.ToList();
            string Roles = "";
            rolelist.Sort();
            rolelist.Reverse();
            rolelist.ForEach(x => Roles += x.Mention + ", ");
            Roles = Roles.Remove(Roles.Length - 2);
            var embed = new EmbedBuilder();
            embed.WithTitle(Context.Guild.Name);
            embed.WithColor(Color.Blue);
            embed.WithThumbnailUrl(Context.Guild.IconUrl);
            embed.AddField("Name", Context.Guild.Name, true);
            embed.AddField("ID", Context.Guild.Id, true);
            embed.AddField("Region", Context.Guild.VoiceRegionId, true);
            embed.AddField("Channel", $"`Text channels` {Context.Guild.TextChannels.Count}\n`Voice channels` {Context.Guild.VoiceChannels.Count}", true);
            embed.AddField("Creation Date", Context.Guild.CreatedAt.DateTime, true);
            embed.AddField("Verification Level", Context.Guild.VerificationLevel, true);
            //embed.AddField("Owner", Context.Guild.Owner.Username, true);
            if (rolelist.Count >= 1000)
            {
                embed.AddField("Roles", $"`Roles` {Context.Guild.Roles.Count} \n To much roles to display!");
            }
            else
            {
                embed.AddField("Roles", $"`Roles` {Context.Guild.Roles.Count} \n {Roles}");  
            }


            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }

        [Command("help serverinfo")]
        [Alias("help si")]
        public async Task helpService()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Serverinfo Command");
            embed.WithColor(Color.Blue);
            embed.WithDescription("**Command**\nserverinfo \n\n **Alias**\nsi \n\n **Description** \n Get infos about the server you are currently chatting on! \n\n **Permissions** \n `No permissions needed!`");
            embed.WithTimestamp(DateTime.Now);

            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }
        
    }
}