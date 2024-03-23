//todo: remove uploaded files in delete/update action in seller and product con
//todo: return id after create entity in controllers
//todo: check admin access in creating product
//todo: try to build some adapter patter for easy switch in razor page
//todo: unit test
//todo: refactor uploader (maybe factory pattern) for solid purposes
//todo: separate select and mapper from repository and data layer! i think repo should return T
using Common.Commons;
using Common.Data;
using Common.Modules.User;

var builder = WebApplication.CreateBuilder(args);

builder.AddAuthentication();
builder.Services.AddAuthorizationBuilder().AddCurrentUserHandler();

builder.Services.AddTokenService();

var connectionString = builder.Configuration.GetConnectionString("MainConnection");
builder.Services.AddSqlServer<BaseDbContext>(connectionString);

builder.Services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<BaseDbContext>();

builder.Services.AddControllers();

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
