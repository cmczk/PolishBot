using dotenv.net;
using PolishBot.Core;
using PolishBot.Core.Persistance.Repositories;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

DotEnv.Load();
var botApiKey = DotEnv.Read()["BOT_API_KEY"];

using var cts = new CancellationTokenSource();
var bot = new TelegramBotClient(token: botApiKey, cancellationToken: cts.Token);
var me = await bot.GetMe();

var dbContext = new PolishBotDbContext();
var flashcardsRepository = new FlashcardsRepository(dbContext);

bot.OnError += OnError;
bot.OnMessage += OnMessage;

Console.WriteLine($"@{me.Username} is running... Press Enter to terminate.");
Console.ReadLine();

cts.Cancel();

async Task OnError(Exception exception, HandleErrorSource source)
{
    Console.WriteLine(exception);
    await Task.Delay(2000, cts.Token);
}

async Task OnMessage(Message message, UpdateType type)
{
    if (string.IsNullOrWhiteSpace(message.Text)) return;

    if (message.Text.Trim() == "/flashcard")
    {
        var randomFlashcard = await flashcardsRepository.GetRandomFlashCard();
        await bot.SendMessage(chatId: message.Chat.Id, text: randomFlashcard.FormatToSend(), parseMode: ParseMode.Html);
    }
    else if (MessageToFlashcardParser.TryParse(message.Text, out var flashcard))
    {
        await flashcardsRepository.AddFlashcard(flashcard);
        await bot.SendMessage(message.Chat, "Я сохранил новую карточку! Вот она:");
        await bot.DeleteMessage(message.Chat, message.Id);
        await bot.SendMessage(chatId: message.Chat, text: flashcard.FormatToSend(), parseMode: ParseMode.Html);
    }
    else
    {
        Console.WriteLine($"Is is not a valid flashcard text:\n{message.Text}");
        await bot.SendMessage(message.Chat, "Из этого сообщения не получится создать карточку :(\nПопробуй ещё раз!\nИспользуй команду /help, помогает...");
    }
}
