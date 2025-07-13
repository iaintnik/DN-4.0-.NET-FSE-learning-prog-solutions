Q1: Explain the concept of RESTful web service, Web API & Microservice.
A1: A RESTful web service is a web-based architecture that uses HTTP protocols to perform operations such as GET, POST, PUT, and DELETE on resources identified by URLs. 
It is based on the REST (Representational State Transfer) architectural style, which emphasizes stateless communication, resource-based interactions, and standard methods. 
A Web API is a framework in ASP.NET Core that allows the creation of HTTP services accessible from various clients like browsers, mobile apps, etc.
A Microservice is an independent, small service that performs a specific business function and communicates with other services over the network, often using REST APIs.
Unlike traditional web services, RESTful APIs are not restricted to XML and commonly use JSON.

Q2: What are the features of REST architecture?
A2: The main features of REST architecture include:
Representational State Transfer (REST): Each resource is identified by a unique URI, and resources can be represented in multiple formats (JSON, XML, etc.).
Stateless: The server does not store any client session state. Each request from the client must contain all information needed for the server to process it.
Messages: Communication between client and server is done using standardized messages like HTTP requests and responses.
Not XML-only: REST APIs are not restricted to sending XML; they commonly use lightweight formats like JSON.
Microservice Compatible: REST architecture is suitable for developing microservices due to its decoupled and stateless nature.

Q3: Explain what is HttpRequest and HttpResponse.
A3: An HttpRequest is a message sent by the client to request resources or perform actions on the server. 
It contains the HTTP method (GET, POST, etc.), headers, body, and URI.
An HttpResponse is the server's reply to the client's request, containing a status code, headers, and an optional response body.
Together, these enable the client-server communication in Web APIs.

Q4: What are the types of Action Verbs used in Web API?
A4: The primary HTTP action verbs used in Web API are:
HttpGet: Used to retrieve data from the server.
HttpPost: Used to submit data to be processed or added on the server.
HttpPut: Used to update existing data on the server.
HttpDelete: Used to delete a resource on the server.
These verbs are declared using attributes like [HttpGet], [HttpPost], etc., above controller methods.

Q5: What are the common HttpStatusCodes used in Web API?
A5: Some commonly used HTTP status codes in Web APIs include:
200 OK: Request processed successfully.
201 Created: New resource created successfully.
204 No Content: The request was successful but no content is returned.
400 Bad Request: The request has invalid syntax or data.
401 Unauthorized: The request requires user authentication.
404 Not Found: The requested resource was not found.
500 Internal Server Error: A generic server-side error.
In ASP.NET Core, these are returned using helper methods like Ok(), BadRequest(), NotFound(), CreatedAtAction(), and NoContent().

Q6: Demonstrate the creation of a simple Web API with Read and Write actions.
A6: In the example code, a controller named ValuesController is created. It inherits from ControllerBase, which is the base class in ASP.NET Core for Web API controllers (without view support). This controller contains in-memory data (a static list of strings) and provides full CRUD operations using action verbs.
Each method is decorated with appropriate HTTP verb attributes:
[HttpGet] to return all values or a value by ID
[HttpPost] to add a new value
[HttpPut("{id}")] to update a value by index
[HttpDelete("{id}")] to remove a value by index
The return types are wrapped in ActionResult<T> or ActionResult to allow sending status codes and responses together.

Q7: What is ControllerBase and why is it used in the given code?
A7: ControllerBase is the base class for Web API controllers in ASP.NET Core. 
It provides methods and properties needed to handle HTTP requests, such as Ok(), NotFound(), BadRequest(), etc. 
It does not support views, which makes it suitable for pure API scenarios.
In the provided code, ValuesController inherits from ControllerBase because it's meant to serve JSON data via HTTP endpoints and does not use Razor Views.

Q8: Explain the code implementation in ValuesController.
A8: The ValuesController is defined in the FirstWebAPI.Controllers namespace and decorated with [ApiController] and [Route("api/[controller]")], which auto-maps the controller to the route api/values.
Inside the class:
A static list values stores in-memory string data.
The Get() method returns all values as a list using Ok(values).
The Get(int id) method fetches a specific value by index and returns NotFound if the index is invalid.
The Post() method accepts a new string value in the body, validates it, adds it to the list, and returns 201 Created with CreatedAtAction.
The Put() method updates a value at a specified index, returning 204 No Content upon success or NotFound if the index is invalid.
The Delete() method removes a value at a specific index, returning 200 OK or NotFound as appropriate.
This code demonstrates the full structure of a RESTful Web API using .NET Core.

Q9: What are the types of configuration files in Web API?
A9: The common configuration files in ASP.NET Core Web API include:
appsettings.json: Stores application settings such as connection strings, logging, and custom configuration values.
launchSettings.json: Contains settings related to how the application is launched (profiles for IIS Express or Kestrel, launch URLs, environment).
Program.cs / Startup.cs: Used to configure services and middleware in the application. In .NET Core 3.1 and .NET 5+, Startup.cs is used, while in .NET 6+, Program.cs handles both bootstrapping and configuration.
In .NET Framework 4.5 Web API:
WebApiConfig.cs: Defines Web API routes using config.MapHttpRoute.
RouteConfig.cs: Used in MVC apps for route mapping.
Web.config: XML file used for app settings, database configuration, and other environment-level settings.

