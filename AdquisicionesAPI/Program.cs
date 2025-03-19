using Microsoft.EntityFrameworkCore;
using AdquisicionesAPI.Data;
using AdquisicionesAPI.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var version = config["AppSettings:Version"] ?? "1.0.0";

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add SQLite database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services registration
builder.Services.AddScoped<ParametricsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI( c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"API APS - v{version}");
    c.RoutePrefix = "swagger";
});

app.MapControllers(); 
// app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());


app.Run();
