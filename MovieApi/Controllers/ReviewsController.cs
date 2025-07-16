using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Core.DomainContracts;
using Movies.Core.DTOs;
using Movies.Core.Entities;

namespace MovieApi.Controllers;

[ApiController]
[Route("api/movies")]
//[Produces("application/json")]
//TODO: Add Swashbuckle.AspNetCore.Annotations 
public class ReviewsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ReviewsController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    // GET: /api/movies/{movieId}/reviews
    [HttpGet("{movieId}/reviews")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(int movieId)
    {
        if (!await _unitOfWork.Reviews.MovieExistsAsync(movieId))
            return NotFound($"Movie with Id: {movieId} does not exist");

        return Ok(_mapper.Map<IEnumerable<ReviewDto>>(await _unitOfWork.Reviews.GetReviewsByMovieAsync(movieId)));
    }

    // POST: /api/movies/{movieId}/reviews
    [HttpPost("{movieId}/reviews")]
    public async Task<IActionResult> AddReview(int movieId, ReviewCreateDto dto)
    {
        var exists = await _unitOfWork.Movies.ExistsAsync(movieId);
        if (!exists) return NotFound($"Movie with Id: {movieId} does not exist");

        var review = _mapper.Map<Review>(dto);
        review.MovieId = movieId;

        _unitOfWork.Reviews.Add(review);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}


