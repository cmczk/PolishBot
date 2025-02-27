using PolishBot.Core.Persistance.Models;

namespace PolishBot.Core;

public static class FlashcardParser
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

        flashcard = lines.Length switch
        {
            2 => new Flashcard(
                word: lines[0][Word.Length..], 
                explanation: string.Empty,
                translation: lines[1][Translation.Length..],
                example: string.Empty),
            
            3 when lines[1].StartsWith(Explanation) => new Flashcard(
                word: lines[0][Word.Length..],
                explanation: lines[1][Explanation.Length..],
                translation: lines[2][Translation.Length..],
                example: string.Empty),
            
            3 when lines[1].StartsWith(Translation) => new Flashcard(
                word: lines[0][Word.Length..],
                explanation: string.Empty,
                translation: lines[1][Translation.Length..],
                example: lines[2][Example.Length..]),
            
            _ => new Flashcard(
                word: lines[0][Word.Length..],
                explanation: lines[1][Explanation.Length..],
                translation: lines[2][Translation.Length..],
                example: lines[3][Example.Length..]),
        };
        
        return Flashcard.IsValid(flashcard);
    }

    private static bool IsValid(string[] lines)
    {
        return IsLengthValid(lines)
               && StartsWithWord(lines)
               && IsOrderValid(lines);
    }

    private static bool IsLengthValid(string[] lines)
    {
        return (lines.Length is >= 2 and <= 4);
    }

    private static bool StartsWithWord(string[] lines)
    {
        return lines[0].StartsWith(Word);
    }

    private static bool IsOrderValid(string[] lines)
    {
        return lines.Length switch
        {
            2 when lines[1].StartsWith(Translation) => true,
            3 when lines[1].StartsWith(Explanation) && lines[2].StartsWith(Translation) => true,
            3 when lines[1].StartsWith(Translation) && lines[2].StartsWith(Example) => true,
            4 when lines[1].StartsWith(Explanation) 
                   && lines[2].StartsWith(Translation)
                   && lines[3].StartsWith(Example) => true,
            _ => false,
        };
    }
}
