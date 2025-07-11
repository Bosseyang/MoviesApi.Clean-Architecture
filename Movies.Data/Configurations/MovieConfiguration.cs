using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Core.Entities;

namespace Movies.Data.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Title)
            //.HasColumnName("Title")
            .HasMaxLength(255)
            .IsRequired();
        //... Add more

        //Shadow property
        builder.Property<DateTime>("Edited");

        //builder.ToTable("Movies");
    }
}
