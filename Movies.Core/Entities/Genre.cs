using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Entities;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Navigationsproperty
    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
