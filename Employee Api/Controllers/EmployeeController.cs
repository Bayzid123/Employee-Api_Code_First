using Employee_Api.Helper;
using Employee_Api.Interfaces;
using Employee_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

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
        [Route("GetAllEmployeeDetails")]
        public async Task<ActionResult<List<Employee>>> GetEmployee()
        {
            return await _employee.GetEmployee();
        }

        [HttpGet]
        [Route("GetEmployeebyId")]

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
        [Route("CreateNewEmployee")]

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
        [Route("UpdateEmployeeDetails")]

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
        [Route("DeleteEmployee")]

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

        [HttpPost]
        [Route("SaveButtonApi")]

        public async Task<ActionResult<string>> SaveEmployee(List<Employee> employee)
        {
            try
            {
                return await _employee.SaveEmployee(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        [Route("GetAllEmployeesBySP")]

        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(await Task.FromResult(_employee.GetEmployees()));
                return Ok(jsonString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("UpdateEmployeeBySP")]
        public async Task<ActionResult> Update(Employee employee)
        {
            try
            {
                var res = JsonConvert.SerializeObject(await _employee.Update(employee)); ;
                return Ok(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteEmployeeBySP")]

        public async Task<ActionResult> Delete(int Id)
        {
            try
            {
                var res = JsonConvert.SerializeObject(await _employee.Delete(Id));
                return Ok(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
