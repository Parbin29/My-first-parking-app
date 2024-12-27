using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ParkingApp API",
        Version = "v1",
        Description = "API documentation for the ParkingApp backend."
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ParkingApp API V1");
        c.RoutePrefix = string.Empty; // Sets Swagger UI at the app's root
    });
}


app.UseHttpsRedirection();
app.MapControllers();

app.Run();

