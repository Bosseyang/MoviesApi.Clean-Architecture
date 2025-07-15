using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.DTOs;
using Movies.Data;

namespace MovieApi.Controllers;

[ApiController]
[Route("api/movies")]
//[Produces("application/json")]
//TODO: Add Swashbuckle.AspNetCore.Annotations 
public class ReviewsController : ControllerBase
{
    private readonly IReviewRepository _repository;
    private readonly IMapper _mapper;

    public ReviewsController(IReviewRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    // GET: /api/movies/{movieId}/reviews
    [HttpGet("{movieId}/reviews")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(int movieId)
    {
        if (!await _repository.MovieExistsAsync(movieId))
            return NotFound($"Movie with Id: {movieId} does not exist");

        return Ok(_mapper.Map<IEnumerable<ReviewDto>>(await _repository.GetReviewsByMovieAsync(movieId)));
    }
}


