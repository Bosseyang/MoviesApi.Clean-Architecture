using Movies.Core.DTOs;

public interface IActorService
{
    Task<MovieDto?> AddActorToMovieAsync(int movieId, int actorId);
    Task<bool?> AddActorToMovieWithRoleAsync(int movieId, MovieActorCreateDto dto);
    Task<IEnumerable<MovieActorDto>?> GetActorsByMovieAsync(int movieId);
    Task<PagedResult<ActorDto>> GetPagedActorsAsync(PagingParams pagingParams);
}