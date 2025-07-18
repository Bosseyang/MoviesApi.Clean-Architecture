using Microsoft.AspNetCore.Mvc;
using Movies.Core.DTOs;
using Movies.Services;
using Movies.Services.Contracts;

[Route("api/movies")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IServiceManager _services;

    public MoviesController(IServiceManager services) => _services = services;

    // GET: api/movies?pagingParams=true
    [HttpGet]
    public async Task<IActionResult> GetMovies([FromQuery] PagingParams pagingParams)
    {
        var result = await _services.Movies.GetPagedMoviesAsync(pagingParams);

        Response.Headers.Append("X-Pagination", System.Text.Json.JsonSerializer.Serialize(result.Meta));

        return Ok(result);
    }

    //// GET: api/movies
    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
    //    => Ok(await _services.Movies.GetMoviesAsync());

    // GET: api/movies/1
    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetMovie(int id, [FromQuery] bool withactors = false)
    {
        var result = await _services.Movies.GetMovieByIdAsync(id, withactors);
        return result is null ? NotFound() : Ok(result);
    }

    // GET: api/movies/1/details
    [HttpGet("{id}/details")]
    public async Task<ActionResult<MovieDetailDto>> GetMovieDetail(int id)
    {
        var result = await _services.Movies.GetMovieDetailAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    // PUT: api/movies/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(int id, MovieUpdateDto dto)
    {
        try
        {
            var updated = await _services.Movies.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }
        catch (ProblemDetailsException ex)
        {
            // Return ProblemDetails with correct content and status code
            return Problem(
                detail: $"Genre with name '{dto.Genre}' does not exist.",
                statusCode: ex.ProblemDetails.Status,
                title: "Validation Error"
            );
        }
    }

    // POST: api/movies
    [HttpPost]
    public async Task<ActionResult<MovieDto>> CreateMovie(MovieCreateDto dto)
    {
        try
        {
            var created = await _services.Movies.CreateAsync(dto);
            return CreatedAtAction(nameof(GetMovie), new { id = created.Id }, created);
        }
        catch (ProblemDetailsException ex)
        {
            return StatusCode(ex.ProblemDetails.Status ?? 400, ex.ProblemDetails);
        }

    }

    // DELETE: api/movies/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var deleted = await _services.Movies.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
