using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Nao_Discord_Bot.Commands.general
{
    public class Botinfo : ModuleBase<SocketCommandContext>
    {
        [Command("botinfo")]
        [Alias("info")]
        public async Task BotInfo()
        {
            var heap = Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString(CultureInfo.InvariantCulture);
            var embed = new EmbedBuilder();
            embed.WithTitle("Nao - Botinfo");
            embed.WithThumbnailUrl(Context.Client.CurrentUser.GetAvatarUrl());
            embed.WithDescription($"Nao is a multi functional Discord Bot written in C# with the Discord.NET library. The Main focus on Nao are the Moderation, Music and Waifu System.");
            embed.AddField($"Username", Context.Client.CurrentUser.Username, true);
            embed.AddField($"ID", (await Context.Client.GetApplicationInfoAsync()).Id, true);
            embed.AddField($"Servers", Context.Client.Guilds.Count, true);
            embed.AddField($"Users", Context.Client.Guilds.Select(x => x.Users.Count).Sum(),true);
            embed.AddField("Invite", "[Click here to invite Nao](https://discord.com/oauth2/authorize?client_id=742732203955454044&scope=bot&permissions=40)", true);
            embed.AddField("Support", "[Click here to join](https://discord.gg/xqTKXh3DBN)", true);
            embed.AddField("OS", Environment.OSVersion);
            embed.AddField("RAM", heap + "GB");

            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }
        [Command("help botinfo")]
        [Alias("help info")]
        public async Task helpService()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Botinfo Command");
            embed.WithColor(Color.Blue);
            embed.WithDescription("**Command**\nbotinfo \n\n **Alias**\ninfo \n\n **Description** \n Get infos about Nao! \n\n **Permissions** \n `No permissions needed!`");
            embed.WithTimestamp(DateTime.Now);

            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }
        
    }
}