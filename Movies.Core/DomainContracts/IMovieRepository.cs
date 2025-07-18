using Microsoft.AspNetCore.Mvc;
using Movies.Core.DTOs;
using Movies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.DomainContracts;

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetMoviesAsync();
    Task<PagedResult<Movie>> GetPagedMoviesAsync(PagingParams pagingParams);
    //Task<IEnumerable<Movie>> GetAllAsync([FromQuery] bool withactors = false);
    Task<Movie?> GetMovieAsync(int id, bool withActors = false);
    Task<Movie?> GetAllMovieDetailsAsync(int id);
    Task<Movie?> GetMovieDetailsAsync(int id);
    //Task<bool> AnyAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> TitleExistsAsync(string title);

    void Add(Movie movie);
    void Update(Movie movie);
    void Remove(Movie movie);

}
