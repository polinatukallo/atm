using Lab5.Application.Services;
using Lab5.Infrastructure.Persistence;
using Lab5.Presentation.Http;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string systemPassword = builder.Configuration["SystemPassword"] ?? "admin";

builder.Services
    .AddApplication(systemPassword)
    .AddInfrastructurePersistence()
    .AddPresentationHttp();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();