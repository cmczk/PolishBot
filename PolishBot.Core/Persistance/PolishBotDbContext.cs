using Microsoft.EntityFrameworkCore;
using PolishBot.Core.Persistance.Models;

public class PolishBotDbContext(DbContextOptions<PolishBotDbContext> options) : DbContext(options)
{
    public DbSet<Flashcard> Flashcards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=polish_bot.db");
    }
}