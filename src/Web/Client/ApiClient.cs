using System.Net;
using System.Net.Http.Json;

namespace Web.Client;

public class ApiClient
{
    private readonly HttpClient _client;
    public ApiClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<bool> LoginAsync(string? username, string? password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return false;
        }

        var response = await _client.PostAsJsonAsync("auth/login", new UserInfo { Username = username, Password = password });
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateUserAsync(string? username, string? password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return false;
        }

        var response = await _client.PostAsJsonAsync("auth/register", new UserInfo { Username = username, Password = password });
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> LogoutAsync()
    {
        var response = await _client.PostAsync("auth/logout", content: null);
        return response.IsSuccessStatusCode;
    }
}