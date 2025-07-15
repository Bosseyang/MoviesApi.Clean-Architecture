using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Data.Repositories;

public class ActorRepository : IActorRepository
{
    private readonly MovieContext _context;
    public ActorRepository(MovieContext context) => _context = context;

    public async Task<Movie?> GetMovieWithActorsAsync(int movieId)
    {
        return await _context.Movies
            .Include(m => m.MovieActors)
            .FirstOrDefaultAsync(m => m.Id == movieId);
    }

    public async Task<bool> ActorAlreadyInMovieAsync(int movieId, int actorId)
    {
        return await _context.MovieActors
            .AnyAsync(ma => ma.MovieId == movieId && ma.ActorId == actorId);
    }

    public async Task AddActorToMovieAsync(Movie movie, int actorId)
    {
        movie.MovieActors.Add(new MovieActor
        {
            MovieId = movie.Id,
            ActorId = actorId
        });

        await _context.SaveChangesAsync();
    }

    public async Task AddActorWithRoleToMovieAsync(int movieId, MovieActor movieActor)
    {
        movieActor.MovieId = movieId;
        _context.MovieActors.Add(movieActor);
        await _context.SaveChangesAsync();
    }
    public async Task<bool> MovieExistsAsync(int movieId)
    {
        return await _context.Movies.AnyAsync(m => m.Id == movieId);
    }

    public async Task<bool> ActorExistsAsync(int actorId)
    {
        return await _context.Actors.AnyAsync(a => a.Id == actorId);
    }
}
