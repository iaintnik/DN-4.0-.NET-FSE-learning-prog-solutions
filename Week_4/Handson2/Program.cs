// Import required namespaces
using Microsoft.OpenApi.Models;  // For Swagger documentation objects

// Create a WebApplicationBuilder instance
var builder = WebApplication.CreateBuilder(args);

// Register services to the dependency injection container

// Add controller support to the application (for API endpoints)
builder.Services.AddControllers();

// Add support for API exploration (used for Swagger documentation)
builder.Services.AddEndpointsApiExplorer();

// Add Swagger services and configure API documentation metadata
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Swagger Demo",  // Title shown in Swagger UI
        Version = "v1",          // API version
        Description = "Demonstration of Swagger with ASP.NET Core Web API",  // Description shown in Swagger UI
        TermsOfService = new Uri("https://example.com/terms"), // Placeholder terms of service

        // Contact info shown in Swagger UI
        Contact = new OpenApiContact
        {
            Name = "John Doe",
            Email = "john@xyzmail.com",
            Url = new Uri("https://www.example.com")
        },

        // License info shown in Swagger UI
        License = new OpenApiLicense
        {
            Name = "License Terms",
            Url = new Uri("https://www.example.com")
        }
    });
});

// Build the application
var app = builder.Build();

// Configure the middleware pipeline (handles how HTTP requests are processed)

// Redirect HTTP to HTTPS for security
app.UseHttpsRedirection();

// Enable authorization (used if [Authorize] attributes are present in controllers)
app.UseAuthorization();

// Enable Swagger middleware to generate the Swagger JSON endpoint
app.UseSwagger();

// Enable the Swagger UI, a web-based interface to interact with your API
app.UseSwaggerUI(c =>
{
    // Define the endpoint for Swagger JSON
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Demo");
});

// Map controller routes (e.g., api/employee)
app.MapControllers();

// Run the application
app.Run();
