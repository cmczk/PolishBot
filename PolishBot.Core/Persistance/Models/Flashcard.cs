using System.ComponentModel.DataAnnotations.Schema;

namespace PolishBot.Core.Persistance.Models;

[Table("flashcards")]
public class Flashcard
{
    public int Id { get; init; }
    public string Word { get; init; }
    public string? Explanation { get; init; }
    public string Translation { get; init; }
    public string? Example { get; init; }

    public Flashcard(string word, string explanation, string translation, string example)
    {
        Word = word;
        Explanation = explanation;
        Translation = translation;
        Example = example;
    }

    public static Flashcard Default { get; } = new(
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty);
}
