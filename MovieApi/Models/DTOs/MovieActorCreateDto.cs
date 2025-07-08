using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.DTOs;

public class MovieActorCreateDto
{
    public int ActorId { get; set; }

    [Required(ErrorMessage = "Role is a required field")]
    [MaxLength(128 , ErrorMessage = $"Max Length is 128")]
    public string Role {  get; set; } = string.Empty;

}
