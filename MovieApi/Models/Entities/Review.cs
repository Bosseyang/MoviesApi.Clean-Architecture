using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.Entities;

public class Review
{
    public int Id { get; set; }
    public string ReviewerName { get; set; } = null!;
    public string Comment { get; set; } = null!;
    [Range(1, 5)]
    public int Rating { get; set; }

    // ForeignKey from Movie 1:M
    public int MovieId { get; set; }

    // Navigation back to Movie 1:M relation
    public Movie Movie { get; set; } = null!;
}
