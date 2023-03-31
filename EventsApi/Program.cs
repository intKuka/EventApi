using EventsApi.Features.Events;
using EventsApi.Middleware;
using EventsApi.MongoDb;
using EventsApi.Policies;
using EventsApi.RabbitMq;
using FluentValidation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//mongoDb
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
builder.Services.AddSingleton<IMongoClient>(_ =>
{
    var settings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
    return new MongoClient(settings!.ConnectionString);
});

//key dependencies
builder.Services.AddScoped<IValidator<Event>, EventValidator>();
builder.Services.AddSingleton<IEventRepo, MongoDbRepo>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddHttpClient(Global.EventClient).AddPolicyHandler(HttpClientPolicy.GetExponentialRetryPolicy());
builder.Services.AddHostedService<RmqDeletionListener>();
builder.Services.AddSingleton(typeof(EventDeletionSender));

builder.Services.AddCors(p => p.AddPolicy("corsPolicy", build 
    => build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var filePath = Path.Combine(AppContext.BaseDirectory, "EventsApi.xml");
    c.IncludeXmlComments(filePath);
});
builder.Services.AddHttpLogging(logging =>
{
    logging.MediaTypeOptions.AddText("application/json");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
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

app.UseHttpLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
