namespace Api.Tests;

public class UserApiTests
{
    [Fact]
    public async Task CanCreateAUser()
    {
        await using var application = new ApiApplication();
        await using var db = application.CreateApiDbContext();

        var client = application.CreateClient();
        var response = await client.PostAsJsonAsync("/users", new UserInfo { Username = "admin", Password = "@pwd" });

        Assert.True(response.IsSuccessStatusCode);

        var user = db.Users.Single();
        Assert.NotNull(user);

        Assert.Equal("admin", user.UserName);
    }

    [Fact]
    public async Task MissingUserOrPasswordReturnsBadRequest()
    {
        await using var application = new ApiApplication();
        await using var db = application.CreateApiDbContext();

        var client = application.CreateClient();
        var response = await client.PostAsJsonAsync("/users", new UserInfo { Username = "user", Password = "" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(problemDetails);

        Assert.Equal("One or more validation errors occurred.", problemDetails.Title);
        Assert.NotEmpty(problemDetails.Errors);
        Assert.Equal(new[] { "The Password field is required." }, problemDetails.Errors["Password"]);

        response = await client.PostAsJsonAsync("/users", new UserInfo { Username = "", Password = "password" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(problemDetails);

        Assert.Equal("One or more validation errors occurred.", problemDetails.Title);
        Assert.NotEmpty(problemDetails.Errors);
        Assert.Equal(new[] { "The Username field is required." }, problemDetails.Errors["Username"]);
    }



    [Fact]
    public async Task MissingUsernameOrProviderKeyReturnsBadRequest()
    {
        await using var application = new ApiApplication();
        await using var db = application.CreateApiDbContext();

        var client = application.CreateClient();
        var response = await client.PostAsJsonAsync("/users/token/Google", new ExternalUserInfo { Username = "user" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(problemDetails);

        Assert.Equal("One or more validation errors occurred.", problemDetails.Title);
        Assert.NotEmpty(problemDetails.Errors);
        Assert.Equal(new[] { $"The {nameof(ExternalUserInfo.ProviderKey)} field is required." }, problemDetails.Errors[nameof(ExternalUserInfo.ProviderKey)]);

        response = await client.PostAsJsonAsync("/users/token/Google", new ExternalUserInfo { ProviderKey = "somekey" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(problemDetails);

        Assert.Equal("One or more validation errors occurred.", problemDetails.Title);
        Assert.NotEmpty(problemDetails.Errors);
        Assert.Equal(new[] { $"The Username field is required." }, problemDetails.Errors["Username"]);
    }

    [Fact]
    public async Task CanGetATokenForValidUser()
    {
        await using var application = new ApiApplication();
        await using var db = application.CreateApiDbContext();
        await application.CreateUserAsync("user", "p@assw0rd1");

        var client = application.CreateClient();
        var response = await client.PostAsJsonAsync("/users/token", new UserInfo { Username = "user", Password = "p@assw0rd1" });

        Assert.True(response.IsSuccessStatusCode);

        var token = await response.Content.ReadFromJsonAsync<AuthToken>();

        Assert.NotNull(token);

        // Check that the token is indeed valid

        // var req = new HttpRequestMessage(HttpMethod.Get, "/todos");
        // req.Headers.Authorization = new("Bearer", token.Token);
        // response = await client.SendAsync(req);

        // Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task CanGetATokenForExternalUser()
    {
        await using var application = new ApiApplication();
        await using var db = application.CreateApiDbContext();

        var client = application.CreateClient();
        var response = await client.PostAsJsonAsync("/users/token/Google", new ExternalUserInfo { Username = "user", ProviderKey = "1003" });

        Assert.True(response.IsSuccessStatusCode);

        var token = await response.Content.ReadFromJsonAsync<AuthToken>();

        Assert.NotNull(token);

        // Check that the token is indeed valid

        // var req = new HttpRequestMessage(HttpMethod.Get, "/todos");
        // req.Headers.Authorization = new("Bearer", token.Token);
        // response = await client.SendAsync(req);

        // Assert.True(response.IsSuccessStatusCode);

        using var scope = application.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApiUser>>();
        var user = await userManager.FindByLoginAsync("Google", "1003");
        Assert.NotNull(user);
        Assert.Equal("user", user.UserName);
    }

    [Fact]
    public async Task BadRequestForInvalidCredentials()
    {
        await using var application = new ApiApplication();
        await using var db = application.CreateApiDbContext();
        await application.CreateUserAsync("user", "p@assw0rd1");

        var client = application.CreateClient();
        var response = await client.PostAsJsonAsync("/users/token", new UserInfo { Username = "user", Password = "prd1" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
