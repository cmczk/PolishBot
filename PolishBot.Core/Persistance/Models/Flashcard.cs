namespace PolishBot.Core.Persistance.Models;

public class Flashcard
{
    public string Word { get; set; } = null!;
    public string Translation { get; set; } = null!;
    public string Example { get; set; } = null!;
}
