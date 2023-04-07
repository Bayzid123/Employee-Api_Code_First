using Employee_Api.Data;
using Employee_Api.DTO;
using Employee_Api.Helper;
using Employee_Api.Interfaces;
using Employee_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;

namespace Employee_Api.Implementations
{
    public class EmployeeRepo : IEmployee
    {
        private readonly EmployeeContext _context;

        DataTable dt = new DataTable();


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
                throw;
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
                    IsActive = employee.IsActive,
                };
                _context.Add(createEmployee);
                await _context.SaveChangesAsync();
                return createEmployee;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //Update Employee

        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
            try
            {
                var updateEmployee = await _context.Employees.Where(x => x.Id == employee.Id).FirstOrDefaultAsync();
                if (updateEmployee == null)
                    throw new Exception("Employee Not Found");
                updateEmployee.Name = employee.Name;
                updateEmployee.Age = employee.Age;
                updateEmployee.Designation = employee.Designation;
                updateEmployee.IsActive = employee.IsActive;

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

        //Save Button Api

        public async Task<ActionResult<string>> SaveEmployee(List<Employee> employee)
        {
            try
            {

                var itemList = new List<Employee>();
                foreach (var item in employee)
                {
                    if (item.Id > 0)
                    {

                        var updateEmployee = await _context.Employees.Where(x => x.Id == item.Id).FirstOrDefaultAsync();

                        updateEmployee.Name = item.Name;
                        updateEmployee.Age = item.Age;
                        updateEmployee.Designation = item.Designation;

                        _context.Employees.Update(updateEmployee);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        var createEmployee = new Employee
                        {
                            Name = item.Name,
                            Age = item.Age,
                            Designation = item.Designation,
                            IsActive = true,
                        };
                        itemList.Add(createEmployee);
                    }
                }

                var inactiveList = (from e in _context.Employees
                                    where !employee.Select(x => x.Id).ToList().Contains(e.Id)
                                    select e).ToList();

                inactiveList.ForEach(x => { x.IsActive = false; });

                _context.Employees.UpdateRange(inactiveList);
                await _context.SaveChangesAsync();


                await _context.Employees.AddRangeAsync(itemList);
                await _context.SaveChangesAsync();
                return "success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        ////Store Procedure//////
        public DataTable GetEmployees()
        {

            try
            {

                using (var connection = new SqlConnection
                    ("server=DESKTOP-TFFNGL1\\SQLEXPRESS;database=EmployeeCrud;trusted_connection=true;"))
                {
                    string sql = "dbo.GetEmployees";
                    using (SqlCommand sqlCmd = new SqlCommand(sql, connection))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            sqlAdapter.Fill(dt);
                        }
                        connection.Close();
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DataTable> Update(Employee employee)
        {
            try
            {
                using (var connection = new SqlConnection
                    ("server=DESKTOP-TFFNGL1\\SQLEXPRESS;database=EmployeeCrud;trusted_connection=true;"))
                {
                    string sql = "dbo.UpdateEmployee";
                    using (SqlCommand sqlCmd = new SqlCommand(sql, connection))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@Id", employee.Id);
                        sqlCmd.Parameters.AddWithValue("@Name", employee.Name);
                        sqlCmd.Parameters.AddWithValue("@Age", employee.Age);
                        sqlCmd.Parameters.AddWithValue("@Designation", employee.Designation);
                        sqlCmd.Parameters.AddWithValue("@IsActive", employee.IsActive);

                        SqlParameter outputParameter = new SqlParameter();
                        outputParameter.ParameterName = "@msg";
                        outputParameter.SqlDbType = System.Data.SqlDbType.VarChar;
                        outputParameter.Size = int.MaxValue;
                        outputParameter.Direction = System.Data.ParameterDirection.Output;
                        sqlCmd.Parameters.Add(outputParameter);
                        connection.Open();
                        sqlCmd.ExecuteNonQuery();
                        var msg = outputParameter.Value.ToString();
                        //connection.Open();
                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            sqlAdapter.Fill(dt);
                        }
                        connection.Close();
                        
                    }
                }
                return dt;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DataTable> Delete(int Id)
        {
            try
            {
                using (var connection = new SqlConnection
                    ("server=DESKTOP-TFFNGL1\\SQLEXPRESS;database=EmployeeCrud;trusted_connection=true;"))
                {
                    string sql = "dbo.DeleteEmployee";
                    using (SqlCommand sqlCmd = new SqlCommand(sql, connection))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@Id", Id);
                        connection.Open();
                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            sqlAdapter.Fill(dt);
                        }
                        connection.Close();
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
