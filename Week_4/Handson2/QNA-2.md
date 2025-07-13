ASP.NET Core Web API - Complete Developer Guide
What is Web API?
A Web API (Application Programming Interface) allows applications to communicate over HTTP. In
ASP.NET Core, Web APIs are used to expose endpoints that clients like mobile apps, web apps, or
IoT devices can consume. APIs typically use RESTful architecture principles, returning data (usually
JSON) in response to HTTP requests.
Types of Dependency Injection (DI) in ASP.NET Core
1. Constructor Injection (Most Common)
The dependencies are provided through the constructor. This is the most preferred and widely used
form of DI in ASP.NET Core.
Example:
public interface IEmployeeService {
List<string> GetAll();
}
public class EmployeeService : IEmployeeService {
public List<string> GetAll() {
return new List<string> { "Alice", "Bob" };
}
}
public class EmployeeController : ControllerBase {
private readonly IEmployeeService _service;
public EmployeeController(IEmployeeService service) {
_service = service;
}
[HttpGet]
public IActionResult Get() => Ok(_service.GetAll());
}
2. Property Injection
The dependency is set using a public property instead of through the constructor.
Example:
public class MyService {
public ILogger Logger { get; set; }
}
Property injection is less common and not automatically handled by the built-in ASP.NET Core
container.
3. Method Injection
Dependencies are passed directly to methods that need them.
Example:
public IActionResult Process(IEmployeeService service) {
var result = service.GetAll();
return Ok(result);
}
Service Lifetimes:
- Singleton: One instance for the entire application lifetime.
builder.Services.AddSingleton<IService, Service>();
- Scoped: One instance per request.
builder.Services.AddScoped<IService, Service>();
- Transient: A new instance every time it is requested.
builder.Services.AddTransient<IService, Service>();
Middleware in ASP.NET Core
Middleware are software components that handle requests and responses. Each piece of
middleware performs some operation on the HTTP request and either passes it to the next
component or short-circuits the pipeline.
Common Middleware:
- app.UseHttpsRedirection(): Redirect HTTP to HTTPS.
- app.UseRouting(): Enables routing of requests.
- app.UseAuthorization(): Applies authorization rules.
- app.UseAuthentication(): Checks authentication tokens.
- app.UseSwagger(): Generates Swagger spec.
- app.UseSwaggerUI(): Enables interactive Swagger interface.
Creating Custom Middleware:
public class LoggingMiddleware {
private readonly RequestDelegate _next;
public LoggingMiddleware(RequestDelegate next) {
_next = next;
}
public async Task Invoke(HttpContext context) {
Console.WriteLine($"Request: {context.Request.Path}");
await _next(context); // Call the next middleware
Console.WriteLine($"Response: {context.Response.StatusCode}");
}
}
Register it in Program.cs:
app.UseMiddleware<LoggingMiddleware>();
Web API Routing
Routing maps HTTP requests to controller actions.
Attribute Routing:
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase {
[HttpGet("{id}")]
public IActionResult GetById(int id) { ... }
}
Action Method Attributes:
- [HttpGet], [HttpPost], [HttpPut], [HttpDelete]
- You can define custom routes with parameters
Model Validation:
Use annotations like:
- [Required]
- [StringLength]
- [Range]
Validate manually using:
if (!ModelState.IsValid) return BadRequest(ModelState);
Swagger Setup
Install NuGet: Swashbuckle.AspNetCore
In Program.cs:
builder.Services.AddSwaggerGen();
app.UseSwagger();
app.UseSwaggerUI();
Then access Swagger UI at:
https://localhost:<port>/swagger
Security Considerations:
- Use HTTPS (app.UseHttpsRedirection())
- Secure endpoints with JWT, OAuth, or cookie-based auth
- Validate all input to prevent SQL injection, XSS, etc.
- Limit public exposure of sensitive APIs
Testing Tools:
- Swagger UI: Interactive documentation and testing
- Postman: API request testing tool
- Unit Testing: Use xUnit/NUnit with mocking (e.g., Moq)
Best Practices Summary:
- Use Dependency Injection for services and repositories
- Keep controllers thin; use services for business logic
- Return standard HTTP status codes
- Use DTOs instead of domain models for API responses
- Validate input properly
- Log errors and use global error handling middleware
- Secure your API with proper authentication/authorization
Conclusion:
This document serves as a complete foundational-to-advanced guide to ASP.NET Core Web API. It
covers concepts such as REST architecture, DI, Middleware, Swagger, routing, security, testing,
and best practices.
Understanding Dependency Injection (DI) and Swagger in ASP.NET Core
------------------------------
What is Dependency Injection?
------------------------------
Dependency Injection (DI) is a design pattern that enables the creation of dependent objects outside
of a class and provides those objects to a class in various ways.
Instead of the class creating the objects it needs (dependencies), they are "injected" into the class
from the outside.
------------------------------
Benefits of Dependency Injection:
------------------------------
- Loose Coupling: Components can evolve independently.
- Testability: Easier to mock dependencies during testing.
- Flexibility: Easily switch implementations.
- Maintainability: Centralized service management.
------------------------------
Types of Dependency Injection:
------------------------------
1. Constructor Injection (Most used in ASP.NET Core)
-> Inject services through a class constructor.
Example:
public class MyService {
private readonly IRepository _repo;
public MyService(IRepository repo) {
_repo = repo;
}
}
2. Property (Setter) Injection
-> Inject dependencies through public properties.
Example:
public class MyService {
public IRepository Repo { get; set; }
}
3. Method Injection
-> Pass dependencies directly into a method.
Example:
public void Execute(IRepository repo) {
// use repo
}
------------------------------
Service Lifetimes in ASP.NET Core:
------------------------------
- Singleton: One instance used throughout the application.
- Scoped: One instance per request (suitable for Web API).
- Transient: A new instance every time it is requested.
Syntax in Program.cs:
builder.Services.AddSingleton<IService, Service>();
builder.Services.AddScoped<IService, Service>();
builder.Services.AddTransient<IService, Service>();
------------------------------
Using Swagger in ASP.NET Core:
------------------------------
Swagger is a tool to generate interactive documentation for Web APIs.
1. Install via NuGet:
Install-Package Swashbuckle.AspNetCore
2. In Program.cs:
builder.Services.AddSwaggerGen(); // Register Swagger service
app.UseSwagger(); // Enable Swagger middleware
app.UseSwaggerUI(); // Enable Swagger UI
3. Access it in browser:
https://localhost:<port>/swagger
------------------------------
Program.cs - Line-by-Line Explanation
------------------------------
var builder = WebApplication.CreateBuilder(args);
- Creates a new WebApplicationBuilder with default settings.
builder.Services.AddControllers();
- Adds support for Web API controllers.
builder.Services.AddEndpointsApiExplorer();
- Enables minimal APIs and endpoint metadata.
builder.Services.AddSwaggerGen();
- Registers Swagger generator.
var app = builder.Build();
- Builds the application pipeline.
app.UseHttpsRedirection();
- Forces HTTPS for incoming requests.
app.UseAuthorization();
- Adds authorization middleware.
app.UseSwagger();
- Generates Swagger endpoint (swagger.json).
app.UseSwaggerUI();
- Provides Swagger UI at /swagger.
app.MapControllers();
- Maps attribute-routed controllers.
app.Run();
- Starts the app.
------------------------------
EmployeeController.cs - Line-by-Line Explanation
------------------------------
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace MySwaggerAPI.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
static List<string> employees = new List<string> { "Alice", "Bob", "Charlie" };
[HttpGet]
public ActionResult<IEnumerable<string>> GetAll()
{
return Ok(employees);
}
[HttpGet("{id}")]
public ActionResult<string> GetById(int id)
{
if (id < 0 || id >= employees.Count)
return NotFound();
return Ok(employees[id]);
}
[HttpPost]
public ActionResult Add([FromBody] string name)
{
employees.Add(name);
return Ok("Added");
}
[HttpPut("{id}")]
public ActionResult Update(int id, [FromBody] string name)
{
if (id < 0 || id >= employees.Count)
return NotFound();
employees[id] = name;
return NoContent();
}
[HttpDelete("{id}")]
public ActionResult Delete(int id)
{
if (id < 0 || id >= employees.Count)
return NotFound();
employees.RemoveAt(id);
return Ok("Deleted");
}
}
}
------------------------------
Key Takeaways:
------------------------------
- DI helps inject dependencies like services into controllers.
- Swagger simplifies API documentation and testing.
- Program.cs configures services and middleware.
- Controllers use attributes like [HttpGet], [HttpPost] for routing.
- DI improves maintainability, testability, and scalability.
Understanding Dependency Injection (DI) in ASP.NET Core
What is Dependency Injection?
Dependency Injection (DI) is a design pattern that allows a class to receive its dependencies from
external sources rather than creating them internally. It helps achieve loose coupling between
components.
Why Use Dependency Injection?
- Loose Coupling: Keeps components independent.
- Testability: Enables easy unit testing.
- Reusability: Share instances across classes.
- Flexibility: Swap implementations easily.
- Centralized Configuration: Manage lifetimes in Program.cs.
Types of DI:
1. Constructor Injection (most common in ASP.NET Core)
2. Setter Injection
3. Method Injection
Example Without DI (Tightly Coupled):
public class EmployeeService {
private readonly EmployeeRepository _repository = new EmployeeRepository();
}
Example With DI (Loosely Coupled):
public class EmployeeService {
private readonly IEmployeeRepository _repository;
public EmployeeService(IEmployeeRepository repository) {
_repository = repository;
}
}
Registering Services in Program.cs:
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
Service Lifetimes:
- Singleton: One instance for entire app lifetime
- Scoped: One instance per request (web API default)
- Transient: New instance every time
Example:
builder.Services.AddSingleton<ILog, LogService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<IMailService, MailService>();
Real Example in Web API:
1. Interface:
public interface IEmployeeService {
List<string> GetAll();
}
2. Implementation:
public class EmployeeService : IEmployeeService {
public List<string> GetAll() {
return new List<string> { "Alice", "Bob" };
}
}
3. Register in Program.cs:
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
4. Inject in Controller:
public class EmployeeController : ControllerBase {
private readonly IEmployeeService _employeeService;
public EmployeeController(IEmployeeService employeeService) {
_employeeService = employeeService;
}
[HttpGet]
public IActionResult Get() {
return Ok(_employeeService.GetAll());
}
}
Program.cs Explanation:
- AddControllers(): Registers controllers for Web API.
- AddSwaggerGen(): Adds Swagger generation service.
- app.UseHttpsRedirection(): Redirects HTTP to HTTPS.
- app.UseAuthorization(): Enables authorization middleware.
- app.UseSwagger(): Enables Swagger JSON endpoint.
- app.UseSwaggerUI(): Enables Swagger interactive UI.
- app.MapControllers(): Maps controller routes.
- app.Run(): Starts the application.
EmployeeController.cs Explanation:
- [ApiController]: Marks class as Web API controller.
- [Route("api/[controller]")]: Maps route to controller name.
- GetAll(): Returns list of employee names.
- GetById(): Returns employee by index.
- Add(): Adds a new employee.
- Update(): Updates an existing employee.
- Delete(): Removes an employee by index.
DI vs Non-DI:
- Non-DI: Manual object creation with new keyword (tightly coupled).
- DI: Objects injected through constructor, interface-based (loosely coupled).
Summary:
- Always use interface-based programming for DI.
- Register all services in Program.cs.
- Inject services via constructor in controllers.
