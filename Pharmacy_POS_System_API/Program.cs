using Microsoft.EntityFrameworkCore;
using Pharmacy_POS_System_API.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultContextConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder.WithOrigins("http://localhost:4200") 
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials(); 
    });
});

var app = builder.Build();

app.UseCors("AllowSpecificOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseSession();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");

        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An unexpected error occurred. Please try again.");
    }
});

app.MapControllers();

app.Run();
