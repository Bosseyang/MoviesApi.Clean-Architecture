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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MoviesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies() 
            => Ok(_mapper.Map<IEnumerable<MovieDto>>(await _unitOfWork.Movies.GetMoviesAsync()));

        // GET: api/Movies/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id, [FromQuery] bool withactors = false)
        {
            if (!await _unitOfWork.Movies.ExistsAsync(id)) return NotFound();
            return Ok(_mapper.Map<MovieDto>(await _unitOfWork.Movies.GetMovieAsync(id, withactors)));
        }

        // GET: api/Movies/{id}/details
        [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailDto>> GetMovieDetail(int id)
        {
            if (!await _unitOfWork.Movies.ExistsAsync(id)) return NotFound();
            return Ok(_mapper.Map<MovieDetailDto>(await _unitOfWork.Movies.GetAllDetailsAsync(id)));
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, MovieUpdateDto dto)
        {
            var movie = await _unitOfWork.Movies.GetMovieDetailsAsync(id);

            if (!await _unitOfWork.Movies.ExistsAsync(id)) return NotFound();

            _mapper.Map(dto, movie);

            _unitOfWork.Movies.Update(movie!);

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.Movies.ExistsAsync(id)) return NotFound();
         
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

            _unitOfWork.Movies.Add(movie);
            await _unitOfWork.CompleteAsync();

            var movieDto = _mapper.Map<MovieDto>(movie);

            return CreatedAtAction(nameof(GetMovie), new { id = movieDto.Id }, movieDto);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _unitOfWork.Movies.GetMovieAsync(id);
            if (!await _unitOfWork.Movies.ExistsAsync(id)) return NotFound();

            _unitOfWork.Movies.Remove(movie!);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
