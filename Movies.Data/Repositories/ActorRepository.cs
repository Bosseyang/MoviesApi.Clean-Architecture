using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Data.Repositories;

public class ActorRepository
{
    private readonly MovieContext _context;
    public ActorRepository(MovieContext context) => _context = context;
    public async Task AddActorToMovieAsync(int movieId, int actorId, string role)
    {
        var movieActor = new MovieActor
        {
            MovieId = movieId,
            ActorId = actorId,
            Role = role
        };

        _context.MovieActors.Add(movieActor);
        await _context.SaveChangesAsync();
    }

    public void AddActorToMovieWithRoleAsync(int movieId)
    {

    }
}
