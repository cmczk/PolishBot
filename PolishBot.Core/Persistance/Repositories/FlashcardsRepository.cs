using Microsoft.EntityFrameworkCore;
using PolishBot.Core.Persistance.Models;

namespace PolishBot.Core.Persistance.Repositories;

public class FlashcardsRepository(PolishBotDbContext context)
{
    private readonly PolishBotDbContext _context = context;

    public async Task AddFlashcard(Flashcard flashcard)
    {
        await _context.Flashcards.AddAsync(flashcard);
        await _context.SaveChangesAsync();
    }

    public async Task<Flashcard> GetRandomFlashCard()
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        
        try
        {
            var flashcardsCount = _context.Flashcards.Count();
            var random = new Random();
            var randomIndex = random.Next(flashcardsCount);

            await transaction.CommitAsync();
            
            return await _context.Flashcards.Skip(randomIndex).FirstOrDefaultAsync()
                ?? Flashcard.Default;
        }
        catch
        {
            await transaction.RollbackAsync();
            return Flashcard.Default;
        }
    }
}
