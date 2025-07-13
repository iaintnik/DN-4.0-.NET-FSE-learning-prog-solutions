using Microsoft.AspNetCore.Mvc;
using EmployeeAPI.Models;
using EmployeeAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeAPI.Controllers
{
    [CustomAuthFilter]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private List<Employee> _employees;

        public EmployeeController()
        {
            _employees = GetStandardEmployeeList();
        }

        private List<Employee> GetStandardEmployeeList()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "Alice",
                    Salary = 60000,
                    Permanent = true,
                    Department = new Department { Id = 1, Name = "HR" },
                    Skills = new List<Skill>
                    {
                        new Skill { Id = 1, Name = "Communication" },
                        new Skill { Id = 2, Name = "Leadership" }
                    },
                    DateOfBirth = new DateTime(1990, 1, 1)
                }
            };
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Employee>), 200)]
        public ActionResult<List<Employee>> Get()
        {
            return Ok(_employees);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            _employees.Add(employee);
            return Ok(employee);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(400)]
        public ActionResult<Employee> Put(int id, [FromBody] Employee updated)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid employee id");
            }

            var existingEmployee = _employees.FirstOrDefault(e => e.Id == id);
            if (existingEmployee == null)
            {
                return BadRequest("Invalid employee id");
            }

            // Update fields
            existingEmployee.Name = updated.Name;
            existingEmployee.Salary = updated.Salary;
            existingEmployee.Permanent = updated.Permanent;
            existingEmployee.Department = updated.Department;
            existingEmployee.Skills = updated.Skills;
            existingEmployee.DateOfBirth = updated.DateOfBirth;

            return Ok(existingEmployee);
        }
    }
}
