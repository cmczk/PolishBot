using PolishBot.Core;
using PolishBot.Core.Persistance.Models;

namespace PolishBot.Tests;

public class MessageToFlashcardParserTests
{
    [Theory]
    [InlineData(
        "Слово: spodziewać się\nПеревод: надеяться",
        true,
        "spodziewać się",
        "",
        "надеяться", 
        "")]
    [InlineData(
        "Слово: potrzebować\nОбъяснение: mieć potrzebę/pragnienie posiadania/zrobienia czegoś\nПеревод: нуждаться",
        true,
        "potrzebować",
        "mieć potrzebę/pragnienie posiadania/zrobienia czegoś",
        "нуждаться",
        "")]
    [InlineData(
        "Слово: początek\nПеревод: начало\nПример: na początku Bóg stworzył niebo i ziemię",
        true,
        "początek",
        "",
        "начало",
        "na początku Bóg stworzył niebo i ziemię")]
    [InlineData(
        "Слово: relacja\nОбъяснение: zależność między dwoma bądź większą liczbą elementów\nПеревод: отношение; маршрут\nПример: ostatni pociąg relacji Wrocław - Warszawa odjeżdża przed północą",
        true,
        "relacja",
        "zależność między dwoma bądź większą liczbą elementów",
        "отношение; маршрут",
        "ostatni pociąg relacji Wrocław - Warszawa odjeżdża przed północą")]
    [InlineData(
        "",
        false,
        "",
        "",
        "",
        "")]
    [InlineData(
        "Слово: relacja",
        false,
        "",
        "",
        "",
        "")]
    [InlineData(
        "Слово: relacja\nОбъяснение: zależność między dwoma bądź większą liczbą elementów",
        false,
        "",
        "",
        "",
        "")]
    [InlineData(
        "Объяснение: zależność między dwoma bądź większą liczbą elementów\nПеревод: отношение; маршрут",
        false,
        "",
        "",
        "",
        "")]
    [InlineData(
        "Слово: relacja\nПеревод: отношение; маршрут\nОбъяснение: zależność między dwoma bądź większą liczbą elementów\nПример: ostatni pociąg relacji Wrocław - Warszawa odjeżdża przed północą",
        false,
        "",
        "",
        "",
        "")]
    [InlineData(
        "Слово: \nОбъяснение: zależność między dwoma bądź większą liczbą elementów\nПеревод: отношение; маршрут\nПример: ostatni pociąg relacji Wrocław - Warszawa odjeżdża przed północą",
        false,
        "",
        "",
        "",
        "")]
    public void TryParse_ShouldReturnTrueForValidFlashcards(
        string message,
        bool expectedResult,
        string expectedWord,
        string expectedExplanation,
        string expectedTranslation,
        string expectedExample)
    {
        var wasParsed = MessageToFlashcardParser.TryParse(message, out var flashcard);

        Assert.Equal(expectedResult, wasParsed);
        
        if (wasParsed)
        {
            Assert.Equal(expectedWord, flashcard.Word);
            Assert.Equal(expectedExplanation, flashcard.Explanation);
            Assert.Equal(expectedTranslation, flashcard.Translation);
            Assert.Equal(expectedExample, flashcard.Example);
        }
        else
        {
            Assert.Equal(flashcard, Flashcard.Default);
        }
    }
}