using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Configurations;
using MovieApi.Models.Entities;

namespace MovieApi.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext (DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; } = default!;
        public DbSet<MovieDetails> MovieDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Actor> Actors { get; set; }
        //public DbSet<MovieActor> MovieActors { get; set; }

        //TODO: Add configurations with fluent api here
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
            //modelBuilder.ApplyConfiguration(new MovieConfigurations());
            //modelBuilder.ApplyConfiguration(new MovieDetailsConfigurations());
        //}

        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    ChangeTracker.DetectChanges();

        //    foreach (var entry in ChangeTracker.Entries<Movie>().Where(e=> e.State == EntityState.Modified))
        //    {
        //        entry.Property("Edited").CurrentValue = DateTime.UtcNow;
        //    }

        //    return base.SaveChangesAsync(cancellationToken);
        //}

    }
}
