using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Core.DomainContracts;
using Movies.Core.DTOs;
using Movies.Core.Entities;
using Movies.Data;

namespace MovieApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    //[Produces("application/json")]
    //TODO: Add Swashbuckle.AspNetCore.Annotations 
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;
        private readonly IMovieRepository _repository;

        public MoviesController(MovieContext context, IMapper mapper, IMovieRepository repository)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies() 
            => Ok(_mapper.Map<IEnumerable<MovieDto>>(await _repository.GetAllAsync()));

        // GET: api/Movies/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id, [FromQuery] bool withactors = false)
        {
            if (!await _repository.ExistsAsync(id)) return NotFound();
            return Ok(_mapper.Map<MovieDto>(await _repository.GetAsync(id, withactors)));
        }

        // GET: api/Movies/{id}/details
        [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailDto>> GetMovieDetail(int id)
        {
            if (!await _repository.ExistsAsync(id)) return NotFound();
            return Ok(_mapper.Map<MovieDetailDto>(await _repository.GetAllDetailsAsync(id)));
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, MovieUpdateDto dto)
        {
            var movie = await _repository.GetMovieDetailsAsync(id);

            if (!await _repository.ExistsAsync(id)) return NotFound();

            _mapper.Map(dto, movie);

            _repository.Update(movie!);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.ExistsAsync(id)) return NotFound();
         
                else throw;
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovieDto>> CreateMovie(MovieCreateDto dto)
        {
            var movie = _mapper.Map<Movie>(dto);

            _repository.Add(movie);
            await _context.SaveChangesAsync();

            var movieDto = _mapper.Map<MovieDto>(movie);

            return CreatedAtAction(nameof(GetMovie), new { id = movieDto.Id }, movieDto);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _repository.GetAsync(id);
            if (!await _repository.ExistsAsync(id)) return NotFound();

            _repository.Remove(movie!);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
