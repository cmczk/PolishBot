using System.ComponentModel.DataAnnotations.Schema;

namespace PolishBot.Core.Persistance.Models;

[Table("flashcards")]
public class Flashcard
{
    public int Id { get; set; }
    public string Word { get; set; } = null!;
    public string? Explanation { get; set; }
    public string Translation { get; set; } = null!;
    public string? Example { get; set; }

    public Flashcard(string word, string explanation, string translation, string example)
    {
        Word = word;
        Explanation = explanation;
        Translation = translation;
        Example = example;
    }

    public static Flashcard Default { get; } = new(
        "Marzenie ściętej głowy",
        "Coś nierealnego, niedostępnego dla kogoś", 
        "Мечта отсечённой головы, нереалистичное желание или устремление",
        "Chcesz się z nią ożenić? Marzenie ściętej głowy! Ona nigdy się nie zgodzi");
}
