using Microsoft.AspNetCore.Components.Authorization;
using MovieCraft.Shared.DTOs;
using System.Net.Http.Json;

namespace MovieCraft.Client.Services;

public class UserState
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public UserDto? CurrentUser { get; private set; }
    public event Action? OnChange;

    public UserState(IHttpClientFactory httpClientFactory, AuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient = httpClientFactory.CreateClient("MovieCraft.ServerAPI");
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task LoadUserAsync()
    {
        if (CurrentUser != null)
        {
            return;
        }

        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity?.IsAuthenticated ?? false)
        {
            var userId = user.FindFirst(c => c.Type == "sub")?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                CurrentUser = await _httpClient.GetFromJsonAsync<UserDto>($"api/users/{userId}");
                NotifyStateChanged();
            }
        }
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
