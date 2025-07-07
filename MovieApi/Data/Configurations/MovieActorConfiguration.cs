using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using MovieApi.Models.Entities;

namespace MovieApi.Data.Configurations;

public class MovieActorConfiguration : IEntityTypeConfiguration<MovieActor>
{
    public void Configure(EntityTypeBuilder<MovieActor> builder)
    {
        builder.HasKey(m => new { m.MovieId, m.ActorId});

        builder.Property(m => m.Role)
            .IsRequired();

        builder.HasOne(m => m.Movie)
            .WithMany(a => a.MovieActors)
            .HasForeignKey(m => m.MovieId);

        builder.HasOne(a => a.Actor)
            .WithMany(m => m.MovieActors)
            .HasForeignKey(a => a.ActorId);

        builder.ToTable("MovieActor");
    }
}
