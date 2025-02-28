using EstadoDeCuenta.API.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using System;
using EstadoDeCuenta.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurar la conexi�n a la base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Agregar controladores con validaci�n FluentValidation
builder.Services.AddControllers()
    .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddScoped<ITarjetaCreditoRepository, TarjetaCreditoRepository>();
builder.Services.AddScoped<ICompraRepository, CompraRepository>();
builder.Services.AddScoped<IPagoRepository, PagoRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Configuraci�n de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirWebApp",
        builder => builder
            .WithOrigins("https://localhost:44370") // Direcci�n del Frontend
            .AllowAnyMethod()
            .AllowAnyHeader());
});




// Configuraci�n de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EstadoDeCuenta API",
        Version = "v1"
    });
});

// Construcci�n de la aplicaci�n
var app = builder.Build();
app.UseCors("PermitirWebApp");
// Configuraci�n del pipeline de middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EstadoDeCuenta API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
