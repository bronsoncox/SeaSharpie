using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace SeaSharpie
{
    class Program
    {
        private DiscordSocketClient botClient;

        // !-- Most of this code janked from https://docs.stillu.cc/guides/getting_started/first-bot.html
        // Creates ASYNC setup
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            botClient = new DiscordSocketClient();

            // TODO: Instantiate for now, check out more into ServiceProvider info
            CommandService commands = new CommandService();
            CommandHandler CH1 = new CommandHandler(botClient, commands);

            // TODO: Adding this to bupass SSL checks for now..
            //ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            botClient.Log += Log;

            //  You can assign your bot token to a string, and pass that in to connect.
            //  This is, however, insecure, particularly if you plan to have your code hosted in a public repository.
            var token = "WeDontNeedNoStinkingTokens!"; // ..but seriously, you do need a token.. //

            // Some alternative options would be to keep your token in an Environment Variable or a standalone file.
            // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
            // var token = File.ReadAllText("token.txt");
            // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

            await botClient.LoginAsync(TokenType.Bot, token);
            await botClient.StartAsync();

            await CH1.InstallCommandsAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        // !-- Basic console logging
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

    }
}
