using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TutorHub.API.Data;
using TutorHub.API.Models;
using TutorHub.API.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TokenService>();

// Database configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

// Identity configuration
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.User.RequireUniqueEmail = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["Jwt:Issuer"],
        ValidAudience = config["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
    };
});
builder.Services.AddAuthorization();

//CORS Service
builder.Services.AddCors(options =>
{
    options.AddPolicy("Dev", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5083",   // frontend
                "http://localhost:5089",   // your API's own Swagger UI port
                "https://localhost:7180"   // Vite / React
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });

    options.AddPolicy("Production", policy =>
    {
        policy
            .WithOrigins(config.GetSection("Cors:Origins").Get<string>() ?? throw new InvalidOperationException("Cors origins not configured"))
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//swagger
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

var app = builder.Build();

// CORS and Swagger setup based on environment
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TutorHub API V1");
        options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
    app.UseCors("Dev");        // ← dev policy
} else
{
    app.UseCors("Production"); // ← production policy
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

//Seed data
await Seed.SeedAsync(app);

app.Run();
