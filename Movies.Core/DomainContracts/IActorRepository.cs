using Movies.Core.Entities;

namespace Movies.Data.Repositories;

public interface IActorRepository
{
    Task<Movie?> GetMovieWithActorsAsync(int movieId);


}