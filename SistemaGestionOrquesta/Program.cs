using SistemaGestionOrquesta.Models;
using SistemaGestionOrquesta.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SistemaGestionOrquesta.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

string MyCors = "MyCors";


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyCors, build =>
    {
        build.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

builder.Services.AddDbContext<OrquestaOESATContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConStringOesat"));
});
builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<IProfesorService, ProfesorService>();
builder.Services.AddScoped<IInstrumentoService, InstrumentoService>();
builder.Services.AddScoped<ICursoService, CursoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseCors(MyCors);
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
