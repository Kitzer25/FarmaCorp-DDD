using System.Text;
using Infraestructure.Configuration;
using Infraestructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

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
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]
                                       ?? throw new InvalidOperationException("SecretKey no encontrado")))
        };
    });

/*
 * JWT Policies
 */
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("TotalAccess", policy => 
        policy.RequireRole("Admin"));
    options.AddPolicy("ClientAccess", policy =>
        policy.RequireRole("Client"));
});


/*
 * Inyección de Dependencias
 */
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
//app.UseMiddleware<>();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

/* JWT Inicialización
app.UseAuthentication();
app.UseAuthorization();
*/

app.UseHttpsRedirection();
app.MapControllers(); //Swagger

app.Run();
