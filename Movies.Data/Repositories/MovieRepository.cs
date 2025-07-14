using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.Entities;

namespace Movies.Data.Repositories;

internal class MovieRepository : IMovieRepository
{

    private readonly MovieContext _context;
    public MovieRepository(MovieContext context) => _context = context;





    public Task<bool> AnyAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Movie>> GetAllAsync() => await _context.Movies.ToListAsync();

    public async Task<Movie?> GetAsync(int id) => await _context.Movies
        .Include(m => m.MovieDetails)
        .Include(m => m.Reviews)
        .Include(m => m.MovieActors).ThenInclude(ma => ma.Actor)
        .FirstOrDefaultAsync(m => m.Id == id);

    public void Add(Movie movie) => _context.Movies.Add(movie);
    public void Remove(Movie movie) => _context.Movies.Remove(movie);
    public void Update(Movie movie) => _context.Movies.Update(movie);
}
