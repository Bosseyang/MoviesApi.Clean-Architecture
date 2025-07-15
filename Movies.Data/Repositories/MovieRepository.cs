using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.DTOs;
using Movies.Core.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Movies.Data.Repositories;

public class MovieRepository : IMovieRepository
{

    private readonly MovieContext _context;
    public MovieRepository(MovieContext context) => _context = context;

    public async Task<IEnumerable<Movie>> GetAllAsync() => await _context.Movies.ToListAsync();

    public async Task<Movie?> GetAsync(int id, bool withActors = false)
    {
        var query = _context.Movies.AsQueryable();
        if(withActors)
        {
            query = query
                .Include(m => m.MovieActors)
                .ThenInclude(m => m.Actor);
        }
        return await query.FirstOrDefaultAsync(m => m.Id == id);

    }
    public async Task<Movie?> GetAllDetailsAsync(int id)
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
