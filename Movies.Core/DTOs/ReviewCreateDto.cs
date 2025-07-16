using System.ComponentModel.DataAnnotations;

namespace Movies.Core.DTOs;

public class ReviewCreateDto
{
    [Required]
    public string ReviewerName { get; set; } = string.Empty;
    [Required]
    public string Comment { get; set; } = string.Empty;
    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }

}