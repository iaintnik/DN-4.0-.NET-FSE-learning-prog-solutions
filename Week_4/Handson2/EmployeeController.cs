// Import required namespaces
using Microsoft.AspNetCore.Mvc;              // Required for ControllerBase, ActionResult, etc.
using System.Collections.Generic;            // For using List<T>

namespace MySwaggerAPI.Controllers            // Namespace for the controller
{
    // This class is a Web API controller
    [ApiController]                          // Indicates this is an API controller (adds automatic model validation, binding, etc.)
    [Route("api/[controller]")]              // Sets route to api/employee (controller name without "Controller")
    public class EmployeeController : ControllerBase  // Inherits from ControllerBase (for Web API functionality)
    {
        // In-memory static list of employee names
        static List<string> employees = new List<string> { "Alice", "Bob", "Charlie" };

        // GET: api/employee
        // Returns the entire list of employees
        [HttpGet]                             // This method responds to GET requests at api/employee
        public ActionResult<IEnumerable<string>> GetAll()
        {
            return Ok(employees);             // Returns 200 OK with the list of employees
        }

        // GET: api/employee/{id}
        // Returns a single employee name by index
        [HttpGet("{id}")]                     // This method responds to GET requests with an id (e.g., api/employee/1)
        public ActionResult<string> GetById(int id)
        {
            if (id < 0 || id >= employees.Count)  // Check if the id is valid
                return NotFound();               // Returns 404 Not Found if index is out of range

            return Ok(employees[id]);            // Returns 200 OK with the employee name
        }

        // POST: api/employee
        // Adds a new employee to the list
        [HttpPost]                              // This method responds to POST requests (e.g., adding data)
        public ActionResult Add([FromBody] string name) // Reads the name from the request body
        {
            employees.Add(name);                // Adds the name to the list
            return Ok("Added");                 // Returns 200 OK with a confirmation message
        }

        // PUT: api/employee/{id}
        // Updates an employee name by index
        [HttpPut("{id}")]                       // This method responds to PUT requests (used for updates)
        public ActionResult Update(int id, [FromBody] string name) // Takes id from URL and new name from body
        {
            if (id < 0 || id >= employees.Count)  // Check if id is valid
                return NotFound();               // Returns 404 Not Found if invalid

            employees[id] = name;                // Updates the employee name at the given index
            return NoContent();                  // Returns 204 No Content (successful, no response body)
        }

        // DELETE: api/employee/{id}
        // Deletes an employee by index
        [HttpDelete("{id}")]                    // This method responds to DELETE requests with an id
        public ActionResult Delete(int id)
        {
            if (id < 0 || id >= employees.Count)  // Check if id is valid
                return NotFound();               // Returns 404 if index is invalid

            employees.RemoveAt(id);              // Removes the employee at the given index
            return Ok("Deleted");                // Returns 200 OK with a confirmation message
        }
    }
}
