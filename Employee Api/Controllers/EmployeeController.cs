using Employee_Api.Interfaces;
using Employee_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;

        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }

        [HttpGet]
        [Route("Get All Employee Details")]
        public async Task<ActionResult<List<Employee>>> GetEmployee()
        {
            return await _employee.GetEmployee();
        }

        [HttpGet]
        [Route("Get Employee by Id")]

        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            try
            {
                return await _employee.GetEmployeeById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("Create a New Employee")]

        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                return await _employee.CreateEmployee(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("Update Employee Details")]

        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
            try
            {
                return await _employee.UpdateEmployee(employee);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        [Route("Delete Employee")]

        public async Task<string> DeleteEmployee(int id)
        {
            try
            {
                return await _employee.DeleteEmployee(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
