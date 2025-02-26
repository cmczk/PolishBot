using dotenv.net;
using Telegram.Bot;

DotEnv.Load();

var botApiKey = DotEnv.Read()["BOT_API_KEY"];

var bot = new TelegramBotClient(botApiKey);
var me = await bot.GetMe();
Console.WriteLine($"Hello, world! My ID is {me.Id} and my name is {me.FirstName}");