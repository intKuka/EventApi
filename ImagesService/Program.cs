using ImagesService;

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


app.MapGet("/images", ImageData.GetAll);
app.MapGet("/images/{id:guid}", ImageData.CheckImage);

app.Run();
