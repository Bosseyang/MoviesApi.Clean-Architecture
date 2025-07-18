using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Core.Entities;

namespace Movies.Data.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(g => g.Movies)
            .WithOne(m => m.Genre)
            .HasForeignKey(m => m.GenreId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder.ToTable("Genres");


    }
}