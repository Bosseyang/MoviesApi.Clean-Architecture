namespace MovieApi.Models.Entities;

public class MovieDetails
{
    public int Id { get; set; }
    public string Synopsis { get; set; } = null!;
    public string Language { get; set; } = null!;
    public int Budget { get; set; }

    // ForeignKey for Movie 1:1 relation
    public int MovieId { get; set; }

    // Navigation property 1:1 relation to Movie
    public Movie Movie { get; set; } = null!;
}
