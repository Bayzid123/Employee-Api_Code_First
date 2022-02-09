using Employee_Api.Helper;
using Employee_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Employee_Api.Interfaces
{
    public interface IEmployee
    {
        public Task<ActionResult<List<Employee>>> GetEmployee();
        public Task<ActionResult<Employee>> GetEmployeeById(int id);
        public Task<ActionResult<Employee>> CreateEmployee(Employee employee);
        public Task<ActionResult<Employee>> UpdateEmployee(Employee employee);
        public Task<string> DeleteEmployee(int id);
        public Task<ActionResult<string>> SaveEmployee(List<Employee> employee);


        ////SP

        public DataTable GetEmployees();
        public Task<DataTable> Update(Employee employee);
        public Task<DataTable> Delete(int Id);
    }
}
