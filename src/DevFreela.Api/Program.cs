using DevFreela.Api;
using DevFreela.Application;
using DevFreela.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddApiServices(builder.Configuration)
    .AddApplicationServices()
    .AddInfrastructure(builder.Configuration)
    .AddAuthServices(builder.Configuration)
    .AddSwaggerConfig();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();