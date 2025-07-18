using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Core.DTOs;
using Movies.Core.Entities;

namespace Movies.Data.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(m => m.Id);

        builder.HasOne(m => m.MovieDetails)
            .WithOne(md => md.Movie)
            .HasForeignKey<MovieDetails>(md => md.MovieId);

        builder.Property(m => m.Title)
            //.HasColumnName("Title")
            .HasMaxLength(255)
            .IsRequired();
        //... Add more

        //Shadow property
        builder.Property<DateTime>("Edited");

        builder.HasOne(m => m.Genre)
            .WithMany(g => g.Movies)
            .HasForeignKey(m => m.GenreId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("Movies");

    }
}
