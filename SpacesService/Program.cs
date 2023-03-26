using SpacesService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}


app.MapGet("/spaces", SpaceData.GetAll);
app.MapGet("/spaces/{id:guid}", SpaceData.CheckSpace);


app.Run();

