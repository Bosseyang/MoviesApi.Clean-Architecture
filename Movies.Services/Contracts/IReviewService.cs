using Movies.Core.DTOs;

namespace Movies.Services.Contracts
{
    public interface IReviewService
    {
        Task<bool?> AddReviewAsync(int movieId, ReviewCreateDto dto);
        Task<IEnumerable<ReviewDto>?> GetReviewsByMovieAsync(int movieId);
    }
}