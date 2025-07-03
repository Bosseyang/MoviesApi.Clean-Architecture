using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieApi.Models.DTOs;

//GET
public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int Year { get; set; }
    //TODO: Normalize later?
    public string Genre { get; set; } = null!;
    public int Duration { get; set; }
    //For query string, if we use GET without query string, we don't want to show Actors=Null
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ActorDto>? Actors { get; set; }

}
