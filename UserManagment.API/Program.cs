using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using UserManagment.Common.Auth;
using UserManagment.Common.Services.Implementations;
using UserManagment.Common.Services.Interfaces;
using UserManagment.Common.Validations.Implementations;
using UserManagment.Common.Validations.Interfaces;
using UserManagment.Infrastructure.Db;
using UserManagment.Infrastructure.Repositories.Implimentations;
using UserManagment.Infrastructure.Repositories.Interfaces;
using UserManagment.Infrastructure.UnitOfWork.Implementations;
using UserManagment.Infrastructure.UnitOfWork.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Services.AddLogging(options =>
{
    options.AddSerilog(dispose: true);
});
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Logging.AddSerilog();

builder.Host.UseSerilog();

builder.Services.AddDbContextPool<AppDbContext>(c =>
    c.UseSqlServer(builder.Configuration["DefaultConnection"]));

AuthConfigurator.Configure(builder);

builder.Services.AddScoped<IBaseRepository, BaseRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserValidations, UserValidations>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IJsonPlaceHolderService, JsonPlaceHolderService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\\r\\n\\r\\nExample: \"Bearer1safsfsdfdfd\"\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
