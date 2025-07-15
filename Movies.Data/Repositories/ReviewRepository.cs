using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Data.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly MovieContext _context;

    public ReviewRepository(MovieContext context) => _context = context;

    public async Task<IEnumerable<Review>> GetReviewsByMovieAsync(int movieId)
    {
        return await _context.Reviews
            .Where(r => r.MovieId == movieId)
            .ToListAsync();
    }
    public async Task<bool> MovieExistsAsync(int movieId)
    {
        return await _context.Movies.AnyAsync(m => m.Id == movieId);
    }

}
