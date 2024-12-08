using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.BusinessLogic;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using System.Security.Claims;

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

// Configure Identity in our System (ASP.NET Identity)
builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    // Password policies...
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
}).AddUserStore<DbUserStore>() // Custom user store (CRUD)
  .AddSignInManager<SignInManager<ApplicationUser>>() // SignInManager enables login/logout functionality for our ApplicationUser
  .AddDefaultTokenProviders(); // Default token provider ensures email confirmation, password reset, 2FA etc.

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Path to the login page
    options.LogoutPath = "/Account/Logout"; // Path to logout
    options.AccessDeniedPath = "/Account/AccessDenied"; // Access denied path
    options.SlidingExpiration = true; // Extend the expiration on activity
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10); // Cookie expiration
});

// Configure Authentication with OAuth2 support (!)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme; // Default cookie scheme
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme; // Default challenge scheme
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme; // Scheme for external logins
})
    .AddCookie(IdentityConstants.ApplicationScheme) // Application cookie
    .AddCookie(IdentityConstants.ExternalScheme) // External login cookie
    .AddOAuth("OAuth2Provider", options =>
    {
        options.ClientId = builder.Configuration["OAuth2:ClientId"]; // From appsettings.json
        options.ClientSecret = builder.Configuration["OAuth2:ClientSecret"]; // From appsettings.json
        options.CallbackPath = "/signin-google"; // OAuth2 callback path

        options.AuthorizationEndpoint = builder.Configuration["OAuth2:AuthorizationEndpoint"];
        options.TokenEndpoint = builder.Configuration["OAuth2:TokenEndpoint"];
        options.UserInformationEndpoint = builder.Configuration["OAuth2:UserInformationEndpoint"];

        options.SaveTokens = true; // Persist tokens
        options.Scope.Add("profile");
        options.Scope.Add("email");

        options.Events = new Microsoft.AspNetCore.Authentication.OAuth.OAuthEvents
        {
            OnCreatingTicket = async context => // Event handles mapping OAuth2 user information (ID, Name, Email etc.) to identity claims
            {
                var userInfo = await context.Backchannel.GetAsync(context.Options.UserInformationEndpoint);
                userInfo.EnsureSuccessStatusCode();
                var user = JsonDocument.Parse(await userInfo.Content.ReadAsStringAsync());
                var userJson = user.RootElement;

                context.Identity?.AddClaim(new Claim(ClaimTypes.NameIdentifier, userJson.GetProperty("id").GetString()));
                context.Identity?.AddClaim(new Claim(ClaimTypes.Name, userJson.GetProperty("name").GetString()));
                context.Identity?.AddClaim(new Claim(ClaimTypes.Email, userJson.GetProperty("email").GetString()));
            }
        };
    });

// Register the custom user store (DbUserStore)
builder.Services.AddScoped<IUserStore<ApplicationUser>, DbUserStore>();
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable Authentication and authorizarion middleware pipeline...
app.UseAuthentication();
app.UseAuthorization();

// Enable session middleware
app.UseSession(); // This enables session for the ServiceAPI project

app.MapControllers();

app.Run();
