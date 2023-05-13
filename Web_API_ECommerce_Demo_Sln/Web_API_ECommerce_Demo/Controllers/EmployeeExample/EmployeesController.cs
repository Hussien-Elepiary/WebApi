using ECommerce_Demo_Core.Entities;
using ECommerce_Demo_Core.Entities.EmployeeExample;
using ECommerce_Demo_Core.Repositories;
using ECommerce_Demo_Core.Specifications;
using ECommerce_Demo_Core.Specifications.EmployeeExample;
using Microsoft.AspNetCore.Mvc;

namespace Web_API_ECommerce_Demo.Controllers.EmployeeExample
{
    public class EmployeesController : BaseApiController
    {
        private readonly IGenericRepository<Employee> _employeesRepo;

        public EmployeesController(IGenericRepository<Employee> employeesRepo)
        {
            _employeesRepo = employeesRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var spec = new EmployeeWithDepartmentSpec();

            var employees = await _employeesRepo.GetAllWithSpecAsync(spec);
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetProductWithSpec(int id)
        {
            var spec = new EmployeeWithDepartmentSpec(id);

            var employee = await _employeesRepo.GetWithSpecAsync(spec);
            return Ok(employee);
        }
    }
}
