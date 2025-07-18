using AutoMapper;
using Movies.Core.DomainContracts;
using Movies.Core.DTOs;
using Movies.Core.Entities;

public class ActorService : IActorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ActorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<PagedResult<ActorDto>> GetPagedActorsAsync(PagingParams pagingParams)
    {
        var paged = await _unitOfWork.Actors.GetPagedActorsAsync(pagingParams);

        return new PagedResult<ActorDto>
        {
            Data = _mapper.Map<IEnumerable<ActorDto>>(paged.Data),
            Meta = paged.Meta
        };
    }
    public async Task<IEnumerable<MovieActorDto>?> GetActorsByMovieAsync(int movieId)
    {
        if (!await _unitOfWork.Actors.MovieExistsAsync(movieId)) return null;

        var movieActors = await _unitOfWork.Actors.GetActorsByMovieAsync(movieId);
        return _mapper.Map<IEnumerable<MovieActorDto>>(movieActors);
    }


    public async Task<MovieDto?> AddActorToMovieAsync(int movieId, int actorId)
    {
        if (!await _unitOfWork.Movies.ExistsAsync(movieId))
            throw new ProblemDetailsException(400, $"Movie with id: {movieId} does not exist");
        if (!await _unitOfWork.Actors.ActorExistsAsync(actorId))
            throw new ProblemDetailsException(400, $"Actor with id: {actorId} does not exist");
        if (await _unitOfWork.Actors.ActorAlreadyInMovieAsync(movieId, actorId))
            throw new ProblemDetailsException(400, $"Actor with id: {actorId} is already in movie with id: {movieId}");

        var movie = await _unitOfWork.Actors.GetMovieWithActorsAsync(movieId);
        if (movie is null) throw new ProblemDetailsException(400, $"Movie with id: {movieId} does not exist");

        await _unitOfWork.Actors.AddActorToMovieAsync(movie, actorId, "Actor");

        return _mapper.Map<MovieDto>(movie);
    }

    public async Task<bool?> AddActorToMovieWithRoleAsync(int movieId, MovieActorCreateDto dto)
    {
        if (!await _unitOfWork.Actors.MovieExistsAsync(movieId)) return null;
        if (!await _unitOfWork.Actors.ActorExistsAsync(dto.ActorId)) return null;
        if (await _unitOfWork.Actors.ActorAlreadyInMovieAsync(movieId, dto.ActorId)) return false;

        var movieActor = _mapper.Map<MovieActor>(dto);
        await _unitOfWork.Actors.AddActorToMovieWithRoleAsync(movieId, movieActor);

        return true;
    }
}
