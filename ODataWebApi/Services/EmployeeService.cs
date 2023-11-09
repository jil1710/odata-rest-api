using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ODataWebApi.AppContextDb;
using ODataWebApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ODataWebApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DemoContext demoContext;

        public EmployeeService(DemoContext demoContext)
        {
            this.demoContext = demoContext;
        }

        public IQueryable<Employee> GetEmployees()
        {
            return demoContext.Employees.AsQueryable();
        }

        public IQueryable<Employee> GetEmployeeById(int id)
        {
            return demoContext.Employees
                .Include(a => a.Department)
                .AsQueryable()
                .Where(c => c.EmployeeId == id);
        }

        public void Create(Employee employee)
        {
            demoContext.Employees
                .Add(employee);
            demoContext.SaveChanges();
        }
        public void Update(Employee employee)
        {
            demoContext.Employees
                .Update(employee);
            demoContext.SaveChanges();
        }
        public void Delete(Employee employee)
        {
            demoContext.Employees
                .Remove(employee);
            demoContext.SaveChanges();
        }

    }
}
