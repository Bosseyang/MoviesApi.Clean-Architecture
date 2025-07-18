using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Services;

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Core.DomainContracts;
using Movies.Core.DTOs;
using Movies.Core.Entities;
using Movies.Services.Contracts;

public class MovieService : IMovieService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedResult<MovieDto>> GetPagedMoviesAsync(PagingParams pagingParams)
    {
        var pagedMovies = await _unitOfWork.Movies.GetPagedMoviesAsync(pagingParams);

        return new PagedResult<MovieDto>
        {
            Data = _mapper.Map<IEnumerable<MovieDto>>(pagedMovies.Data),
            Meta = pagedMovies.Meta
        };
    }

    public async Task<IEnumerable<MovieDto>> GetMoviesAsync()
    {
        var movies = await _unitOfWork.Movies.GetMoviesAsync();
        return _mapper.Map<IEnumerable<MovieDto>>(movies);
    }

    public async Task<MovieDto?> GetMovieByIdAsync(int id, bool withActors)
    {
        if (!await _unitOfWork.Movies.ExistsAsync(id)) return null;
        var movie = await _unitOfWork.Movies.GetMovieAsync(id, withActors);
        return _mapper.Map<MovieDto>(movie);
    }
    public async Task<MovieDetailDto?> GetMovieDetailAsync(int id)
    {
        if (!await _unitOfWork.Movies.ExistsAsync(id)) return null;
        var movie = await _unitOfWork.Movies.GetAllMovieDetailsAsync(id);
        return _mapper.Map<MovieDetailDto>(movie);
    }

    public async Task<MovieDto> CreateAsync(MovieCreateDto dto)
    {
        var genre = await _unitOfWork.Genres.GetByNameAsync(dto.Genre);
        if (genre == null)
            throw new ProblemDetailsException(
                400, $"Genre with name '{dto.Genre}' does not exist."
            );
        if (dto.MovieDetails.Budget < 0)
            throw new ProblemDetailsException(400, $"Budget cannot be negative");


        if (genre?.Name.ToLower() == "documentary")
        {
            if (dto.ActorIds.Count > 10)
                throw new ProblemDetailsException(400, "Documentaries may have max 10 actors.");

            if (dto.MovieDetails.Budget > 1_000_000)
                throw new ProblemDetailsException(400, "Documentaries may not exceed 1 million in budget.");
        }
        if (await _unitOfWork.Movies.TitleExistsAsync(dto.Title))
            throw new ProblemDetailsException(400, "A movie with this title already exists.");


        var movie = _mapper.Map<Movie>(dto);
        movie.GenreId = genre!.Id;
        movie.Genre = genre;

        movie.MovieActors = dto.ActorIds.Select(actorId => new MovieActor
        {
            ActorId = actorId,
            Role = "Actor"
        }).ToList();

        _unitOfWork.Movies.Add(movie);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<MovieDto>(movie);
    }

    public async Task<bool> UpdateAsync(int id, MovieUpdateDto dto)
    {
        var movie = await _unitOfWork.Movies.GetMovieDetailsAsync(id);
        if (movie == null)
            throw new ProblemDetailsException(400, $"Movie with id '{id}' does not exist.");

        var genre = await _unitOfWork.Genres.GetByNameAsync(dto.Genre);
        if (genre == null)
            throw new ProblemDetailsException(
                400, $"Genre with name '{dto.Genre}' does not exist."
            );
        if (dto.Budget < 0)
            throw new ProblemDetailsException(400, $"Budget cannot be negative");


        if (genre?.Name.ToLower() == "documentary")
        {
            if (dto.MovieActors.Count > 10)
                throw new ProblemDetailsException(400, "Documentaries may have max 10 actors.");

            if (dto.Budget > 1_000_000)
                throw new ProblemDetailsException(400, "Documentaries may not exceed 1 million in budget.");
        }


        _mapper.Map(dto, movie);
        movie.GenreId = genre!.Id;
        movie.Genre = genre;

        _unitOfWork.Movies.Update(movie);
        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var movie = await _unitOfWork.Movies.GetMovieAsync(id);
        if (movie == null) return false;

        _unitOfWork.Movies.Remove(movie);
        await _unitOfWork.CompleteAsync();
        return true;
    }
}
