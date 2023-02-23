using TaskManagerApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TaskManagerApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)] 
public class EmployeeController:ControllerBase{
    IEmployeeService _employeeService;
    public EmployeeController(IEmployeeService service) {
        _employeeService = service;
    }

        /// <summary>
    /// get all employess
    /// </summary>
    /// <returns></returns>
    [HttpGet("emp")]
    [Authorize(Roles="admin")]
    public IActionResult GetAllEmployees() {
        try {
            var employees = _employeeService.GetEmployeesList();
            if (employees == null) return NotFound();
            return Ok(employees);
        } catch (Exception) {
            return BadRequest();
        }
    }

    /// <summary>
    /// get employee details by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("[action]/id")]
      [Authorize(Roles="user")]
    public IActionResult GetEmployeesById(int id) {
        try {
            var employees = _employeeService.GetEmployeeDetailsById(id);
            if (employees == null) return NotFound();
            return Ok(employees);
        } catch (Exception) {
            return BadRequest();
        }
    }
    
    /// <summary>
    /// save employee
    /// </summary>
    /// <param name="employeeModel"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("[action]")]
     [Authorize(Roles="admin")]
    public IActionResult SaveEmployees(Employee employeeModel) {
        try {
            var model = _employeeService.SaveEmployee(employeeModel);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    /// <summary>
    /// delete employee
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("[action]")]
     [Authorize(Roles="admin")]
    public IActionResult DeleteEmployee(int id) {
        try {
            var model = _employeeService.DeleteEmployee(id);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
}