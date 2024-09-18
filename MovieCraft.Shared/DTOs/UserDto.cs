using System.ComponentModel.DataAnnotations;

namespace MovieCraft.Shared.DTOs;

public class UserDto
{
    [Required]
    public string UserId { get; set; } = default!;

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name can have a maximum of 100 characters.")]
    public string Name { get; set; } = default!;
}
