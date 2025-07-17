using Microsoft.AspNetCore.Mvc;
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

    //Task<IEnumerable<Movie>> GetAllAsync([FromQuery] bool withactors = false);
    Task<Movie?> GetMovieAsync(int id, bool withActors = false);
    Task<Movie?> GetAllDetailsAsync(int id);
    Task<Movie?> GetMovieDetailsAsync(int id);
    //Task<bool> AnyAsync(int id);
    Task<bool> ExistsAsync(int id);

    void Add(Movie movie);
    void Update(Movie movie);
    void Remove(Movie movie);

}
