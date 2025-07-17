using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Core.DomainContracts;
using Movies.Core.DTOs;

namespace MovieApi.TestDemosOnly;

[ApiController]
[Route("api/repo")]
public class RepositoryController : ControllerBase
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;

    public RepositoryController(IMovieRepository movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<MovieDto>> GetMovie()
    {
        return Ok();
    }

}