using Web.Server;

var builder = WebApplication.CreateBuilder(args);

// Configure auth with the front end
builder.AddAuthentication();
builder.Services.AddAuthorizationBuilder();

// Add razor pages so we can render the Blazor WASM component
builder.Services.AddRazorPages();

// Add the forwarder to make sending requests to the backend easier
builder.Services.AddHttpForwarder();

var url = builder.Configuration.GetServiceUri("api")?.ToString() ??
              builder.Configuration["ApiUrl"] ?? 
              throw new InvalidOperationException("API URL is not configured");

// Configure the HttpClient for the backend API
builder.Services.AddHttpClient<AuthClient>(client =>
{
    client.BaseAddress = new(url);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapFallbackToPage("/_Host");

// Configure the APIs
app.MapAuth();
app.MapApi(url);

app.Run();
