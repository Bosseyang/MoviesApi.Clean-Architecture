using System.ComponentModel.DataAnnotations;

namespace Movies.Core.DTOs;
//PUT
public class MovieUpdateDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    [Range(1900, 2025)]
    public int Year { get; set; }
    //TODO: Normalize later?
    [Required]
    public string Genre { get; set; } = string.Empty;
    [Range(1, 400)]
    public int Duration { get; set; }
    //Updating MovieDetails
    public string Synopsis { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    [Range(0, int.MaxValue)]
    public int Budget { get; set; }

    public List<MovieActorDto> MovieActors { get; set; } = new();
}
