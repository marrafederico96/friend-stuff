using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Features.Auth.DTOs;

public class RegisterDto
{
    [Required(ErrorMessage = "Username cannot be empty")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "First Name cannot be empty")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last Name cannot be empty")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email cannot be empty")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password cannot be empty")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirm password cannot be empty")]
    [Compare("Password", ErrorMessage = "Password do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;

}
