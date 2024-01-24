using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Trello1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddPolicy(name: "christopher",
   policy => {
       policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
   }
));

// Service DbContext
builder.Services.AddDbContext<PresidentContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

try
{
    var app = builder.Build();

    // Configurez le pipeline de requête HTTP.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors("christopher");  // Placez ceci avant app.MapControllers()

    app.UseAuthorization();

    app.MapControllers();

    app.Run("http://localhost:5136");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred while building or running the application: {ex}");
}
