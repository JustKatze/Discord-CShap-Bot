using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Nao_Discord_Bot.Commands.moderation
{
    public class Kick : ModuleBase<SocketCommandContext>
    {
        [Command("kick")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task kick(SocketGuildUser kickuser = null, string reason = "No reason.")
        {
            var CommandUser = Context.Guild.Users.Where(x => x.Id == Context.User.Id).FirstOrDefault() as SocketGuildUser;
            if (CommandUser.GuildPermissions.BanMembers)
            {
                if (kickuser == null)
                {
                    var embed = new EmbedBuilder();
                    embed.WithColor(Color.Red);
                    embed.WithDescription("Please mention the user you want to kick!");
                    embed.WithTimestamp(DateTime.Now);
                    await Context.Channel.SendMessageAsync(embed: embed.Build());
                }
                else
                {
                    var BotUser = Context.Guild.Users.Where(x => x.Id == Context.Client.CurrentUser.Id).FirstOrDefault() as SocketGuildUser;
                    if (BotUser.Hierarchy <= kickuser.Hierarchy)
                    {
                        var bembed = new EmbedBuilder();
                        bembed.WithColor(Color.Red);
                        bembed.WithDescription("Sorry I can't kick that user!");
                        bembed.WithTimestamp(DateTime.Now);
                        await Context.Channel.SendMessageAsync(embed: bembed.Build());
                        return;
                    }

                    kickuser.KickAsync(reason: reason);
                    var sembed = new EmbedBuilder();
                    sembed.WithColor(Color.Green);
                    sembed.WithDescription("User got Kick!");
                    sembed.WithTimestamp(DateTime.Now);
                    await Context.Channel.SendMessageAsync(embed: sembed.Build());
                }
            }
            else
            {
                var embed = new EmbedBuilder();
                embed.WithColor(Color.Red);
                embed.WithDescription("You don't have permissions to use this command!");
                embed.WithTimestamp(DateTime.Now);
                await Context.Channel.SendMessageAsync(embed: embed.Build());
            }
        }
        [Command("help kick")]
        public async Task helpService()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Kick Command");
            embed.WithColor(Color.Blue);
            embed.WithDescription("**Command**\nkick \n\n **Alias**\n--- \n\n **Description** \n Kick a member from the current guild! \n\n **Permissions** \n `Kick_Members`");
            embed.WithTimestamp(DateTime.Now);

            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}