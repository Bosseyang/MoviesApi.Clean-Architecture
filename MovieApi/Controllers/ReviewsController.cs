using Microsoft.AspNetCore.Mvc;
using Movies.Core.DTOs;
using Movies.Services.Contracts;

namespace MovieApi.Controllers;

[ApiController]
[Route("api/movies")]
public class ReviewsController : ControllerBase
{
    private readonly IServiceManager _services;

    public ReviewsController(IServiceManager services) => _services = services;

    // GET: /api/movies/{movieId}/reviews
    [HttpGet("{movieId}/reviews")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(int movieId)
    {
        var result = await _services.Reviews.GetReviewsByMovieAsync(movieId);
        return result is null ? NotFound($"Movie with Id: {movieId} does not exist") : Ok(result);
    }

    // POST: /api/movies/{movieId}/reviews
    [HttpPost("{movieId}/reviews")]
    public async Task<IActionResult> AddReview(int movieId, ReviewCreateDto dto)
    {
        var success = await _services.Reviews.AddReviewAsync(movieId, dto);
        return success switch
        {
            null => NotFound($"Movie with Id: {movieId} does not exist"),
            false => BadRequest(),
            true => NoContent()
        };
    }
}
