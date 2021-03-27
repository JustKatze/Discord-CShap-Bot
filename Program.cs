using System;
using System.IO;
using System.Configuration;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Nao_Discord_Bot.Commands;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Victoria;
using YamlDotNet.Serialization;

namespace Nao_Discord_Bot
{
    class Program
    {
        
        private DiscordSocketClient client;
        private static string prefix;
        public CommandService commands;
        static void Main() => new Program().MainAsync().GetAwaiter().GetResult();
        public async Task MainAsync()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig());
            
            commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });
            
            await commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
            prefix = "n+";

            client.MessageReceived += MessageEvent;
            client.Ready += onClientRady;

            await client.LoginAsync(TokenType.Bot, "Your Token");
            await client.StartAsync();
            await Task.Delay(-1);
        }


        private async Task onClientRady()
        {
            await client.SetStatusAsync(UserStatus.DoNotDisturb);
            await client.SetGameAsync("to Charlotte", $"", ActivityType.Watching);
        }

        private async Task MessageEvent(SocketMessage MessageParam)
        {
            var Message = MessageParam as SocketUserMessage;
            var Context= new SocketCommandContext(client, Message);
            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            int ArgPos = 0;
            if (!(Message.HasStringPrefix(prefix, ref ArgPos))) return;
            var result = await commands.ExecuteAsync(context: Context, ArgPos, null, MultiMatchHandling.Best);
            if (!result.IsSuccess)
            {
                Console.WriteLine(($"Error at executing command: {Context.Message.Content} | Error: {result.ErrorReason}"));
                await Context.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }
}