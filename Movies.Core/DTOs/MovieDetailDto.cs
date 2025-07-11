using System.Text.Json.Serialization;

namespace Movies.Core.DTOs;

public class MovieDetailDto
{
    //Movie Data
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Genre { get; set; } = string.Empty;
    public int Duration { get; set; }
    //public MovieDto MovieData { get; set; } = new MovieDto();

    //MovieDetails
    public string Synopsis { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public int Budget { get; set; }

    public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<ActorDto> Actors { get; set; } = new List<ActorDto>();
}

