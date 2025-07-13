README: Hands-On 4 - ASP.NET Core Web API with CRUD, Filters, and Swagger
This project demonstrates how to build and test a RESTful Web API using ASP.NET Core. The API
exposes endpoints to manage a collection of employee data using standard CRUD operations
(Create, Read, Update, Delete). It also shows how to integrate custom filters for authorization and
exception handling, and how to document and test the API using Swagger UI and Postman.
Objective
The goal is to understand how to:
- Build a Web API using a custom model (Employee)
- Implement GET, POST, and PUT methods with proper data validation
- Use [FromBody] to bind complex input objects from JSON
- Create custom filters using ActionFilterAttribute and IExceptionFilter
- Use Swagger for interactive API documentation and testing
- Enforce authorization headers using a custom filter
Project Components
1. Model Classes
The Employee model represents each employee's data. It contains properties like Id, Name, Salary,
Department, Skills, and DateOfBirth. The Department and Skill models are custom types embedded
within the employee structure, representing one-to-one and one-to-many relationships respectively.
These models are designed to simulate a real-world object graph that includes nested and list-based
data structures. They're used for both input and output payloads in the API.
2. EmployeeController
This controller provides endpoints for:
- GET /api/employee: Returns a list of hardcoded employees
- POST /api/employee: Adds a new employee object (via [FromBody])
- PUT /api/employee/{id}: Updates an existing employee based on input JSON
The controller uses an in-memory (hardcoded) list of employees for simplicity. In a real-world
scenario, this would be replaced with a service or database context.
The PUT method validates:
- If the ID is less than or equal to zero => returns 400 Bad Request
- If the employee with the given ID doesn't exist => returns 400 Bad Request
- If valid, it updates all fields of the employee and returns 200 OK with the updated object
3. [FromBody] Attribute
The [FromBody] attribute is used to bind complex data types sent in the body of an HTTP request
(in JSON format) to method parameters in the controller. It is essential when using POST or PUT
operations to receive complete objects from clients. ASP.NET Core automatically deserializes the
JSON payload into the corresponding model object (Employee in this case).
4. Custom Authorization Filter
A class called CustomAuthFilter inherits from ActionFilterAttribute. It intercepts all incoming HTTP
requests to check if an Authorization header is present:
- If the header is missing, it returns a 400 Bad Request with the message: "Invalid request - No Auth
token"
- If the header is present but does not contain the word "Bearer", it returns another 400 Bad Request
with the message: "Invalid request - Token present but Bearer unavailable"
This filter is applied to the EmployeeController using the [CustomAuthFilter] attribute, ensuring every
action in that controller is protected.
5. Custom Exception Filter
The CustomExceptionFilter implements the IExceptionFilter interface. It catches any unhandled
exceptions thrown during the processing of a request. When an exception is caught:
- It writes the exception details to a file (C:\Logs\exception_log.txt)
- It returns a 500 Internal Server Error response to the client
This filter is added globally through the AddControllers() configuration in Program.cs, ensuring all
controllers benefit from centralized error handling.
6. Swagger Integration
Swagger (via Swashbuckle.AspNetCore) is configured in Program.cs to:
- Generate OpenAPI documentation for all endpoints
- Display request/response schemas automatically
- Provide a "Try it out" button for easy manual testing of endpoints
Additional configuration enables a Bearer token field through the Authorize button at the top right of
Swagger UI. This makes it possible to add authorization headers for testing secured endpoints
directly within Swagger.
7. Testing with Postman
In Postman, each endpoint can be tested by:
- Setting method type (GET, POST, or PUT)
- Setting headers (Content-Type: application/json, Authorization: Bearer 12345)
- Choosing Body -> raw -> JSON to send model data
- Reviewing the response, which returns standard HTTP status codes and JSON bodies
Testing Scenarios
Scenario | Method | Expected Result
----------------------------------- | ------------------ | -------------------------------
Get all employees | GET /api/employee | Returns employee list with 200
Add new employee | POST /api/employee | Returns the new employee with 200
Update valid employee | PUT /api/employee/1| Returns updated employee, 200 OK
Update with invalid ID | PUT /api/employee/99| Returns 400 Bad Request
Missing Authorization header | Any | Returns 400 with error message
Exception thrown manually | GET (with exception)| Returns 500 Internal Server Error, logs
error
Key Takeaways
- ASP.NET Core makes it easy to build clean, RESTful APIs using model binding, routing, and
controller actions
- Filters are a powerful feature that allows pre- and post-processing of requests and responses
- [FromBody] is required to read complex model data from the request payload
- Swagger provides an out-of-the-box UI for testing and exploring API endpoints
- Custom filters let you extend framework behavior for authorization and exception handling
- Postman complements Swagger by allowing you to simulate real-world client API calls
