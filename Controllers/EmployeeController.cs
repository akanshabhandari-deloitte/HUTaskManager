using TaskManagerApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TaskManagerApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)] 
public class EmployeeController:ControllerBase{
    IEmployeeService _employeeService;
    private readonly ILogger<Employee> _logger;

    public EmployeeController(IEmployeeService service,ILogger<Employee> logger) {
        _employeeService = service;
        _logger=logger;
    }
    [HttpGet("emp")]
    [Authorize(Roles="admin")]
    public IActionResult GetAllEmployees() {
        try {
            _logger.LogInformation("List of emp");
            var employees = _employeeService.GetEmployeesList();
            if (employees == null) return NotFound();
            return Ok(employees);
        } catch (Exception) {
            _logger.LogError("This is an error message.");
            return BadRequest();
        }
    }
    [HttpGet]
    [Route("[action]/id")]
      [Authorize(Roles="admin")]
    public IActionResult GetEmployeesById(int id) {
        try {
            var employees = _employeeService.GetEmployeeDetailsById(id);
                _logger.LogInformation("Get Emp by ID");
            if (employees == null) 
            {
                _logger.LogError("Employee not found");
                return NotFound();
            }
            return Ok(employees);
        } catch (Exception) {
            return BadRequest();
        }
    }
  
    [HttpPost]
    [Route("[action]")]
     [Authorize(Roles="admin")]
    public IActionResult SaveEmployees(Employee employeeModel) {
        try {
            var model = _employeeService.SaveEmployee(employeeModel);
              _logger.LogInformation("Save Employee");
            return Ok(model);
        } catch (Exception) {
            _logger.LogError("Employee not Saved");
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("[action]")]
     [Authorize(Roles="admin")]
    public IActionResult DeleteEmployee(int id) {
        try {
            var model = _employeeService.DeleteEmployee(id);
              _logger.LogInformation("Save Employee");
            return Ok(model);
        } catch (Exception) {
             _logger.LogError("Employee not Deleted");
            return BadRequest();
        }
    }
}