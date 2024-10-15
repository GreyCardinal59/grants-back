using System.Text.Json;
using admin_back;
using admin_back.Contracts;
using admin_back.Controllers;
using admin_back.DataAccess;
using admin_back.Extensions;
using admin_back.Interfaces;
using admin_back.Services;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;


services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});

services.AddSpaStaticFiles(config =>
{
    config.RootPath = "wwwroot/assets";
});

services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

services.AddApiAuthentication(services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());

services.AddControllers();

services.AddSwaggerGen();

services.AddScoped<GrantsDbContext>();
services.AddScoped<UsersDbContext>();

services.AddScoped<HttpContextAccessor>();

services.AddScoped<UsersService>();
services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IPasswordHasher, PasswordHasher>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
await using var grantsDbContext = scope.ServiceProvider.GetRequiredService<GrantsDbContext>();
await using var usersDbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
await grantsDbContext.Database.EnsureCreatedAsync();
await usersDbContext.Database.EnsureCreatedAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseSpaStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAllOrigins");
app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
