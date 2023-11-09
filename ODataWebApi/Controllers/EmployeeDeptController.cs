using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ODataWebApi.AppContextDb;
using ODataWebApi.Models;
using ODataWebApi.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ODataWebApi.Controllers
{
    
    public class EmployeeDeptController : ODataController
    {
        private readonly IEmployeeService emp;

        public EmployeeDeptController(IEmployeeService emp)
        {
            this.emp = emp;
        }



        [HttpGet]
        [EnableQuery]
        public IQueryable<Employee> Get()
        {
            return emp.GetEmployees();
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public SingleResult<Employee> Get([FromODataUri] int key)
        {
            return SingleResult.Create(emp.GetEmployeeById(key));
        }


        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            emp.Create(employee);

            return Created("Employee", employee);
        }


        [HttpPut]
        public IActionResult Put([FromODataUri] int key, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != employee.EmployeeId)
            {
                return BadRequest();
            }

            emp.Update(employee);

            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete([FromODataUri] int key)
        {
            var employee = emp.GetEmployeeById(key);
            if (employee is null)
            {
                return BadRequest();
            }

            emp.Delete(employee.First());

            return NoContent();
        }
    }
}
