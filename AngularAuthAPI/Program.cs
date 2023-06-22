using AuthProject.Application.Interface;
using AuthProject.Application.Services;
using AuthProject.Domain.Repositories;
using AuthProjectAPI.Context;
using AuthProjectAPI.ErrorHandling;
using AuthProjectAPI.Extensions;
using AuthProjectAPI.Helpers;
using AuthProjectAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.ConfigureCors();
    builder.Services.ConfigureAutoMapper();
    builder.Services.ConfigureApiVersioning();

    builder.Services.AddAppDbContext(builder.Configuration);

    builder.Services.AddJwtAuthentication(builder.Configuration);

    builder.Services.AddResponseCompression(option => {
        option.EnableForHttps = true;
    });

    builder.Services.AddSingleton<ITokenManager, TokenManager>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IUserService, UserService>();

}



var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.UseMiddleware<ErrorHandlerMiddleware>();

    app.UseResponseCompression();

    app.UseHttpsRedirection();
    app.UseCors("MyPolicy");

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}