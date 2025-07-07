using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models.Entities;

namespace MovieApi.Data.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Title)
            .HasColumnName("Title")
            .HasMaxLength(255);
        //... Add more

        //Shadow property
        builder.Property<DateTime>("Edited");

        builder.ToTable("Movies");
    }
}
