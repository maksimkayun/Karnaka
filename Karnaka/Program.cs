using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.Types;
using Karnaka.Data;
using Karnaka.GraphQL.Schemas;
using Karnaka.GraphQL.ServicesGraphQL;
using Karnaka.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using Unchase.Swashbuckle.AspNetCore.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddScoped<IConspiratorServiceGraphQL, ConspiratorServiceGraphQL>();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo {Title = "Мой API", Version = "v1"});
});

// Добавляем GraphQL
builder.Services.AddScoped<ISchema, ConspiratorSchema>();
builder.Services.AddGraphQL(options => { options.EnableMetrics = true; }).AddSystemTextJson();

// + DB контекст
builder.Services.AddDbContext<KarnakaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KarnakaContext")));

// Добавляем сервисы и мапперы
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IConspiratorService, ConspiratorService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<KarnakaContext, KarnakaContext>();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
    app.UseGraphQLAltair();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseGraphQL<ISchema>();

app.MapControllers();

app.Run();