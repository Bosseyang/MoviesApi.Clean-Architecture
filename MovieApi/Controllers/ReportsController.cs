using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Data;

namespace MovieApi.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportsController : ControllerBase
{
    private readonly MovieContext _context;
    private readonly IMapper _mapper;

    public ReportsController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}
