using FriendStuff.Data;
using FriendStuff.Domain.Entities;
using FriendStuff.Features.Auth;
using FriendStuff.Features.EventExpense;
using FriendStuff.Features.EventExpense.ExpenseRefund;
using FriendStuff.Features.Group;
using FriendStuff.Features.Group.Member;
using FriendStuff.Features.GroupEvent;
using FriendStuff.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies(o =>
    {
        o.ApplicationCookie?.Configure(cookieOption =>
        {
            cookieOption.Cookie.HttpOnly = true;
            cookieOption.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            cookieOption.ExpireTimeSpan = TimeSpan.FromDays(15);
            cookieOption.SlidingExpiration = true;
            cookieOption.Cookie.SameSite = SameSiteMode.Strict;
            cookieOption.Cookie.MaxAge = TimeSpan.FromDays(15);
        });
    });

builder.Services.AddDbContext<FriendStuffDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityCore<User>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<FriendStuffDbContext>()
    .AddSignInManager();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IGroupMemberService, GroupMemberService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IRefundService, RefundService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAuthEndpoints();
app.UseAntiforgery();

app.Run();
