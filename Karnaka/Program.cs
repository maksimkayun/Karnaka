using Karnaka.Data;
using Karnaka.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// + DB контекст
builder.Services.AddDbContext<KarnakaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KarnakaContext")));

// Добавляем сервисы и мапперы
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IConspiratorService, ConspiratorService>();
builder.Services.AddScoped<ILocationService, LocationService>();


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

app.Run();