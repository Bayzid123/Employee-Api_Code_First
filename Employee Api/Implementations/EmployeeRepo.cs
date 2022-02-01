using Employee_Api.Data;
using Employee_Api.Interfaces;
using Employee_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_Api.Implementations
{
    public class EmployeeRepo : IEmployee
    {
        private readonly EmployeeContext _context;

        public EmployeeRepo(EmployeeContext context)
        {
            _context = context;
        }

        //Get All Employee
        public async Task<ActionResult<List<Employee>>> GetEmployee()
        {
            return await _context.Employees.ToListAsync();
        }

        //Get Employee By Id

        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            try
            {
                var empInfo = await _context.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (empInfo == null)
                    throw new Exception("Employee Not Found");
                return empInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Create Employee

        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                var createEmployee = new Employee
                {
                    Name = employee.Name,
                    Age = employee.Age,
                    Designation = employee.Designation,
                };
                _context.Add(createEmployee);
                await _context.SaveChangesAsync();
                return createEmployee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Update Employee

        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
            try
            {
                var updateEmployee = await _context.Employees.FindAsync(employee.Id);
                if (updateEmployee == null)
                    throw new Exception("Employee Not Found");
                updateEmployee.Name = employee.Name;
                updateEmployee.Age = employee.Age;
                updateEmployee.Designation = employee.Designation;
                await _context.SaveChangesAsync();
                return updateEmployee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Delete Employee

        public async Task<string> DeleteEmployee(int id)
        {
            try
            {
                var deleteEmployee = await _context.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (deleteEmployee == null)
                    throw new Exception("Employee Not Found");

                _context.Employees.Remove(deleteEmployee);
                await _context.SaveChangesAsync();

                return "Delete Successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
