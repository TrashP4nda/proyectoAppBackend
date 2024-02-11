using proyectoApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using proyectoApi.Services;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSingleton<IEmailService, SmtpEmailService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("databasecon"), 
            new MySqlServerVersion(new Version(8, 0, 21))) // Make sure this version matches your MySQL server version
);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            
            ValidateIssuerSigningKey = true,
            ValidAudience = "best-api",
            ValidIssuer = "prueba.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtSettings:Key").Value)),
            
        };
    });

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder
            .AllowAnyOrigin() // Allows any origin
            .AllowAnyMethod()
            .AllowAnyHeader());
});


// Build the application
var app = builder.Build();


// Configure the HTTP request pipeline here, after calling Build()
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseCors("AllowAnyOrigin");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

Console.WriteLine(builder.Configuration.GetSection("JwtSettings:Key").Value);
app.Run();

//app.UseHttpsRedirection();


static string GenerateSecretKey()
{
    var rng = new RNGCryptoServiceProvider();
    var keyBytes = new byte[16]; // 16 bytes for 128 bits
    rng.GetBytes(keyBytes);
    return Convert.ToBase64String(keyBytes); // Convert to a Base64 string
}

