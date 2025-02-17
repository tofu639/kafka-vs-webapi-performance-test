var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Define a POST endpoint for testing performance
app.MapPost("/weatherforecast", async (HttpContext context) =>
{
    // Read the incoming message from the request body
    using var reader = new StreamReader(context.Request.Body);
    var message = await reader.ReadToEndAsync();

    // Simulate some processing (optional)
    return Results.Ok(new { Message = $"Received: {message}" });
})
.WithName("PostWeatherForecast")
.WithOpenApi();

app.Run();