using Web.Shared.User;

namespace Web.Server;

public class AuthClient
{
    private readonly HttpClient _client;

	public AuthClient(HttpClient client) => this._client = client;

	public async Task<string?> GetTokenAsync(UserInfo userInfo)
    {
        var response = await _client.PostAsJsonAsync("users/token", userInfo);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var token = await response.Content.ReadFromJsonAsync<AuthToken>();

        return token?.Token;
    }

    public async Task<string?> CreateUserAsync(UserInfo userInfo)
    {
        var response = await _client.PostAsJsonAsync("users", userInfo);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await this.GetTokenAsync(userInfo);
    }
}
