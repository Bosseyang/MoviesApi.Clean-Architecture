namespace MovieApi.Models.Entities;

public class Actor
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int BirthYear { get; set; }

    // ForeignKey to Movie
    public int MovieId { get; set; }

    // Navigation property N:M throught MovieActor
    public List<Actor> MovieActors { get; set; } = new List<Actor>();
}
