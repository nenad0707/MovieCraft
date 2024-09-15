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
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Sinks.Network;
using Serilog.Sinks.SystemConsole.Themes;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

builder.Services.Configure<PapertrailSettings>(builder.Configuration.GetSection("Papertrail"));

// Add services to the container.
var papertrailSettings = new PapertrailSettings();
builder.Configuration.GetSection("Papertrail").Bind(papertrailSettings);

var syslogFormat = "{Timestamp:yyyy-MM-ddTHH:mm:ssZ} [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}"; 
var syslogFormatter = new MessageTemplateTextFormatter(syslogFormat, null);
var papertrailUri = $"tls://{papertrailSettings.Host}:{papertrailSettings.Port}";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() 
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) 
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information) 
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information) 
    .Enrich.FromLogContext()
    .WriteTo.Console(
    outputTemplate: syslogFormat,
    theme: AnsiConsoleTheme.Literate
)
    .WriteTo.TCPSink(
        papertrailUri,
        textFormatter: syslogFormatter)
    .CreateLogger();



builder.Host.UseSerilog();

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
