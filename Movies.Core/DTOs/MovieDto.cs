using System.Text.Json.Serialization;

namespace Movies.Core.DTOs;

//GET
public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
    //TODO: Normalize later?
    public string Genre { get; set; } = string.Empty;
    public int Duration { get; set; }
    //For query string, if we use GET without query string, we don't want to show Actors=Null
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<ActorDto>? Actors { get; set; } = new List<ActorDto>();

}
