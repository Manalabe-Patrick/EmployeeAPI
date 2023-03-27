using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Data
{
    public class EmployeeDBContext: DbContext  
    {
        public EmployeeDBContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

    } 
}
