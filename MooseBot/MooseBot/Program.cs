namespace MooseBot
{
	using System;
	using System.IO;
	using System.Threading.Tasks;
	using DSharpPlus;
	using Newtonsoft.Json;

	class Program
	{

		static void Main(string[] args)
		{
			MainAsync().GetAwaiter().GetResult();	
		}

		static async Task MainAsync()
		{
			// See config.example.json for how the config should be setup, we dont want to push the config with the real token for security reasons.
			var configPath = Directory.GetCurrentDirectory() + "/config.json";
			var reader = new StreamReader(configPath);

			var config = JsonConvert.DeserializeObject<MooseBotConfig>(reader.ReadToEnd());

			var discord = new DiscordClient(new DiscordConfiguration()
			{
				Token = config.Token,
				TokenType = TokenType.Bot,
				Intents = DiscordIntents.AllUnprivileged,
			});

			discord.MessageCreated += async (s, e) =>
			{
				if (e.Message.Content.ToLower().StartsWith("ping"))
				{
					await e.Message.RespondAsync("pong!");
				}
			};

			await discord.ConnectAsync();
			await Task.Delay(-1);
		}
	}
}
