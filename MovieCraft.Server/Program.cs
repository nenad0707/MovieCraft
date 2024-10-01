using Microsoft.EntityFrameworkCore;
using MovieCraft.Infrastructure.Persistence;
using MovieCraft.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddLoggingServices();
builder.AddAuthenticationServices();
builder.AddStandardServices();
builder.AddDatabaseServices();
builder.AddApplicationServices();
builder.AddRateLimitingServices();


var app = builder.Build();

// Apply migrations only in Docker container
if (Environment.GetEnvironmentVariable("RUN_MIGRATIONS") == "true")
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        dbContext.Database.Migrate();
    }
}


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

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
