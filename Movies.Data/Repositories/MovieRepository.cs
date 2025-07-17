using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.Entities;

namespace Movies.Data.Repositories;

public class MovieRepository : /*RepositoryBase<Movie>,*/ IMovieRepository
{

    private readonly MovieContext _context;
    public MovieRepository(MovieContext context) => _context = context;

    public async Task<IEnumerable<Movie>> GetMoviesAsync() => await _context.Movies.ToListAsync();

    public async Task<Movie?> GetMovieAsync(int id, bool withActors = false)
    {
        var query = _context.Movies.AsQueryable();
        if (withActors)
        {
            query = query
                .Include(m => m.MovieActors)
                .ThenInclude(m => m.Actor);
        }
        return await query.FirstOrDefaultAsync(m => m.Id == id);

    }
    public async Task<Movie?> GetAllMovieDetailsAsync(int id)
    {
        return await _context.Movies
            .Include(m => m.MovieDetails)
            .Include(m => m.Reviews)
            .Include(m => m.MovieActors).ThenInclude(ma => ma.Actor)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    public async Task<Movie?> GetMovieDetailsAsync(int id)
    {
        return await _context.Movies
            .Include(m => m.MovieDetails)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Movies.AnyAsync(m => m.Id == id);
    }
    public void Add(Movie movie) => _context.Movies.Add(movie);
    public void Remove(Movie movie) => _context.Movies.Remove(movie);
    public void Update(Movie movie) => _context.Movies.Update(movie);
}
