using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Features.Auth.DTOs;

public record UsernameDto
{
    [Required]
    public required string Username { get; init; }

}
