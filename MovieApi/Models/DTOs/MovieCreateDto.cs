using MovieApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs;
//POST
public class MovieCreateDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
    //TODO: Normalize later?
    [Required]
    public string Genre { get; set; } = string.Empty;
    [Range(45, 300)]
    public int Duration { get; set; }
    // MovieDetails
    //public string Synopsis { get; set; } = string.Empty;
    //public string Language { get; set; } = string.Empty;
    //public int Budget { get; set; }

    //Links to MovieDetailDto
    [Required]
    public MovieDetailCreateDto MovieDetails { get; set; } = new();
}
