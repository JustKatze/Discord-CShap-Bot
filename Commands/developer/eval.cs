using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace Nao_Discord_Bot.Commands.developer
{
    public class Eval : ModuleBase<SocketCommandContext>
    {
        private static ulong OwnerID = 292588280304893952;
        [Command("eval")] 
        public async Task eval(string code, bool allowInfiniteLoop = true)
        {
            if (Context.User.Id == OwnerID)
            {
                using (Context.Channel.EnterTypingState())
                {
                    try
                    {
                        var references = new List<MetadataReference>();
                        var referencedAssemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies();
                        foreach (var referencedAssembly in referencedAssemblies)
                            references.Add(MetadataReference.CreateFromFile(Assembly.Load(referencedAssembly).Location));
                        var scriptoptions = ScriptOptions.Default.WithReferences(references);
                        Globals globals = new Globals { Context = Context, Guild = Context.Guild as SocketGuild };
                        object o = await CSharpScript.EvaluateAsync(@"using System;using System.Linq;using System.Threading.Tasks;using Discord.WebSocket;using Discord;" + code, scriptoptions, globals);
                        if (o == null)
                        {
                            await Context.Channel.SendMessageAsync($"Eval finished!");
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync($"Result: {o.ToString()}");
                        }
                    }
                    catch(Exception e)
                    {
                        var errorembed = new EmbedBuilder();
                        errorembed.WithTitle("Eval");
                        errorembed.WithColor(Color.Red);
                        errorembed.WithDescription($"**Output**\n```Error: {e.Message} \n From: {e.Source}```");
                        await Context.Channel.SendMessageAsync(embed: errorembed.Build());
                    }
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("You don't have permissions to use this command!");
            }

        }
        public class Globals
        {
            public ICommandContext Context;
            public SocketGuild Guild;
        }

        [Command("help eval")]
        public async Task helpService()
            {
                var embed = new EmbedBuilder();
                embed.WithTitle("Eval Command");
                embed.WithColor(Color.Blue);
                embed.WithDescription("**Command**\neval \n\n **Alias**\n--- \n\n **Description** \n Eval command via Nao! (Only Developers) \n\n **Permissions** \n `Bot Developer`");
                embed.WithTimestamp(DateTime.Now);

                await Context.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}