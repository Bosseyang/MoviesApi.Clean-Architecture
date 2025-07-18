using Movies.Core.DTOs;
using Movies.Core.Entities;

namespace Movies.Data.Repositories
{
    public interface IActorRepository
    {
        Task<bool> ActorAlreadyInMovieAsync(int movieId, int actorId);
        Task<bool> ActorExistsAsync(int actorId);
        Task AddActorToMovieAsync(Movie movie, int actorId, string role);
        Task AddActorToMovieAsync(int movieId, int actorId, string role);
        Task AddActorToMovieWithRoleAsync(int movieId, MovieActor movieActor);
        Task<IEnumerable<MovieActor>> GetActorsByMovieAsync(int movieId);
        Task<Movie?> GetMovieWithActorsAsync(int movieId);
        Task<bool> MovieExistsAsync(int movieId);
        Task<PagedResult<Actor>> GetPagedActorsAsync(PagingParams pagingParams);


    }
}