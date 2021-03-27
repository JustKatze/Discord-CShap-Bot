using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Nao_Discord_Bot.Commands.Utilities
{
    public class userinfo : ModuleBase<SocketCommandContext>
    {
        [Command("userinfo")]
        [Alias("ui")]
        public async Task UserInfo(SocketGuildUser User = null)
        {

            SocketGuildUser GuildUser;
            if (User == null)
            {
                GuildUser = Context.Guild.Users.Where(x => x.Id == Context.User.Id).FirstOrDefault() as SocketGuildUser;
            }
            else
            {
                GuildUser = User;
            }
            
            string Roles = "";
            var RoleList = GuildUser.Roles.ToList();
            RoleList.Sort();
            RoleList.Reverse();
            RoleList.ForEach(x => Roles += x.Mention + ", ");
            Roles = Roles.Remove(Roles.Length - 2);

            string Permissions = "";
            var PermissionList = GuildUser.GuildPermissions.ToList();
            PermissionList.Sort();
            PermissionList.Reverse();
            PermissionList.ForEach(x => Permissions += x.ToString() + ", ");
            
            var embed = new EmbedBuilder();
            embed.WithTitle("Userinfo");
            embed.WithTimestamp(DateTime.Now);
            embed.WithThumbnailUrl(GuildUser.GetAvatarUrl());
            embed.WithColor(Color.Blue);
            embed.AddField("Name", GuildUser.ToString(), true);
            embed.AddField("ID", GuildUser.Id, true);
            embed.AddField("Joined", GuildUser.JoinedAt, true);
            embed.AddField("Creation Date", GuildUser.CreatedAt.DateTime, true);
            embed.AddField("Permissions", Permissions);
            embed.AddField("Roles", Roles);

            await Context.Channel.SendMessageAsync(embed: embed.Build());

        }
        [Command("help userinfo")]
        [Alias("help ui")]
        public async Task helpService()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Userinfo Command");
            embed.WithColor(Color.Blue);
            embed.WithDescription("**Command**\nuserinfo \n\n **Alias**\nui \n\n **Description** \n Get infos about a user on the current server! \n\n **Permissions** \n `No permissions needed!`");
            embed.WithTimestamp(DateTime.Now);

            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}