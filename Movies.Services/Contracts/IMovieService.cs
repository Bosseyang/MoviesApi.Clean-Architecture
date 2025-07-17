using Movies.Core.DTOs;

namespace Movies.Services.Contracts
{
    public interface IMovieService
    {
        Task<MovieDto> CreateAsync(MovieCreateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<MovieDto?> GetMovieByIdAsync(int id, bool withActors);
        Task<MovieDetailDto?> GetMovieDetailAsync(int id);

        Task<IEnumerable<MovieDto>> GetMoviesAsync();
        Task<bool> UpdateAsync(int id, MovieUpdateDto dto);
    }
}