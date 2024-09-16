using MovieCraft.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddLoggingServices();
builder.AddAuthenticationServices();
builder.AddStandardServices();
builder.AddDatabaseServices();
builder.AddApplicationServices();
builder.AddRateLimitingServices();


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
