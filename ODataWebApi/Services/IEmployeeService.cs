using Microsoft.AspNetCore.Mvc;
using ODataWebApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ODataWebApi.Services
{
    public interface IEmployeeService
    {
        IQueryable<Employee> GetEmployeeById(int id);
        IQueryable<Employee> GetEmployees();

        public void Create(Employee employee);
        public void Update(Employee employee);
        public void Delete(Employee employee);
    }
}