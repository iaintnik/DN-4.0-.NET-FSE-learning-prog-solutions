using Microsoft.AspNetCore.Mvc;
using EmployeeAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using EmployeeAPI.Filters;

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
        public IActionResult Put(int id, [FromBody] Employee updated)
        {
            var emp = _employees.FirstOrDefault(e => e.Id == id);
            if (emp == null) return NotFound();

            emp.Name = updated.Name;
            emp.Salary = updated.Salary;
            return Ok(emp);
        }
    }
}
