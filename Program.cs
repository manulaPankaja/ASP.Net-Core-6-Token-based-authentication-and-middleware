using Microsoft.EntityFrameworkCore;
using Token_based_authentication_and_middleware;
using Token_based_authentication_and_middleware.Helpers.Utils.GlobalAttributes;
using Token_based_authentication_and_middleware.Middlewares;
using Token_based_authentication_and_middleware.Services.AuthenticateService;
using Token_based_authentication_and_middleware.Services.StoryService;
using Token_based_authentication_and_middleware.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

//add globbal attributes
GlobalAttributes.mySQLConfiguration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

//Add Application Db Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStoryService, StoryService>();
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();
app.UseJwtMiddleware();

app.MapControllers();

app.Run();
