using AutoMapper;
using System.Text.Json.Serialization;
using TexCode.Authorization;
using TexCode.DatabaseContext;
using TexCode.Helpers;
using TexCode.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container
var services = builder.Services;
services.AddCors();

// add services for controllers and 
// add AuthorizeAttribute to all controllers and actions x => x.Filters.Add<AuthorizeAttribute>())
services.AddControllers();

// configure strongly typed settings object
services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// configure DI for application services
services.AddScoped<IUserService, UserService>();
services.AddScoped<IEmailService, EmailService>();
services.AddScoped<IShelfLayoutService, ShelfLayoutService>();
services.AddScoped<ISKUService, SKUService>();

// configuring Swagger/OpenAPI
services.AddEndpointsApiExplorer();

services.AddDbContext<APIContext>();
services.AddCors();
services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
services.AddSwaggerGen();

// configure HTTP request pipeline
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();