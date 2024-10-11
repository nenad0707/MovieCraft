using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MovieCraft.Client;
using MovieCraft.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("MovieCraft.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddHttpClient("AnonymousServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddSingleton<BackgroundState>();
builder.Services.AddSingleton<MovieService>();
builder.Services.AddSingleton<PopularMoviesState>();
builder.Services.AddScoped<UserState>();

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://nenadorg.onmicrosoft.com/e3c55284-c67e-495f-a42d-5fc3766d9317/access_as_user");
});

await builder.Build().RunAsync();
