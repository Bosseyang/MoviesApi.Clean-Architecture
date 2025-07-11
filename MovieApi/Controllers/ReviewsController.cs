using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Core.DTOs;
using Movies.Data;

namespace MovieApi.Controllers;

[ApiController]
[Route("api/movies")]
//[Produces("application/json")]
//TODO: Add Swashbuckle.AspNetCore.Annotations 
public class ReviewsController : ControllerBase
{
    private readonly MovieContext _context;
    private readonly IMapper _mapper;

    public ReviewsController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    //GET: /api/movies/{movieId}/reviews
    [HttpGet("{movieId}/reviews")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(int movieId)
    {
        var movieExists = await _context.Movies.AnyAsync(m => m.Id == movieId);
        if (!movieExists) return NotFound($"Movie with Id: {movieId} does not exist");

        var dto = await _mapper
            .ProjectTo<ReviewDto>(_context.Reviews.Where(r => r.MovieId == movieId))
            .ToListAsync();

        return Ok(dto);
    }
}


