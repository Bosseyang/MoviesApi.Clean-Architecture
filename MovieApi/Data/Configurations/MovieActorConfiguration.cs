using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using MovieApi.Models.Entities;

namespace MovieApi.Data.Configurations;

public class MovieActorConfiguration : IEntityTypeConfiguration<MovieActor>
{
    public void Configure(EntityTypeBuilder<MovieActor> builder)
    {
        //Composite key
        builder.HasKey(ma => new { ma.MovieId, ma.ActorId});

        builder.Property(m => m.Role)
            .IsRequired()
            .HasMaxLength(128);

        builder.HasOne(ma => ma.Movie)
            .WithMany(m => m.MovieActors)
            .HasForeignKey(ma => ma.MovieId);

        builder.HasOne(ma => ma.Actor)
            .WithMany(m => m.MovieActors)
            .HasForeignKey(ma => ma.ActorId);

        builder.ToTable("MovieActor");
    }
}
