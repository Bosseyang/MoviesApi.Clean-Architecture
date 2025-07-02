namespace MovieApi.Models.Entities;

public class Actor
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int BirthYear { get; set; }

    // ForeignKey to Movie
    public int MovieId { get; set; }

    // Navigation N:M throught MovieActor
    //public List<MovieActor> MovieActors { get; set; }
}
