using ServiceAPI.BusinessLogic;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.DatabaseAccess.Interfaces;
using ServiceAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Allows for ProductControl to access DbProduct through DI Container
// builder.Services.AddScoped<ICRUD_DB<Product>, DbProduct>();
builder.Services.AddScoped<DbProduct>();

// Allows for ProductsController to access ProductControl through DI Container
builder.Services.AddScoped<IProductControl, ProductControl>(); 

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

app.UseAuthorization();

app.MapControllers();

/*
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=GetProducts}/{id?}");
*/
app.Run();
