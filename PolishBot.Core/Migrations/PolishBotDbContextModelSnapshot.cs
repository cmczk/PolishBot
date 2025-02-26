﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace PolishBot.Core.Migrations
{
    [DbContext(typeof(PolishBotDbContext))]
    partial class PolishBotDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("PolishBot.Core.Persistance.Models.Flashcard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Example")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Explanation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Translation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("flashcards");
                });
#pragma warning restore 612, 618
        }
    }
}
