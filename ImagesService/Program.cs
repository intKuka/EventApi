using ImagesService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(typeof(ImageData));
builder.Services.AddSingleton(typeof(ImageDeletionSender));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

var imageData = app.Services.GetService<ImageData>();
app.MapGet("/images", imageData!.GetAll);
app.MapGet("/images/{id:guid}", imageData.CheckImage);
app.MapDelete("images/{id:guid}", imageData.DeleteImage);


app.Run();
