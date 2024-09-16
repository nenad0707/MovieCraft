using Microsoft.AspNetCore.RateLimiting;
using MovieCraft.Server.Extensions;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.AddLoggingServices();
builder.AddAuthenticationServices();
builder.AddStandardServices();
builder.AddDatabaseServices();
builder.AddApplicationServices();

builder.Services.AddRateLimiter(options =>
{
    
    options.AddTokenBucketLimiter("basic", opt =>
    {
        opt.TokenLimit = 100; 
        opt.TokensPerPeriod = 10;
        opt.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
        opt.QueueLimit = 2;
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst; 
    });
});

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
app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
