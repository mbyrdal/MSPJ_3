using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.BusinessLogic;
using ServiceAPI.DatabaseAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ServiceAPI.Utilities;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<DbProduct>();
builder.Services.AddScoped<DbCustomer>();
builder.Services.AddScoped<DbCar>();
builder.Services.AddScoped<DbCarTemplate>();

builder.Services.AddScoped<IProductControl, ProductControl>();
builder.Services.AddScoped<ICustomerControl, CustomerControl>();
builder.Services.AddScoped<ICarControl, CarControl>();
builder.Services.AddScoped<ICarTemplateControl, CarTemplateControl>();

// Add JWT configuration
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));


// Add Authentication services with JWT bearer token support
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
        options.RequireHttpsMetadata = false; // set to true in prod
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });

// Configure session state to store ShoppingCart (customer specific)
builder.Services.AddDistributedMemoryCache(); // For session storage
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5); // Session timeout
    options.Cookie.HttpOnly = true; // Make session cookie HttpOnly
    options.Cookie.IsEssential = true; // Make session cookie essential
});

builder.Services.AddControllers();
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

app.UseAuthorization();

// Enable session middleware
app.UseSession(); // This enables session for the ServiceAPI project

app.MapControllers();

app.Run();
