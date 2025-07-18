using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
using Movies.Data.Configurations;

namespace Movies.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; } = default!;
        public DbSet<MovieDetails> MovieDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        //TODO: Add configurations with fluent api here
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new MovieConfiguration());
            //modelBuilder.ApplyConfiguration(new MovieDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new MovieActorConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
        }

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
