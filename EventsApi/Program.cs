using EventsApi.Features.Events.Data;
using EventsApi.Features.Events.Validators;
using EventsApi.Features.Middleware;
using EventsApi.Features.Models;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IValidator<Event>, EventValidator>();
builder.Services.AddSingleton<IEventData, EventData>();
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
