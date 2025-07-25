using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Features.Auth.DTOs;

public record UserInfoDto
{
    [Required]
    public string Username { get; init; } = string.Empty;

    [Required]
    public string Email { get; init; } = string.Empty;

    [Required]
    public string FirstName { get; init; } = string.Empty;

    [Required]
    public string LastName { get; init; } = string.Empty;

}
