using dotenv.net;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

DotEnv.Load();
var botApiKey = DotEnv.Read()["BOT_API_KEY"];

using var cts = new CancellationTokenSource();
var bot = new TelegramBotClient(token: botApiKey, cancellationToken: cts.Token);
var me = await bot.GetMe();

bot.OnMessage += OnMessage;

Console.WriteLine($"Hello, world! My ID is {me.Id} and my name is {me.FirstName}");
Console.ReadLine();
cts.Cancel();

async Task OnMessage(Message message, UpdateType type)
{
    if (message.Text is null) return;
    Console.WriteLine($"Received {type} '{message.Text}' in {message.Chat}");
    await bot.SendMessage(message.Chat, $"{message.From} said:\n {message.Text}");
}
