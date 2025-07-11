using System.ComponentModel.DataAnnotations;

namespace Movies.Core.DTOs;

public class MovieDetailCreateDto
{
    //[Required]
    public string Synopsis { get; set; } = string.Empty;

    //[Required]
    public string Language { get; set; } = string.Empty;

    [Range(0, int.MaxValue)]
    public int Budget { get; set; }
}
