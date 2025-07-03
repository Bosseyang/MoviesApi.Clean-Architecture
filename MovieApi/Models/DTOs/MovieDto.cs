using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs;

//GET
public class MovieDto
{
    public string Title { get; set; } = null!;
    public int Year { get; set; }
    //TODO: Normalize later?
    public string Genre { get; set; } = null!;
    public int Duration { get; set; }

}
