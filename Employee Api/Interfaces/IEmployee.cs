using Employee_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Api.Interfaces
{
    public interface IEmployee
    {
        public Task<ActionResult<List<Employee>>> GetEmployee();
        public Task<ActionResult<Employee>> GetEmployeeById(int id);
        public Task<ActionResult<Employee>> CreateEmployee(Employee employee);
        public Task<ActionResult<Employee>> UpdateEmployee(Employee employee);
        public Task<string> DeleteEmployee(int id);
    }
}
