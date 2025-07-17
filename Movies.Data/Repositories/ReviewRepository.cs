using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.DTOs;
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

    public async Task<PagedResult<Review>> GetPagedReviewsAsync(int movieId, PagingParams pagingParams)
    {
        var query = _context.Reviews.Where(r => r.MovieId == movieId);

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pagingParams.PageSize);

        var items = await query
            .Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
            .Take(pagingParams.PageSize)
            .ToListAsync();

        return new PagedResult<Review>
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

    public void Add(Review review) => _context.Reviews.Add(review);
    public void Remove(Review review) => _context.Reviews.Remove(review);

}
