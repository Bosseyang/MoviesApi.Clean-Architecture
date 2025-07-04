using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models.Entities;

namespace MovieApi.Data.Configurations;

public class MovieDetailsConfigurations : IEntityTypeConfiguration<MovieDetails>
{
    public void Configure(EntityTypeBuilder<MovieDetails> builder)
    {

        builder.ToTable("MovieDetails");
    }
}
