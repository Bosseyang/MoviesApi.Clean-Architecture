using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.DTOs;
using MovieApi.Models.Entities;
using MovieApi.Controllers;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/movies")]
    //[Produces("application/json")]
    //TODO: Add Swashbuckle.AspNetCore.Annotations 
    public class ActorsController : ControllerBase
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;

        public ActorsController(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // POST /api/movies/{movieId}/actors/{actorId}
        [HttpPost("{movieId}/actors/{actorId}")]
        public async Task<ActionResult<MovieDto>> AddActorToMovie(int movieId, int actorId)
        {

            var movie = await _context.Movies
                .Include(m => m.Actors)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null) return NotFound($"Movie with Id: {movieId} not found");

            var actor = await _context.Actors.FindAsync(actorId);
            if (actor == null) return NotFound($"Actor with Id: {actorId} not found");

            if (movie.Actors.Any(a => a.Id == actorId))
                return BadRequest("Actor already added.");

            movie.Actors.Add(actor);
            await _context.SaveChangesAsync();

            var movieDto = _mapper.Map<MovieDto>(movie);
            return CreatedAtAction(actionName: "GetMovie", 
                                    controllerName: "Movies", 
                                    new { id = movieDto.Id }, movieDto);
        }

        // POST /api/movies/{movieId}/actors
        [HttpPost("{movieId}/actors")]
        public async Task<ActionResult<MovieDto>> AddActorToMovieWithRole(int movieId, MovieActorCreateDto dto)
        {
            if (!MovieExists(movieId)) return NotFound($"Movie with Id: {movieId} not found");

            var actor = await _context.Actors.FindAsync(dto.ActorId);
            if (actor == null) return NotFound($"Actor with Id: {dto.ActorId} not found");

            var actorExists = await _context.MovieActors.AnyAsync(ma => ma.MovieId == movieId && ma.ActorId == dto.ActorId);
            if (actorExists) return BadRequest("Actor is already added to this movie");

            var movieActor = _mapper.Map<MovieActor>(dto);
            movieActor.MovieId = movieId;

            _context.MovieActors.Add(movieActor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }

    }
}

