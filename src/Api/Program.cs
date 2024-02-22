using Api.Modules.Product;

var builder = WebApplication.CreateBuilder(args);

builder.AddAuthentication();
builder.Services.AddAuthorizationBuilder().AddCurrentUserHandler();

builder.Services.AddTokenService();

var connectionString = builder.Configuration.GetConnectionString("Api") ?? "Data Source=.db/Api.db";
builder.Services.AddSqlite<ApiDbContext>(connectionString);

builder.Services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<ApiDbContext>();

builder.Services.AddCurrentUser();

builder.Services.AddEndpointsApiExplorer().AddSwaggerGen(o => o.InferSecuritySchemes());

builder.Services.AddRateLimiting();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseSwaggerUI();
}

app.UseRateLimiter();

app.Map("/", () => Results.Redirect("/swagger"));
app.MapUsers();
app.MapProducts();

app.Run();
