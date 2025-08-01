using System;
using FriendStuff.Features.Auth.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FriendStuff.Features.Auth;

public static class AuthApi
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {

        app.MapPost("/Account/Login", async (
            [FromForm] LoginDto loginData,
            IAuthService authService) =>
        {
            try
            {
                await authService.LoginUser(loginData);
                return Results.LocalRedirect("/");
            }
            catch (ArgumentException ex)
            {
                return Results.Redirect($"/Account/Login?Error={Uri.EscapeDataString(ex.Message)}");
            }
        }).DisableAntiforgery();

        app.MapPost("/Account/Logout", async (
            IAuthService authService) =>
        {
            await authService.Logout();
            return Results.Redirect("/");
        }).DisableAntiforgery().RequireAuthorization();
    }


}
