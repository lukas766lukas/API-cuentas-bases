using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sentinel.Server.Modelos;
using Sentinel.Server.Servicios;
using Sentinel.Server.Servicios.Encriptar;
using Sentinel.Server.Servicios.JWT;
using Sentinel.Server.Servicios.Sesion;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMapster();
//builder.Services.AddScoped<IService, Service>();

builder.Services.AddControllers();
//injectamos nuestro codigo para crear la base de datos definida en AppDbcontext y appsettings.json
builder.Services.AddDbContext<AppDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//injectamos LOS SERVICE y las claves
builder.Services.AddScoped<IHashPassword, HashPasword>();//responsable de las claves
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IService, Service>();
//service de las sesiones:
builder.Services.AddSingleton<ISessionService, SessionService>();

//JWT configuracion para usarlo de forma pre-configurada y personalizada.
builder.Services.AddAuthentication(opciones => {
    opciones.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opciones.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opciones.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(opciones => {
    //preguntar si esto esta bien
    opciones.RequireHttpsMetadata = false;
    opciones.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"]!))

    };
    opciones.Events = new JwtBearerEvents
    {
        OnTokenValidated =  context =>
        {
            var sessionService = context.HttpContext.RequestServices
            .GetRequiredService<ISessionService>();

            var sessionIdClaim = context.Principal?
                .FindFirst("SessionId")?.Value;

            if (sessionIdClaim == null ||
                !Guid.TryParse(sessionIdClaim, out Guid sessionId) ||
                !sessionService.SesionValida(sessionId))
            {
                context.Fail("Sesión inválida o expirada");
            }

            return Task.CompletedTask;
        }
    };

});




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
