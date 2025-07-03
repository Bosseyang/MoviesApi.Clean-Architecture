namespace MovieApi.Models.DTOs;

public class MovieDetailDto
{
    //Movie Data
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int Year { get; set; }
    public string Genre { get; set; } = null!;
    public int Duration { get; set; }

    //MovieDetails
    public string Synopsis { get; set; } = null!;
    public string Language { get; set; } = null!;
    public int Budget { get; set; }

    public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
    public List<ActorDto> Actors { get; set; } = new List<ActorDto>();
}

