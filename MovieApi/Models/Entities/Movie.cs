namespace MovieApi.Models.Entities;

public class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;
    public int Year { get; set; }
    //TODO: Normalize later?
    public string Genre { get; set; } = null!;
    public int Duration { get; set; }

    // 1:1 Relation with MovieDetails, Each Movie has One MovieDetail
    public MovieDetails MovieDetails { get; set; } = null!;

    // 1:M Relation with Review, Each Movie has Many Reviews
    public List<Review> Reviews { get; set; } = null!;

    // M:N Relation with Actor through connection? tabel ActorMovie, Each Movie can have several Actors and Each Actor can be in Several Movies
    public ICollection<Actor> Actors { get; set; } = new List<Actor>();

}
