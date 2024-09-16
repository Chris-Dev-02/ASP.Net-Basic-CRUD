using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Basic_CRUD.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding DB connection string
builder.Services.AddDbContext<BasicCrudContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DB_String")));
// Ignoring cycle refences on foreign keys
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Adding cors settings
var customCorsRules = "CustomCorsRules";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: customCorsRules, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(customCorsRules);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
