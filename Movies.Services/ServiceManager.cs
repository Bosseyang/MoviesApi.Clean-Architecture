using AutoMapper;
using Movies.Core.DomainContracts;
using Movies.Services.Contracts;

namespace Movies.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IMovieService> _movieService;
        private readonly Lazy<IActorService> _actorService;
        private readonly Lazy<IReviewService> _reviewService;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _movieService = new(() => new MovieService(unitOfWork, mapper));
            _actorService = new(() => new ActorService(unitOfWork, mapper));
            _reviewService = new(() => new ReviewService(unitOfWork, mapper));
        }

        public IMovieService Movies => _movieService.Value;
        public IActorService Actors => _actorService.Value;
        public IReviewService Reviews => _reviewService.Value;
    }
}

