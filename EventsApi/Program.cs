using EventsApi.Features.Events.Validators;
using EventsApi.Features.Middleware;
using EventsApi.Features.Models;
using EventsApi.MongoDb;
using FluentValidation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//mongoDb
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
    return new MongoClient(settings!.ConnectionString);
});

//key dependencies
builder.Services.AddScoped<IValidator<Event>, EventValidator>();
builder.Services.AddSingleton<IEventRepo, MongoDbRepo>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddCors(p => p.AddPolicy("corsPolicy", build 
    => build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var filePath = Path.Combine(AppContext.BaseDirectory, "EventsApi.xml");
    c.IncludeXmlComments(filePath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseCors("corsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();

}

app.UseAuthorization();

app.MapControllers();

app.Run();
