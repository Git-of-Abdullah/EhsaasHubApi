using EhsaasHub.Data;
using EhsaasHub.Models.AuthModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();


var envConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
Console.WriteLine(envConnectionString);
var SuperSecret = Environment.GetEnvironmentVariable("SUPER_SECRET");


var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
if (!string.IsNullOrEmpty(jwtIssuer))
{
    builder.Configuration["Jwt:Issuer"] = jwtIssuer;
}

if (!string.IsNullOrEmpty(envConnectionString))
{
    builder.Configuration["ConnectionStrings:DefaultConnection"] = envConnectionString;
    Console.WriteLine($"DB Connection String: {envConnectionString}");
}

if (!string.IsNullOrEmpty(SuperSecret))
{
    builder.Configuration["Jwt:Key"] = SuperSecret;
    Console.WriteLine($"Secret Key: {SuperSecret}");
}


// Add EF Core DbContext with connection string (update your connection string in appsettings.json)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // You can configure password, lockout, user settings here if needed
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

//Add Authentication
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
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });





// Add services to the container.





builder.Services.AddControllers();
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

try
{
    app.Run();
}
catch(Exception ex)
{
    Console.WriteLine(" Unhandled exception during app startup:");
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
}

