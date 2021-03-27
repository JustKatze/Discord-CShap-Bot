using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Nao_Discord_Bot.Commands.moderation
{
    public class Ban : ModuleBase<SocketCommandContext>
    {
        [Command("ban")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task ban(SocketGuildUser Banuser = null, string reason = "No reason.")
        {
            var CommandUser = Context.Guild.Users.Where(x => x.Id == Context.User.Id).FirstOrDefault() as SocketGuildUser;
            if (CommandUser.GuildPermissions.BanMembers)
            {
                if (Banuser == null)
                {
                    var embed = new EmbedBuilder();
                    embed.WithColor(Color.Red);
                    embed.WithDescription("Please mention the user you want to ban!");
                    embed.WithTimestamp(DateTime.Now);
                    await Context.Channel.SendMessageAsync(embed: embed.Build());
                }
                else
                {
                    var BotUser = Context.Guild.Users.Where(x => x.Id == Context.Client.CurrentUser.Id).FirstOrDefault() as SocketGuildUser;
                    if (BotUser.Hierarchy <= Banuser.Hierarchy)
                    {
                        var bembed = new EmbedBuilder();
                        bembed.WithColor(Color.Red);
                        bembed.WithDescription("Sorry I can't ban that user!");
                        bembed.WithTimestamp(DateTime.Now);
                        await Context.Channel.SendMessageAsync(embed: bembed.Build());
                        return;
                    }

                    Banuser.BanAsync(reason: reason);
                    var sembed = new EmbedBuilder();
                    sembed.WithColor(Color.Green);
                    sembed.WithDescription("User got banned!");
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
        [Command("help ban")]
        public async Task helpService()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Ban Command");
            embed.WithColor(Color.Blue);
            embed.WithDescription("**Command**\nban \n\n **Alias**\n--- \n\n **Description** \n Ban a member from the current guild! \n\n **Permissions** \n `Ban_Members`");
            embed.WithTimestamp(DateTime.Now);

            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}