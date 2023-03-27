using EmployeeAPI.Data;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly EmployeeDBContext _employeeDBContext;
        public EmployeesController(EmployeeDBContext employeeDBContext)
        {
            _employeeDBContext = employeeDBContext;
        }

        //get employees
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeDBContext.Employees.OrderBy(e => e.LastName).ToListAsync();

            return Ok(employees);
        }

        //add employee
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeData)
        {
            employeeData.Id = Guid.NewGuid();
            await _employeeDBContext.Employees.AddAsync(employeeData);
            await _employeeDBContext.SaveChangesAsync();

            return Ok(employeeData);
        }

        //edit employee data
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployee)
        {
            var employee = await _employeeDBContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.FirstName = updateEmployee.FirstName;
            employee.LastName = updateEmployee.LastName;
            employee.MiddleName = updateEmployee.MiddleName;
            employee.Contact = updateEmployee.Contact;
            employee.HouseNumber = updateEmployee.HouseNumber;
            employee.DateOfBirth = updateEmployee.DateOfBirth;
            employee.Street = updateEmployee.Street;
            employee.Village = updateEmployee.Village;
            employee.Barangay = updateEmployee.Barangay;
            employee.Town = updateEmployee.Town;
            employee.City = updateEmployee.City;
            employee.Province = updateEmployee.Province;
            employee.ZipCode = updateEmployee.ZipCode;

            await _employeeDBContext.SaveChangesAsync();

            return Ok(employee);
        }

        //remove employee
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _employeeDBContext.Employees.FindAsync(id);

            if(employee == null)
            {
                return NotFound();
            }

            _employeeDBContext.Employees.Remove(employee);
            await _employeeDBContext.SaveChangesAsync();

            return Ok(employee);
        }
    } 
}
