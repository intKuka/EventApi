using Microsoft.AspNetCore.Builder;
using SpacesService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(typeof(SpaceData));
builder.Services.AddSingleton(typeof(SpaceDeletionSender));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

var spaceData = app.Services.GetService<SpaceData>();
app.MapGet("/spaces", spaceData!.GetAll);
app.MapGet("/spaces/{id:guid}", spaceData.CheckSpace);
app.MapDelete("spaces/{id:guid}", spaceData.DeleteSpace);


app.Run();

