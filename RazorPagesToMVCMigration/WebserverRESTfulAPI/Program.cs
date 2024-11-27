using ServiceAPI.BusinessLogic;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.DatabaseAccess.Interfaces;
using ServiceAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Allows for CF objects to access DbAccess objects through DI Container
// builder.Services.AddScoped<ICRUD_DB<Product>, DbProduct>(); ??
builder.Services.AddScoped<DbProduct>();
builder.Services.AddScoped<DbCustomer>();
builder.Services.AddScoped<DbCar>();

// Allows for Controllers to access CF objects through DI Container
builder.Services.AddScoped<IProductControl, ProductControl>();
builder.Services.AddScoped<ICustomerControl, CustomerControl>();
builder.Services.AddScoped<ICarModelControl, CarModelControl>();

// Configure Session state to store ShoppingCart (customer specific)
builder.Services.AddDistributedMemoryCache(); // For session storage
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


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

app.UseSession(); // Enable session

app.MapControllers();

/*
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=GetProducts}/{id?}");
*/
app.Run();