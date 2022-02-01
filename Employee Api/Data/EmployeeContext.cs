using Employee_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Api.Data
{
    public class EmployeeContext:DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }

        public DbSet<Employee>Employees { get; set; }
    }
}
