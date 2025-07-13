Overview of ASP.NET Core Web API Implementation
In this application, we build a simple RESTful Web API using ASP.NET Core.
The API allows you to manage Employee data through standard HTTP methods like GET, POST, and PUT.
The data is modeled using a custom class Employee that contains nested types like Department and a list of Skill.
We also integrate important features like model binding using [FromBody], custom filters for authorization and exception handling, and expose all endpoints through Swagger for easy testing and visualization.

Model Binding and [FromBody]
In ASP.NET Core, model binding maps data from HTTP requests to action method parameters. 
For complex objects like the Employee model, ASP.NET Core binds data from the request body using the [FromBody] attribute. This tells the framework to read the incoming JSON and map it to the corresponding C# object. This is especially useful in POST and PUT operations where the full object is submitted in the request body.

Action Methods and Controllers
The controller class EmployeeController defines various endpoints.
The GET method returns a list of employees using a helper method GetStandardEmployeeList(). 
The POST method accepts an Employee object from the body and adds it to the list.
The PUT method updates an existing employee by ID.
All responses return appropriate HTTP status codes and JSON results using the ActionResult<T> wrapper.

Dependency Injection and Service Registration
In this particular case, dependency injection is not explicitly demonstrated, but ASP.
NET Core supports registering services in Program.cs using AddScoped, AddSingleton, or AddTransient.
For example, if you were managing data through a service layer, you'd inject the service into your controller through the constructor. 
The ASP.NET Core framework automatically resolves and provides the required service objects.

Custom Filters in ASP.NET Core
ASP.NET Core provides a powerful filtering system to execute code before or after certain stages in the request processing pipeline. There are different types of filters:
Authorization Filters – Run early in the request pipeline to determine whether a user is authorized to access a resource.
Resource Filters – Run after authorization but before model binding.
Action Filters – Run just before and after an action method is called.
Exception Filters – Run when unhandled exceptions occur during the action method execution or later in the pipeline.
Result Filters – Run just before and after the action result is executed.
Custom Authorization Filter
In this project, a custom authorization filter is created by inheriting from ActionFilterAttribute.
The CustomAuthFilter overrides the OnActionExecuting method to check whether the incoming HTTP request contains an Authorization header.
If it doesn't, a BadRequestObjectResult is returned with a specific message. 
If the header exists but doesn’t contain the word "Bearer", another validation error is returned.
This demonstrates how you can intercept requests to implement simple custom authentication logic.
Custom Exception Filter
The CustomExceptionFilter implements the IExceptionFilter interface. This filter captures any unhandled exceptions that occur during request processing.
When an exception is caught, it writes the exception details to a local file and returns a generic Internal Server Error (HTTP 500) to the client. 
This promotes cleaner controller code and ensures consistent error handling throughout the application. 
The filter is registered globally in the Program.cs file, so it's applied to all controllers automatically.

Program.cs Configuration
The Program.cs file configures essential services and middleware required by the application.
It registers controllers, enables Swagger for API documentation, and sets up the HTTP request pipeline with middleware such as HTTPS redirection and authorization.
The CustomExceptionFilter is added globally during service registration, so exception handling is centralized.

Swagger Integration
Swagger is used to document and test the Web API.
It provides an interactive UI where users can see available endpoints, their request parameters, and response formats. 
With Swagger, developers can test endpoints like GET, POST, and PUT directly from the browser without needing external tools.
Swagger also allows adding headers (like the Authorization header) for testing protected endpoints.

HTTP Status Codes
The Web API uses various HTTP status codes to communicate the result of requests:
200 OK: Successful requests (GET, POST, PUT).
400 Bad Request: Used in the custom authorization filter when required headers are missing or invalid.
500 Internal Server Error: Returned by the exception filter when an unhandled exception occurs in the controller.
Using standard status codes ensures clients can correctly interpret the result of their API calls.
