using APIGestionInventario.BAL;
using APIGestionInventario.BAL.JWT;
using APIGestionInventario.DAL.Repositories;
using APIGestionInventario.Interfaces;
using APIGestionInventario.Middleware;
using APIGestionInventario.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GestionInventarioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GestionInventarioDB")));

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAllOrigins",
        configurePolicy: policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });


builder.Services.AddSingleton<IJWTServices, JWTServices>();
builder.Services.AddSingleton<IGeneralServices, GeneralServices>();

builder.Services.AddScoped(typeof(IRepositoyGestionInventarioDB<>), typeof(RepositoyGestionInventarioDB<>));
builder.Services.AddScoped(typeof(IUsuarioRepository), typeof(UsuarioRepository));
builder.Services.AddScoped(typeof(IProductoRepository), typeof(ProductoRepository));
builder.Services.AddScoped(typeof(IOrdenCompraRepository), typeof(OrdenCompraRepository));
builder.Services.AddScoped(typeof(IOrdenReposicionRepository), typeof(OrdenReposicionRepository));

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Introduce el token JWT en el siguiente formato: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Gestión de Inventario",
        Version = "v1",
        Description = "API para gestionar productos, órdenes de compra, ordenes de reposicion.",
        Contact = new OpenApiContact
        {
            Name = "esalazar",
            Email = "esalazarl@example.com"
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CustomErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
