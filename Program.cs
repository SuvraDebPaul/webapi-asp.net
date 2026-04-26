using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();              // Exposes OpenAPI JSON at /openapi/v1.json
    app.MapScalarApiReference();   // Enables Scalar UI at /scalar

    // Redirect root URL to Scalar in dev
    app.MapGet("/", () => Results.Redirect("/scalar"));
}
else
{
    // In production, show a simple message at root
    app.MapGet("/", () => "API is working fine");
}

// Common routes
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();




app.MapGet("/products", () =>
{
   var products = new List<Product>()
   {
       new Product("Samsung", 1250),
       new Product("IPhone", 1356)
   };

    return Results.Ok(products);
});

app.Run();

public record Product(string Name, decimal Price);
