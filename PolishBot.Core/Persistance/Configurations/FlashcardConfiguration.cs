using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolishBot.Core.Persistance.Models;

namespace PolishBot.Core.Persistance.Configurations;

public class FlashcardConfiguration : IEntityTypeConfiguration<Flashcard>
{
    public void Configure(EntityTypeBuilder<Flashcard> builder)
    {
        builder.HasKey(f => f.Id);
        
        builder.Property(f => f.Word).IsRequired().HasMaxLength(255);
        builder.Property(f => f.Explanation).IsRequired().HasMaxLength(255);
        builder.Property(f => f.Translation).IsRequired().HasMaxLength(255);
        builder.Property(f => f.Example).IsRequired().HasMaxLength(255);
    }
}
