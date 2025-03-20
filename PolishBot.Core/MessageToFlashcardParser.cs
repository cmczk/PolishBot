using PolishBot.Core.Persistance.Models;

namespace PolishBot.Core;

public static class MessageToFlashcardParser
{
    private const string Word = "Слово: ";
    private const string Explanation = "Объяснение: ";
    private const string Translation = "Перевод: ";
    private const string Example = "Пример: ";

    public static bool TryParse(string message, out Flashcard flashcard)
    {
        var lines = message.Split("\n");

        if (!IsValid(lines))
        {
            flashcard = Flashcard.Default;
            return false;
        }

        flashcard = lines switch
        {
            [var first, var second] => new Flashcard(
                word: first[Word.Length..],
                explanation: string.Empty,
                translation: second[Translation.Length..],
                example: string.Empty),

            [var first, var second, var third] when second.StartsWith(Explanation) => new Flashcard(
                word: first[Word.Length..],
                explanation: second[Explanation.Length..],
                translation: third[Translation.Length..],
                example: string.Empty),

            [var first, var second, var third] when second.StartsWith(Translation) => new Flashcard(
                word: first[Word.Length..],
                explanation: string.Empty,
                translation: second[Translation.Length..],
                example: third[Example.Length..]),

            [var first, var second, var third, var fourth] => new Flashcard(
                word: first[Word.Length..],
                explanation: second[Explanation.Length..],
                translation: third[Translation.Length..],
                example: fourth[Example.Length..]),

            _ => Flashcard.Default,
        };

        return true;
    }

    private static bool IsValid(string[] lines) =>
        IsLengthValid(lines)
        && StartsWithWord(lines)
        && !IsWordOrTranslationEmptyOrWhiteSpace(lines)
        && IsOrderValid(lines);

    private static bool IsLengthValid(string[] lines) =>
        lines.Length is >= 2 and <= 4;

    private static bool StartsWithWord(string[] lines) =>
        lines[0].StartsWith(Word);

    private static bool IsWordOrTranslationEmptyOrWhiteSpace(string[] lines) =>
        lines.Any(line =>
            (line.StartsWith(Word) && string.IsNullOrWhiteSpace(line[Word.Length..]))
            || (line.StartsWith(Translation) && string.IsNullOrWhiteSpace(line[Translation.Length..])));

    private static bool IsOrderValid(string[] lines)
    {
        return lines switch
        {
            [var _, var second] when second.StartsWith(Translation) => true,

            [var _, var second, var third] when
                second.StartsWith(Explanation)
                && third.StartsWith(Translation) => true,

            [var _, var second, var third] when
                second.StartsWith(Translation)
                && third.StartsWith(Example) => true,

            [var _, var second, var third, var fourth] when
                second.StartsWith(Explanation)
                && third.StartsWith(Translation)
                && fourth.StartsWith(Example) => true,

            _ => false,
        };
    }
}
