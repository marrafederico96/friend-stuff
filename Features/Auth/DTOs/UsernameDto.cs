using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Features.Auth.DTOs;

public record UsernameDto
{
    [Required] public string Username { get; set; } = string.Empty;

}
