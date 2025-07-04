using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.DTOs;

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
        var dto = await _mapper
            .ProjectTo<ReviewDto>(_context.Reviews.Where(r => r.MovieId == movieId))
            .ToListAsync();

        if (dto == null)
            return NotFound();

        return Ok(dto);
    }
}


