namespace MovieApi.Models.Entities;

public class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;
    public int Year { get; set; }
    //TODO: Normalize later?
    public string Genre { get; set; } = null!;
    public int Duration { get; set; }

}
