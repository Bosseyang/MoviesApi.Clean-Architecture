using Microsoft.EntityFrameworkCore;
using Movies.Core.DTOs;
using Movies.Core.Entities;


namespace Movies.Data.Repositories;

public class ActorRepository : IActorRepository
{
    private readonly MovieContext _context;
    public ActorRepository(MovieContext context) => _context = context;

    public async Task<PagedResult<Actor>> GetPagedActorsAsync(PagingParams pagingParams)
    {
        var query = _context.Actors.AsQueryable();

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pagingParams.PageSize);

        var items = await query
            .Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
            .Take(pagingParams.PageSize)
            .ToListAsync();

        return new PagedResult<Actor>
        {
            Data = items,
            Meta = new MetaData
            {
                TotalItems = totalItems,
                CurrentPage = pagingParams.PageNumber,
                TotalPages = totalPages,
                PageSize = pagingParams.PageSize
            }
        };
    }

    public async Task<Movie?> GetMovieWithActorsAsync(int movieId)
    {
        return await _context.Movies
            .Include(m => m.MovieActors)
            .FirstOrDefaultAsync(m => m.Id == movieId);
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
    public async Task AddActorToMovieAsync(int movieId, int actorId)
    {
        var movie = await _context.Movies
            .Include(m => m.MovieActors)
            .FirstOrDefaultAsync(m => m.Id == movieId);

        if (movie == null) throw new Exception("Movie not found");

        movie.MovieActors.Add(new MovieActor
        {
            MovieId = movieId,
            ActorId = actorId
        });

        await _context.SaveChangesAsync();
    }

    public async Task AddActorToMovieWithRoleAsync(int movieId, MovieActor movieActor)
    {
        movieActor.MovieId = movieId;
        _context.MovieActors.Add(movieActor);
        await _context.SaveChangesAsync();
    }
    public async Task AddActorToMovieWithRoleAsync(int movieId, int actorId, string role)
    {
        var movie = await _context.Movies
            .Include(m => m.MovieActors)
            .FirstOrDefaultAsync(m => m.Id == movieId);
        if (movie == null) throw new Exception("Movie not found");

        var actor = await _context.Actors.FindAsync(actorId);
        if (actor == null) throw new Exception("Actor not found");

        var alreadyExists = await _context.MovieActors
            .AnyAsync(ma => ma.MovieId == movieId && ma.ActorId == actorId);
        if (alreadyExists) throw new Exception("Actor already added to movie.");

        var movieActor = new MovieActor
        {
            MovieId = movieId,
            ActorId = actorId,
            Role = role
        };

        _context.MovieActors.Add(movieActor);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<MovieActor>> GetActorsByMovieAsync(int movieId)
    {
        return await _context.MovieActors
            .Where(ma => ma.MovieId == movieId)
            .Include(ma => ma.Actor)
            .ToListAsync();
    }

    public async Task<bool> ActorAlreadyInMovieAsync(int movieId, int actorId)
    {
        return await _context.MovieActors
            .AnyAsync(ma => ma.MovieId == movieId && ma.ActorId == actorId);
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
