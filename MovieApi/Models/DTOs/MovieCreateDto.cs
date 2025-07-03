using MovieApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs;
//POST
public class MovieCreateDto
{
    [Required]
    public string Title { get; set; } = null!;
    [Range(1900, 2025)]
    public int Year { get; set; }
    //TODO: Normalize later?
    [Required]
    public string Genre { get; set; } = null!;
    [Range(45, 300)]
    public int Duration { get; set; }
}
