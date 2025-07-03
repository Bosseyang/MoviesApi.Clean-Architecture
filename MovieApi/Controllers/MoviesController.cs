using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.DTOs;
using MovieApi.Models.Entities;

namespace MovieApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    //[Produces("application/json")]
    //TODO: Add Swashbuckle.AspNetCore.Annotations 
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies([FromQuery] bool withActors = false)
        {
            var movies = await _context.Movies
                .Select(m => new MovieDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Year = m.Year,
                    Genre = m.Genre,
                    Duration = m.Duration
                }).ToListAsync();

            return Ok(movies);
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id, [FromQuery] bool withActors = false)
        {
            var query = _context.Movies.AsQueryable();

            if (withActors)
                query = query.Include(m => m.Actors);

            var movie = await query.FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
                return NotFound();

            var dto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Genre = movie.Genre,
                Duration = movie.Duration
            };

            if (withActors)
            {
                dto.Actors = movie.Actors.Select(a => new ActorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    BirthYear = a.BirthYear
                }).ToList();
            }

            return Ok(dto);
        }

        // GET: api/Movies/{id}/details
        [HttpGet("{id}/details")]
        public async Task<ActionResult<IEnumerable<MovieDetailDto>>> GetMovieDetail(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieDetails)
                .Include(m => m.Actors)
                .Include(m => m.Reviews)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
                return NotFound();

            var dto = new MovieDetailDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Genre = movie.Genre,
                Duration = movie.Duration,
                //From MovieDetails
                Synopsis = movie.MovieDetails.Synopsis,
                Language = movie.MovieDetails.Language,
                Budget = movie.MovieDetails.Budget,
                //From Actors
                Actors = movie.Actors.Select(a => new ActorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    BirthYear = a.BirthYear,
                }).ToList(),
                //From Reviews
                Reviews = movie.Reviews.Select(r => new ReviewDto
                {
                    Id = r.Id,
                    ReviewerName = r.ReviewerName,
                    Comment = r.Comment,
                    Rating = r.Rating,

                }).ToList()


            };

            return Ok(dto);
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
