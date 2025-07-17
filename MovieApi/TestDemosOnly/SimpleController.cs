using Microsoft.AspNetCore.Mvc;
using Movies.Core.DTOs;

namespace MovieApi.TestDemosOnly;

[ApiController]
[Route("api/simple")]
public class SimpleController : ControllerBase
{
    public SimpleController()
    {

    }

    [HttpGet]
    public async Task<ActionResult<MovieDto>> GetMovie()
    {
        return Ok();
    }

}
