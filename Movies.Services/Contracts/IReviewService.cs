﻿using Movies.Core.DTOs;

namespace Movies.Services.Contracts
{
    public interface IReviewService
    {
        Task AddReviewAsync(int movieId, ReviewCreateDto dto);
        Task<IEnumerable<ReviewDto>?> GetReviewsByMovieAsync(int movieId);
        Task<PagedResult<ReviewDto>> GetPagedReviewsAsync(int movieId, PagingParams pagingParams);
    }
}