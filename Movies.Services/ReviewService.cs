using AutoMapper;
using Movies.Core.DomainContracts;
using Movies.Core.DTOs;
using Movies.Core.Entities;
using Movies.Services.Contracts;

namespace Movies.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewDto>?> GetReviewsByMovieAsync(int movieId)
        {
            if (!await _unitOfWork.Reviews.MovieExistsAsync(movieId)) return null;
            return _mapper.Map<IEnumerable<ReviewDto>>(await _unitOfWork.Reviews.GetReviewsByMovieAsync(movieId));
        }

        public async Task<bool?> AddReviewAsync(int movieId, ReviewCreateDto dto)
        {
            if (!await _unitOfWork.Movies.ExistsAsync(movieId)) return null;

            var review = _mapper.Map<Review>(dto);
            review.MovieId = movieId;

            _unitOfWork.Reviews.Add(review);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
