using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolishBot.Core.Persistance.Models;

namespace PolishBot.Core.Persistance.Configurations;

public class FlashcardConfiguration : IEntityTypeConfiguration<Flashcard>
{
    public void Configure(EntityTypeBuilder<Flashcard> builder)
    {
        builder.HasKey(f => f.Id);
    }
}
