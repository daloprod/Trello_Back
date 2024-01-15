using Microsoft.Extensions.Configuration;
using Trello1.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<PresidentContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddCors(options => options.AddPolicy(name: "christopher",
   policy => {
       policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
   }
   ));

var app = builder.Build();

app.UseCors("christopher");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
