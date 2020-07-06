using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus.Interactivity;
using DSharpPlus.Entities;
using System.Runtime.InteropServices.ComTypes;
using DSharpPlus.Interactivity.EventHandling;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using BooruAPI.Core.Utilities;
using Imgur;
using Imgur.API;
using Imgur.API.Models.Impl;
using Newtonsoft.Json;
using RestSharp;
using Refit;
using Image = Imgur.API.Models.Impl.Image;
using FlawBOT.Core.Properties;
using BooruAPI;
using BooruAPI.Core;
using BooruSharp.Booru;
using BooruSharp.Search;
using BooruSharp.Search.Post;
using Reddit;
using Reddit.Exceptions;
using System.IO;

namespace OrderBot.Commands
{
	public class TeamCommands : BaseCommandModule
	{

		[Command("Join")]
		public async Task Join(CommandContext ctx)
		{
			var JoinEmbed = new DiscordEmbedBuilder
			{
				Title = "Would you like to join this role?",
				Color = DiscordColor.Yellow,
				ImageUrl = ctx.Client.CurrentUser.AvatarUrl,

			};

			var joinMessage = await ctx.Channel.SendMessageAsync(embed: JoinEmbed).ConfigureAwait(false);

			var thumbsUpEmoji = DiscordEmoji.FromName(ctx.Client, ":+1:");
			var thumbSDownEmoji = DiscordEmoji.FromName(ctx.Client, ":-1:");

			await joinMessage.CreateReactionAsync(thumbsUpEmoji).ConfigureAwait(false);
			await joinMessage.CreateReactionAsync(thumbSDownEmoji).ConfigureAwait(false);

			var interactivity = ctx.Client.GetInteractivity();

			var reactionResult = await interactivity.WaitForReactionAsync(
				x => x.Message == joinMessage &&
					 x.User == ctx.User &&
					 (x.Emoji == thumbsUpEmoji || x.Emoji == thumbSDownEmoji)).ConfigureAwait(false);

			if (reactionResult.Result.Emoji == thumbsUpEmoji)
			{
				var role = ctx.Guild.GetRole(718404624008216656);
				await ctx.Member.GrantRoleAsync(role).ConfigureAwait(false);
			}

			await joinMessage.DeleteAsync().ConfigureAwait(false);
		}

		[Command("pfp")]
		[Description("gets profile picture")]
		public async Task Pfp(CommandContext ctx, DiscordMember mention)
		{
			var pfpEmbed = new DiscordEmbedBuilder
			{
				Title = "Your PFP is ready!",
				Color = DiscordColor.Blurple,
				Description = $"This is the PFP of {mention.Username}",
				ImageUrl = mention.AvatarUrl
			};

			await ctx.Channel.SendMessageAsync(embed: pfpEmbed);
		}

		[Command("roles")]
		[Description("gets roles of each member")]
		public async Task Roles(CommandContext ctx, DiscordMember mention)
		{
			List<String> rolesItterate = new List<String>();
			foreach (var Roles in mention.Roles)
			{
				rolesItterate.Add(Roles.Name);
			}

			string combinedString = string.Join(", ", rolesItterate);
			if (combinedString.Contains(" ឵឵ ឵឵"))
			{
				combinedString.Replace(" ឵឵ ឵឵", "STUPID UNICODE ROLE");
			}

			await ctx.Channel.SendMessageAsync($"Current roles for {mention.DisplayName} are ``{combinedString}``")
				.ConfigureAwait(false);
		}

		[Command("joinDate")]
		[Description("gets joinDate")]
		public async Task joinDate(CommandContext ctx, DiscordMember mention)
		{
			await ctx.Channel.SendMessageAsync($"{mention.Username} joined ``{ctx.Guild.Name}`` at {mention.JoinedAt}")
				.ConfigureAwait(false);
		}

		[Hidden]
		[Command("kick")]
		[Description("kicks user")]
		[RequirePermissions(DSharpPlus.Permissions.KickMembers)]
		public async Task KickUser(CommandContext ctx, DiscordMember mention)
		{
			await mention.RemoveAsync("No reason specified");
			var MESSAGE = await ctx.Message.Channel.SendMessageAsync(
			  $"{mention.Username} was kicked by {ctx.Member.Username} | no reason Specified");
			await mention.RemoveAsync("No reason specified");
			var noReason = new DiscordEmbedBuilder
			{
				Title = $"{mention.Username} was kicked by {ctx.Member.Username} | no reason Specified",
				Color = DiscordColor.Yellow,
				ImageUrl = "https://media1.tenor.com/images/e1033b1081defdb52e4d137b963b420d/tenor.gif?itemid=14154013"
			};

			await ctx.Guild.GetChannel(716174886837420086).SendMessageAsync(embed: noReason).ConfigureAwait(false);
			await Task.Delay(TimeSpan.FromSeconds(8));
			await MESSAGE.DeleteAsync();
			await ctx.Message.DeleteAsync();


		}
		[Hidden]
		[Command("kick")]
		[Description("kicks user")]
		[RequirePermissions(DSharpPlus.Permissions.KickMembers)]
		public async Task KickUser(CommandContext ctx, DiscordMember mention, [RemainingText] string Reason)
		{
			await mention.RemoveAsync(Reason);
			var MESSAGE = await ctx.Message.Channel.SendMessageAsync(
				$"{mention.Username} was kicked by ``{ctx.Member.Username}`` for {Reason}");
			var pfpEmbed = new DiscordEmbedBuilder
			{
				Title = $"{mention.Username} was kicked by {ctx.Member.Username} for ``{Reason}``",
				Color = DiscordColor.Yellow,
				ImageUrl =
					"https://media1.tenor.com/images/e1033b1081defdb52e4d137b963b420d/tenor.gif?itemid=14154013"
			};
			await ctx.Guild.GetChannel(716174886837420086).SendMessageAsync(embed: pfpEmbed).ConfigureAwait(false);
			await Task.Delay(TimeSpan.FromSeconds(8));
			await MESSAGE.DeleteAsync();
			await ctx.Message.DeleteAsync();
		}

		[Command("Purge")]
		[Description("purges messages")]
		public async Task Members(CommandContext ctx, int Amount)
		{
			var messages = ctx.Channel.GetMessagesAsync(Amount);
			await ctx.Channel.DeleteMessagesAsync(messages.Result);

			var message = await ctx.Channel.SendMessageAsync($"``{Amount}`` messages deleted.");
			await Task.Delay(5000);
			await message.DeleteAsync();
		}

		[Command("Mute")]
		[Description("Mutes members")]
		public async Task Mute(CommandContext ctx, DiscordMember mention, string Time)
		{
			var embedMuted = new DiscordEmbedBuilder
			{
				Title = $"{mention.Username}#{mention.Discriminator} has been muted!"
			}; 

			var embedUnmuted = new DiscordEmbedBuilder
			{
				Title = $"{mention.Username}#{mention.Discriminator} has been unmuted!"
			};

			var muteRole = ctx.Guild.GetRole(721903282355699743);

			await mention.GrantRoleAsync(muteRole);
			var goodMessage = await ctx.Channel.SendMessageAsync(embed:embedMuted);
			await Task.Delay(5000);
			await goodMessage.DeleteAsync();

			await Task.Delay(TimeSpan.FromMinutes(Convert.ToInt32(Time)));

			await mention.RevokeRoleAsync(muteRole);
			var shitMessage = await ctx.Channel.SendMessageAsync(embed: embedUnmuted);
			await Task.Delay(5000);
			await shitMessage.DeleteAsync();
		}
		[Command("unmute")]
		[Description("unmute members")]
		public async Task Unmute(CommandContext ctx, DiscordMember mention, string Time)
		{
			var muteRole = ctx.Guild.GetRole(721903282355699743);
			await mention.RevokeRoleAsync(muteRole);

		}

		[Command("reddit")]
		[Description("Grabs images from reddit based on Subreddit")]
		public async Task reddit(CommandContext msg, string subreddit)
		{
			try
			{
				var json = String.Empty;

				using (var fs = File.OpenRead("config.json"))
				using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
					json = await sr.ReadToEndAsync().ConfigureAwait(false);

				var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);


				var redditCli = new RedditClient(appId: configJson.AppId, accessToken: configJson.Access, refreshToken: configJson.Refresh);
				var greentext = redditCli.Subreddit(subreddit).About();
				int rnd = new Random().Next(10);
				var post = greentext.Posts.Hot[rnd].Listing;
				var linkPost = "https://www.reddit.com" + post.Permalink;
				var link = post.URL;

				await msg.RespondAsync(link);


			}
			catch (RedditUnauthorizedException e)
			{
				await msg.RespondAsync("exception cought!");

			}

			catch (RedditNotFoundException e)
			{
				await msg.RespondAsync("``subreddit not found``");
			}

			catch (RedditForbiddenException e)
			{
				await msg.RespondAsync("``Reddit Not accessible``");
			}
		}
	}
}
	


