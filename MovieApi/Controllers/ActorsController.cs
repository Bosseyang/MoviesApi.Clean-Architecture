using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Movies.Core.DTOs;
using Movies.Services.Contracts;
using System.Text.Json;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class ActorsController : ControllerBase
    {
        private readonly IServiceManager _services;

        public ActorsController(IServiceManager services) => _services = services;

        // GET: /api/movies/actors
        [HttpGet("actors")]
        public async Task<IActionResult> GetActors([FromQuery] PagingParams pagingParams)
        {
            var result = await _services.Actors.GetPagedActorsAsync(pagingParams);
            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(result.Meta));
            return Ok(result);
        }

        // GET: /api/movies/{movieId}/actors
        [HttpGet("{movieId}/actors")]
        public async Task<ActionResult<IEnumerable<MovieActorDto>>> GetActorsByMovie(int movieId)
        {
            var result = await _services.Actors.GetActorsByMovieAsync(movieId);
            return result is null ? NotFound($"Movie with Id: {movieId} not found") : Ok(result);
        }

        // POST: /api/movies/{movieId}/actors/{actorId}
        [HttpPost("{movieId}/actors/{actorId}")]
        public async Task<ActionResult<MovieDto>> AddActorToMovie(int movieId, int actorId)
        {
            //var result = await _services.Actors.AddActorToMovieAsync(movieId, actorId);
            //return result switch
            //{
            //    null => NotFound(),
            //    MovieDto dto => CreatedAtAction("GetMovie", "Movies", new { id = dto.Id }, dto)
            //};

            try
            {
                await _services.Actors.AddActorToMovieAsync(movieId, actorId);
                return NoContent();
            }
            catch (ProblemDetailsException ex)
            {
                return StatusCode(ex.ProblemDetails.Status ?? 400, ex.ProblemDetails);
            }
        }

        // POST: /api/movies/{movieId}/actors
        [HttpPost("{movieId}/actors")]
        public async Task<ActionResult> AddActorToMovieWithRole(int movieId, MovieActorCreateDto dto)
        {
            var success = await _services.Actors.AddActorToMovieWithRoleAsync(movieId, dto);

            return success switch
            {
                null => NotFound(),
                false => BadRequest("Actor already added to this movie."),
                true => NoContent()
            };
        }
    }
}
