using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Services;

using AutoMapper;
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
        var movie = _mapper.Map<Movie>(dto);
        _unitOfWork.Movies.Add(movie);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<MovieDto>(movie);
    }


    public async Task<bool> UpdateAsync(int id, MovieUpdateDto dto)
    {
        var movie = await _unitOfWork.Movies.GetMovieDetailsAsync(id);
        if (movie == null) return false;

        _mapper.Map(dto, movie);
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
