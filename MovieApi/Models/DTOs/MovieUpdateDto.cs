using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs;
//PUT
public class MovieUpdateDto
{
    [Required]
    public string Title { get; set; } = null!;
    [Range(1900, 2025)]
    public int Year { get; set; }
    //TODO: Normalize later?
    [Required]
    public string Genre { get; set; } = null!;
    [Range(1, 400)]
    public int Duration { get; set; }
    //Updating MovieDetails
    public string Synopsis { get; set; } = null!;
    public string Language { get; set; } = null!;
    [Range(0, int.MaxValue)]
    public int Budget {  get; set; }
}
