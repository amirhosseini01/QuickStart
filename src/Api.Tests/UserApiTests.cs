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
        var passwordError = new[] { "The Password field is required." };
        Assert.Equal(passwordError, problemDetails.Errors["Password"]);

        response = await client.PostAsJsonAsync("/users", new UserInfo { Username = "", Password = "password" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(problemDetails);

        Assert.Equal("One or more validation errors occurred.", problemDetails.Title);
        Assert.NotEmpty(problemDetails.Errors);
        var userNameError = new[] { "The Username field is required." };
        Assert.Equal(userNameError, problemDetails.Errors["Username"]);
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
