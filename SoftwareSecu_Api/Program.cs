using BlazorSoftwareSecu.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"C:\Temp\BlazorSoftwareSecuKeys"))
    .SetApplicationName("BlazorSoftwareSecu");

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddCookie(IdentityConstants.ApplicationScheme, options =>
    {
        options.Cookie.Name = ".AspNetCore.Identity.Application";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorApp", policy =>
    {
        policy.WithOrigins("https://localhost:7083")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("BlazorApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "SoftwareSecu API kører.");

app.MapDelete("/api/users/{id}", async (
    string id,
    UserManager<ApplicationUser> userManager) =>
{
    var user = await userManager.FindByIdAsync(id);

    if (user is null)
    {
        return Results.NotFound("Brugeren blev ikke fundet.");
    }

    var result = await userManager.DeleteAsync(user);

    if (!result.Succeeded)
    {
        return Results.BadRequest(result.Errors);
    }

    return Results.Ok($"Brugeren {user.Email} blev slettet via API.");
})
.RequireAuthorization(policy => policy.RequireRole("Admin"));

app.Run();