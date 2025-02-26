using PolishBot.Core.Persistance.Models;

namespace PolishBot.Core;

public static class FlashcardParser
{
    public static bool TryParse(string message, out Flashcard flashcard)
    {
        var lines = message.Split("\n");

        if (!IsValid(lines))
        {
            flashcard = Flashcard.Default;
            return false;
        }

        if (lines.Length == 2)
        {
            flashcard = new(word: lines[0], explanation: string.Empty, translation: lines[1], example: string.Empty);
            return true;
        }
        else if (lines.Length == 3 && lines[1].StartsWith("Объяснение:"))
        {
            flashcard = new(word: lines[0], explanation: lines[1], translation: lines[2], example: string.Empty);
            return true;
        }
        else if (lines.Length == 3 && lines[1].StartsWith("Перевод:"))
        {
            flashcard = new(word: lines[0], explanation: string.Empty, translation: lines[1], example: lines[2]);
            return true;
        }
        else
        {
            flashcard = new(word: lines[0], explanation: lines[1], translation: lines[2], example: lines[3]);
            return true;
        }
    }

    private static bool IsValid(string[] lines)
    {
        return lines.Length == 2 && lines[0].StartsWith("Слово:") && lines[1].StartsWith("Перевод:")
            || lines.Length == 3 && lines[0].StartsWith("Слово:") && lines[1].StartsWith("Объяснение:") && lines[2].StartsWith("Перевод:")
            || lines.Length == 3 && lines[0].StartsWith("Слово:") && lines[1].StartsWith("Перевод:") && lines[2].StartsWith("Пример:")
            || lines.Length == 4 && lines[0].StartsWith("Слово:") && lines[1].StartsWith("Объяснение:") && lines[2].StartsWith("Перевод:") && lines[3].StartsWith("Пример:");
    }
}
