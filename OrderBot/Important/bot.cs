using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using Newtonsoft.Json;
using OrderBot.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.Exceptions;

namespace OrderBot
{
	public class Bot
	{
		public DiscordClient Client { get; private set; }
		public CommandsNextExtension commands { get; private set; }
		public InteractivityExtension Interactivity { get; private set; }
		public async Task RunAsync() 
		{
			var json = String.Empty;

			using (var fs = File.OpenRead("config.json"))
			using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
				json = await sr.ReadToEndAsync().ConfigureAwait(false);

			var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

			var config = new DiscordConfiguration
			{
				Token = configJson.token,
				TokenType = TokenType.Bot,
				AutoReconnect = true,
				LogLevel = LogLevel.Debug,
				UseInternalLogHandler = true
				};


			Client = new DiscordClient(config);

			Client.Ready += OnClientReady;

			Client.UseInteractivity(new InteractivityConfiguration
			{
				Timeout = TimeSpan.FromMinutes(2)
			});

			var commandsConfig = new CommandsNextConfiguration
			{
				StringPrefixes = new string[] { configJson.Prefix },
				EnableMentionPrefix = true,
				EnableDms = false,
				DmHelp = true
			};

			commands = Client.UseCommandsNext(commandsConfig);

			commands.RegisterCommands<FunCommands>();
			commands.RegisterCommands<TeamCommands>();
			commands.CommandErrored += OnCommandError;
			Client.MessageDeleted += OnMessageDelete;
			await Client.ConnectAsync();

			await Task.Delay(-1);

		}

		private async Task OnMessageDelete(MessageDeleteEventArgs e)
		{

			DateTime time = DateTime.Now;
			DiscordChannel botLoggingChannel = e.Guild.GetChannel(716174886837420086);
			await Client.SendMessageAsync(botLoggingChannel,$"message saying {e.Message.Content} from {e.Message.Author.Mention} was deleted at {time}!");

		}

		private async Task OnCommandError(CommandErrorEventArgs e)
		{
			if (e.Exception is UnauthorizedException)
			{
				
		
				var cantKickEmbed = new DiscordEmbedBuilder
				{
					Title = "This user can't be kicked!",
					Color = DiscordColor.Yellow
				};
				var message = await Client.SendMessageAsync(e.Context.Channel, embed: cantKickEmbed);
				await Task.Delay(TimeSpan.FromSeconds(8));
				await message.DeleteAsync();

				
			}

		}



		private Task OnClientReady(ReadyEventArgs e)
		{
			return Task.CompletedTask;
		}

		
	}

}
