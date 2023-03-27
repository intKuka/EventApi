using PaymentService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(typeof(PayData));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

var operations = app.Services.GetService<PayData>();
app.MapGet("/payment", operations!.GetCurrentOperation);
app.MapGet("/payment/create", operations.CreateOperation);
app.MapGet("/payment/confirm", operations.ConfirmOperation);
app.MapGet("/payment/cancel", operations.CancelOperation);

app.Run();
