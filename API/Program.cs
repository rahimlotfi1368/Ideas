global using API.Extensions;
global using Domain.Entities;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using Services.EFConfigurations;
using AutoWrapper;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configuredatabasecontext(builder.Configuration);

builder.Services.ConfigureIdentity();

builder.Services.ConfigureMyJwtAuthentication(builder.Configuration);

builder.Services.ConfigureMyServices();

builder.Services.ConfigureMyMappers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureMySwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()|| app.Environment.IsProduction())
{
    app.UseCustomSwagger();
}


using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

try
{

    var dataBase = services.GetRequiredService<DataBaseContext>();

    var userManager = services.GetRequiredService<UserManager<User>>();

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await dataBase.Database.MigrateAsync();

    await Seed.SeedData(dataBase, userManager, roleManager);

}
catch (Exception ex)
{
    throw new Exception(ex.Message);
}


app.UseApiResponseAndExceptionWrapper();

app.UseMyStaticFiles();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.UseOptions();

app.Run();
