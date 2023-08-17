using Microsoft.OpenApi.Models;
using WebApplicationApiClime.Sqlite;
using WebApplicationApiClime.Sqlite.Repositories;
using WebApplicationApiClime.Sqlite.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<IClimaAeroportoRepository, ClimaAeroportoRepository>();
builder.Services.AddScoped<IClimaCidadeRepository, ClimaCidadeRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API - Clima",
        Version = "v1",
        Description = "API para obter informações sobre o clima",
        Contact = new OpenApiContact
        {
            Name = "Rafael Pacheco",
            Email = "seu.email@exemplo.com",
            Url = new System.Uri("https://seusite.com")
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// sqlite
app.Services.GetService<IDatabaseBootstrap>().Setup();

app.Run();
