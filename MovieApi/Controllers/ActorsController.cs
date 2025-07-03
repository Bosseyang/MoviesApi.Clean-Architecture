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
    [ApiController]
    [Route("api/actors")]
    //[Produces("application/json")]
    //TODO: Add Swashbuckle.AspNetCore.Annotations 
    public class ActorsController : ControllerBase
    {
        private readonly MovieContext _context;

        public ActorsController(MovieContext context)
        {
            _context = context;
        }

        // POST /api/movies/{movieId}/actors/{actorId}
        [HttpPost("/api/movies/{movieId}/actors/{actorId}")]
        public async Task<IActionResult> AddActorToMovie(int movieId, int actorId)
        {
            var movie = await _context.Movies
                .Include(m => m.Actors)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            var actor = await _context.Actors.FindAsync(actorId);

            if (movie == null)
                return NotFound($"Movie with id {movieId} not found.");

            if (actor == null)
                return NotFound($"Actor with id {actorId} not found.");

            if (movie.Actors.Any(a => a.Id == actorId))
                return BadRequest("Actor already exist in this movie.");

            movie.Actors.Add(actor);
            await _context.SaveChangesAsync();

            return Ok($"{actor.Name} with actorID: {actorId} added to movie: {movie.Title} with movieID: {movieId}");
        }
    }
}

