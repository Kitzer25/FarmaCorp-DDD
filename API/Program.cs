using System.Text;
using API.Middlewares;
using Application.Configuration;
using Infraestructure.Configuration;
using Infraestructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Core.Constants;

DotNetEnv.Env.Load("../.env");

var builder = WebApplication.CreateBuilder(args);

/*
 * DbContext
 */
builder.Services.AddDbContext<AppDbContext>(o =>
{
    var stringcn = builder.Configuration.GetConnectionString("Supabase");

    o.UseNpgsql(stringcn);
});

/*
 * JWT
 */
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]
                                       ?? throw new InvalidOperationException("SecretKey no encontrado")))
        };
    });

/*
 * JWT Policies


 * Políticas de autorización
 *
 * AdminOnly        → solo Admin
 * PharmacistAccess → Admin + Pharmacist   (recetas, inventario)
 * SalesAccess      → Admin + SalesRep + Manager (productos, pedidos, lotes)
 * ManagerAccess    → Admin + Manager      (reportes, clientes, promociones)
 * InternalAccess   → cualquier empleado interno
 * ClientAccess     → compradores del eCommerce
 * TotalAccess      → alias de AdminOnly (retrocompatibilidad)
 */
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PolicyNames.AdminOnly,
        p => p.RequireRole(Roles.Admin));

    options.AddPolicy(PolicyNames.PharmacistAccess,
        p => p.RequireRole(Roles.Admin, Roles.Pharmacist));

    options.AddPolicy(PolicyNames.SalesAccess,
        p => p.RequireRole(Roles.Admin, Roles.SalesRep, Roles.Manager));

    options.AddPolicy(PolicyNames.ManagerAccess,
        p => p.RequireRole(Roles.Admin, Roles.Manager));

    options.AddPolicy(PolicyNames.InternalAccess,
        p => p.RequireRole(Roles.Admin, Roles.Pharmacist, Roles.SalesRep, Roles.Manager));

    options.AddPolicy(PolicyNames.ClientAccess,
        p => p.RequireRole(Roles.Client));

    // Alias de retrocompatibilidad
    options.AddPolicy(PolicyNames.TotalAccess,
        p => p.RequireRole(Roles.Admin));
});


/*
 * Inyección de Dependencias
 */
builder.Services.AddApplication();
builder.Services.Addinfraestructure();


/* Swagger */
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});


var app = builder.Build();

/* Implementar Middleware */
app.UseMiddleware<ErrorHandlingMiddleware>();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/* JWT Inicialización */
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); //Swagger

app.Run();
