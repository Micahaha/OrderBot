﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus.Interactivity;
using DSharpPlus.Entities;
using BooruSharp.Search;

namespace OrderBot.Commands
{
	public class FunCommands : BaseCommandModule
	{
		[Command("ping")]
		[Description("Returns pong")]
		public async Task Ping(CommandContext ctx)
		{
			await ctx.Channel.SendMessageAsync("pong").ConfigureAwait(false);
		}
		[Command("Add")]
		[Description("Adds Two Numbers together")]
		public async Task Add(CommandContext ctx, [Description("First Number")]int numberOne, [Description("Second Number")]int numberTwo)
		{
			await ctx.Channel.SendMessageAsync((numberOne + numberTwo).ToString()).ConfigureAwait(false);
		}
		[Command("img")]
		public async Task TestCommand(CommandContext ctx)
		{
			await ctx.Channel.SendMessageAsync($"Your pfp is, {ctx.User.AvatarUrl}");
		}

		[Command("Codeblock")]
		public async Task Response(CommandContext ctx)
		{
			var Interactivity = ctx.Client.GetInteractivity();

			// Waits for message in Channels
			var message = await Interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

			await ctx.Channel.SendMessageAsync($"```{message.Result.Content}```");
		}
		[Command("ResponseEmoji")]
		public async Task ResponseEmoji(CommandContext ctx)
		{
			var Interactivity = ctx.Client.GetInteractivity();

			// Waits for message in Channels
			var message = await Interactivity.WaitForReactionAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

			await ctx.Channel.SendMessageAsync(message.Result.Emoji);
		}

	}
}
