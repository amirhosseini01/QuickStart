//todo: remove uploaded files in delete/update action in seller and product con
//todo: return id after create entity in controllers
//todo: check admin access in creating product
//todo: refactor code for easy copy paste
//todo: try to build some adapter patter for easy switch in razor page
//todo: unit test
//todo: check content of uploaded file and search for other possible validation
using Api.Common;
using Api.Modules.Users;

var builder = WebApplication.CreateBuilder(args);

builder.AddAuthentication();
builder.Services.AddAuthorizationBuilder().AddCurrentUserHandler();

builder.Services.AddTokenService();

var connectionString = builder.Configuration.GetConnectionString("Api") ?? "Data Source=.db/Api.db";
builder.Services.AddSqlite<ApiDbContext>(connectionString);

builder.Services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<ApiDbContext>();

builder.Services.AddControllers();

builder.Services.AddScrutor();
builder.Services.AddCustomServices();

builder.Services.AddCurrentUser();

builder.Services.AddEndpointsApiExplorer().AddSwaggerGen(o => o.InferSecuritySchemes());

builder.Services.AddRateLimiting();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseSwaggerUI();
}

app.UseStaticFiles();

app.UseRateLimiter();


app.MapControllers();

app.Map("/", () => Results.Redirect("/swagger"));
app.MapUsers();

app.Run();
