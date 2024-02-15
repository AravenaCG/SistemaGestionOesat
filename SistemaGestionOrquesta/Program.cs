using SistemaGestionOrquesta.Models;
using SistemaGestionOrquesta.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SistemaGestionOrquesta.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string MyCors = "MyCors";


builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

    };
});
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

builder.Configuration.AddJsonFile("appsettings.json");
var secretKey = builder.Configuration.GetSection("Jwt").GetSection("Key").ToString();
var keyBytes = Encoding.UTF8.GetBytes(secretKey);




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseCors(MyCors);
app.UseHttpsRedirection();
app.UseAuthentication();
//app.UseIdentityServer();

app.UseAuthorization();

app.MapControllers();

app.Run();