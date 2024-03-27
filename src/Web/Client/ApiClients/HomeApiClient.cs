using System.Net.Http.Json;

namespace Client.ApiClients;

public static class HomeApiClient
{
    public static async Task<bool> GetHomePageData(this HttpClient client)
    {
        var response = await client.GetFromJsonAsync<bool>(requestUri: ApiUrls.HomePageData);
        return response;
    }
}