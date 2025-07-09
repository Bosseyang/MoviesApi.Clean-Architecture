using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.DTOs;

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
