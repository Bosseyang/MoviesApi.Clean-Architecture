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

        // POST: /api/movies/{movieId}/actors/{actorId}
        // TODO: (lägg till aktör till film med roll)
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Actor>> PostActor(Actor actor)
        //{
        //    _context.Actors.Add(actor);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetMovie", new { id = actor.Id }, actor);
        //}


        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
    }
}

