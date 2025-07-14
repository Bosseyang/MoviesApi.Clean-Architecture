namespace Movies.Core.Entities;

public class Actor
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int BirthYear { get; set; }

    // ForeignKey to Movie for Connection Table
    //public int MovieId { get; set; }

    // Navigation property N:M throught ActorMovie Connection Table
    //public ICollection<Movie> Movies { get; set; } = new List<Movie>();

    public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
}
