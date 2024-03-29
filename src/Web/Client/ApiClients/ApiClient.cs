using System.Net.Http.Json;
// using Common.Modules.User;

namespace Client;

public class ApiClient
{
    private readonly HttpClient _client;
    public ApiClient(HttpClient client) => _client = client;

    public async Task<bool> LoginAsync(string? username, string? password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return false;
        }
        return true;
        // var response = await _client.PostAsJsonAsync("auth/login", new UserInfo { Username = username, Password = password });
        // return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateUserAsync(string? username, string? password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return false;
        }
        return true;
        // var response = await _client.PostAsJsonAsync("auth/register", new UserInfo { Username = username, Password = password });
        // return response.IsSuccessStatusCode;
    }

    public async Task<bool> LogoutAsync()
    {
        var uri = new Uri("auth/logout");
        var response = await _client.PostAsync(uri, content: null).ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }
}