using MaintenanceManagement.Infrastructure;
using MaintenanceManagement.Application;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();

// Add services to the container
builder.Services.AddInfrastructureServices(configuration);
builder.Services.AddApplicationServices(configuration);

// Set the URL to listen to (port 5000)

// Add controllers and other services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS configuration to allow Angular app to communicate
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevOrigin",
        builder => builder.WithOrigins("http://localhost:4200")  // Angular dev server URL
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials());
});

// JWT Authentication configuration
var jwtSettings = configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];
var key = Encoding.ASCII.GetBytes(secretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS before Authentication and Authorization (this is crucial for allowing cross-origin requests)
app.UseCors("AllowAngularDevOrigin");

app.UseAuthentication();  // Authentication comes before Authorization
app.UseAuthorization();   // Authorization comes after Authentication

app.MapControllers();

app.Run();


