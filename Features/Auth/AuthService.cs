using FriendStuff.Domain.Entities;
using FriendStuff.Features.Auth.DTOs;
using Microsoft.AspNetCore.Identity;

namespace FriendStuff.Features.Auth;

public class AuthService(UserManager<User> userManager, SignInManager<User> signInManager) : IAuthService
{
    public async Task RegisterUser(RegisterDto registerDto)
    {
        var user = new User
        {
            UserName = registerDto.Username.Trim(),
            Email = registerDto.Email.Trim(),
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName
        };

        var result = await userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new ArgumentException($"Registration failed: {errors}");
        }
    }

    public async Task LoginUser(LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email) ?? throw new ArgumentException("Wrong credentials");

        var result = await signInManager.PasswordSignInAsync(user, loginDto.Password, isPersistent: false, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            throw new ArgumentException("Wrong credentials");
        }
    }

    public async Task DeleteUser(UsernameDto usernameDto)
    {
        var user = await userManager.FindByNameAsync(usernameDto.Username.Trim());
        if (user == null)
            throw new ArgumentException("User not found");

        var result = await userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Failed to delete user: {errors}");
        }
    }

    public async Task<UserInfoDto> GetUserInfo(string username)
    {
        var user = await userManager.FindByNameAsync(username) ?? throw new ArgumentException("User not found");
        return new UserInfoDto
        {
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.UserName!
        };
    }
}
