using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.Entities;

namespace Movies.Data.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly MovieContext _context;
    public GenreRepository(MovieContext context) => _context = context;

    public async Task<bool> ExistsAsync(string name)
        => await _context.Genres.AnyAsync(g => g.Name == name);

    public async Task<IEnumerable<Genre>> GetAllAsync()
        => await _context.Genres.ToListAsync();

    public async Task<Genre?> GetByNameAsync(string name)
    {
        return await _context.Genres.FirstOrDefaultAsync(g => g.Name == name);
    }
}
