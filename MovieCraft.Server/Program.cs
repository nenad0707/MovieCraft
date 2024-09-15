using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using MovieCraft.Application.Features.Movies.Queries;
using MovieCraft.Application.Interfaces;
using MovieCraft.Application.Mappings;
using MovieCraft.Infrastructure.Configuration;
using MovieCraft.Infrastructure.Persistence;
using MovieCraft.Infrastructure.Persistence.Repositories;
using MovieCraft.Infrastructure.Repositories;
using MovieCraft.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<MovieDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// MediatR and AutoMapper setup
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetPopularMoviesQuery).Assembly));
builder.Services.AddAutoMapper(typeof(MovieProfile));

// Register TMDb settings
builder.Services.Configure<TmdbSettings>(builder.Configuration.GetSection("TMDbSettings"));
builder.Services.AddScoped<ITmdbService, TmdbService>();

// Register repositories
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFavoriteMovieRepository, FavoriteMovieRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
