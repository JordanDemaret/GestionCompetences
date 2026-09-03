using GestionCompetences.Application.Common.Hasher;
using GestionCompetences.Application.Utilisateur;
using GestionCompetences.Infrastructure;
using GestionCompetences.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<CompetenceDBContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("database"))
);

builder.Services.AddSingleton<IPasswordHasher, PasswordHasherService>();

builder.Services.AddScoped<IUtilisateurRepository, UtilisateurService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
