using Movies.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Movies.Core.DTOs;
//POST
public class MovieCreateDto
{
    [Required(ErrorMessage = "Title is a required field.")]
    public string Title { get; set; } = string.Empty;
    [Range(1900, 2025)]
    public int Year { get; set; }
    [Required]
    [MaxLength(255, ErrorMessage = "Max length is 255.")]
    public string Genre { get; set; } = string.Empty;
    [Range(45, 300)]
    public int Duration { get; set; }

    //Navigation prop
    //[Required]
    public MovieDetailCreateDto MovieDetails { get; set; } = new();
    public List<MovieActorDto> MovieActors { get; set; } = new();
}
