using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SistemaGestionOrquesta.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SistemaGestionOrquesta.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
string MyCors = "MyCors";

// 1. CONFIGURACIÓN DE JWT (Sincronizada con tu Key corta)
var secretKey = builder.Configuration["Jwt:Key"] ?? "=MOTU$KeyMaster$=Yn8k2jPvO7ZFGJ4NWLXH3prTSM9V";
var keyBytes = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuers = new[] { builder.Configuration["Jwt:Issuer"], "https://jwtauthapi.azurewebsites.net/" },
            ValidAudiences = new[] { builder.Configuration["Jwt:Audience"], "https://jwtauthapi.azurewebsites.net/" },
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
        };
    });

// 2. SERVICIOS Y CONTROLLERS
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyCors, policy =>
    {
        policy
            .WithOrigins(
                "https://victorious-cliff-0294ff410.3.azurestaticapps.net",
                "https://localhost:7182",
                "http://localhost:5182"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// 3. BASE DE DATOS (Lee de la Connection String de Azure o Local)
builder.Services.AddDbContext<OrquestaOESATContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConStringOesat"));
});

// 4. INYECCIÓN DE DEPENDENCIAS
builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<IProfesorService, ProfesorService>();
builder.Services.AddScoped<IInstrumentoService, InstrumentoService>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IPrestamosService, PrestamosService>();
builder.Services.AddScoped<IAsistenciaService, AsistenciaService>();
builder.Services.AddScoped<IEventoService, EventoService>();

var app = builder.Build();

// 5. SWAGGER SIEMPRE ACTIVO PARA TESTEAR EN AZURE
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("./v1/swagger.json", "SistemaGestionOrquesta v1");
    c.RoutePrefix = "swagger";
});

// ⚠️ CORS DEBE IR PRIMERO — antes de Auth y Authorization
// Esto garantiza que las respuestas 401, 403, 404 también incluyan
// los headers CORS y el navegador no las bloquee.
app.UseCors(MyCors);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();