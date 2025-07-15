using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Core.DTOs;
using Movies.Core.Entities;
using Movies.Data;
using Movies.Data.Repositories;


// TODO: Might want to move the logic etc out of there eventually.
namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class ActorsController : ControllerBase
    {
        private readonly IActorRepository _repository;
        private readonly IMapper _mapper;

        public ActorsController(IActorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        // GET /api/movies/{movieId}/actors
        [HttpGet("{movieId}/actors")]
        public async Task<ActionResult<IEnumerable<MovieActorDto>>> GetActorsByMovie(int movieId)
        {
            if (!await _repository.MovieExistsAsync(movieId))
                return NotFound($"Movie with Id: {movieId} not found");

            var dto = _mapper.Map<IEnumerable<MovieActorDto>>(await _repository.GetActorsByMovieAsync(movieId));

            return Ok(dto);
        }

        // POST /api/movies/{movieId}/actors/{actorId}
        [HttpPost("{movieId}/actors/{actorId}")]
        public async Task<ActionResult<MovieDto>> AddActorToMovie(int movieId, int actorId)
        {
            if (!await _repository.MovieExistsAsync(movieId))
                return NotFound($"Movie with Id: {movieId} not found");

            if (!await _repository.ActorExistsAsync(actorId))
                return NotFound($"Actor with Id: {actorId} not found");

            if (await _repository.ActorAlreadyInMovieAsync(movieId, actorId))
                return BadRequest("Actor already added.");

            var movie = await _repository.GetMovieWithActorsAsync(movieId);
            if (movie == null) return NotFound();

            await _repository.AddActorToMovieAsync(movie, actorId);

            var movieDto = _mapper.Map<MovieDto>(movie);
            return CreatedAtAction("GetMovie", "Movies", new { id = movieDto.Id }, movieDto);
        }

// POST /api/movies/{movieId}/actors
        [HttpPost("{movieId}/actors")]
        public async Task<ActionResult> AddActorToMovieWithRole(int movieId, MovieActorCreateDto dto)
        {
            if (!await _repository.MovieExistsAsync(movieId))
                return NotFound($"Movie with Id: {movieId} not found");

            if (!await _repository.ActorExistsAsync(dto.ActorId))
                return NotFound($"Actor with Id: {dto.ActorId} not found");

            if (await _repository.ActorAlreadyInMovieAsync(movieId, dto.ActorId))
                return BadRequest("Actor already added to this movie.");

            var movieActor = _mapper.Map<MovieActor>(dto);
            await _repository.AddActorToMovieWithRoleAsync(movieId, movieActor);

            return NoContent();
        }
    }
}

