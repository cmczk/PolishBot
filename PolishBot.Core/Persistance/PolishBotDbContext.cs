using Microsoft.EntityFrameworkCore;
using PolishBot.Core.Persistance.Configurations;
using PolishBot.Core.Persistance.Models;

public class PolishBotDbContext() : DbContext()
{
    public DbSet<Flashcard> Flashcards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=polish_bot.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FlashcardConfiguration());
    }
}