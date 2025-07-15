using Movies.Core.DTOs;
using Movies.Core.Entities;

namespace Movies.Data.Repositories;

public interface IActorRepository
{
    Task<Movie?> GetMovieWithActorsAsync(int movieId);
    Task AddActorToMovieWithRoleAsync(int movieId, MovieActor movieActor);
    Task AddActorToMovieAsync(Movie movie, int actorId);
    Task<IEnumerable<MovieActor>> GetActorsByMovieAsync(int movieId);

    Task<bool> ActorAlreadyInMovieAsync(int movieId, int actorId);
    Task<bool> MovieExistsAsync(int movieId);
    Task<bool> ActorExistsAsync(int actorId);
    
}