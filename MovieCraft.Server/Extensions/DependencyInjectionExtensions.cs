using Microsoft.EntityFrameworkCore;
using MovieCraft.Infrastructure.Configuration;
using MovieCraft.Infrastructure.Persistence;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog;
using Serilog.Sinks.Network;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using MovieCraft.Application.Features.Movies.Queries;
using MovieCraft.Application.Interfaces;
using MovieCraft.Application.Mappings;
using MovieCraft.Infrastructure.Persistence.Repositories;
using MovieCraft.Infrastructure.Repositories;
using MovieCraft.Infrastructure.Services;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Microsoft.Extensions.Options;
using FluentValidation;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;

namespace MovieCraft.Server.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddLoggingServices(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((hostingContext, services, loggerConfiguration) =>
        {
            
            loggerConfiguration
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate: "{Timestamp:yyyy-MM-ddTHH:mm:ssZ} [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}",
                    theme: AnsiConsoleTheme.Literate
                );
        });
    }


    public static void AddAuthenticationServices(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));
    }

    public static void AddStandardServices(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddEnvironmentVariables();
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();
        builder.Services.AddMemoryCache();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    public static void AddDatabaseServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<MovieDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    }

    public static void AddApplicationServices(this WebApplicationBuilder builder)
    {
        // Add MeadiatR and AutoMapper
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetPopularMoviesQuery).Assembly));
        builder.Services.AddAutoMapper(typeof(MovieProfile));

        // Add FluentValidation validators
        builder.Services.AddValidatorsFromAssembly(typeof(GetPopularMoviesQuery).Assembly);

        // Add FluentValidation MediatR pipeline behavior
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // Add TMDb service
        builder.Services.Configure<TmdbSettings>(builder.Configuration.GetSection("TMDbSettings"));
        builder.Services.AddScoped<ITmdbService, TmdbService>();

        // Add repositories
        builder.Services.AddScoped<IMovieRepository, MovieRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IFavoriteMovieRepository, FavoriteMovieRepository>();
    }

    public static void AddRateLimitingServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: "global",
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 100,
                        Window = TimeSpan.FromSeconds(10),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 2
                    }));

            options.AddTokenBucketLimiter("basic", opt =>
            {
                opt.TokenLimit = 100;
                opt.TokensPerPeriod = 10;
                opt.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
                opt.QueueLimit = 2;
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            });
        });
    }
}
