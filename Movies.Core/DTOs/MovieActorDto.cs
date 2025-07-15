using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.DTOs;

public class MovieActorDto
{
    public int ActorId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int BirthYear { get; set; }
    public string Role { get; set; } = string.Empty;
}
