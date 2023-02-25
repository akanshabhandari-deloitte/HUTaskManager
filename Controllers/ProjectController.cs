using TaskManagerApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TaskManagerApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)] 
public class ProjectController:ControllerBase{
    IProjectService _projectService;
    private TaskManagerContext _db;
    public ProjectController(IProjectService service,TaskManagerContext db) {
        _projectService = service;
        _db=db;
    }

        /// <summary>
    /// get all employess
    /// </summary>
    /// <returns></returns>
    [HttpGet("project")]
    // [Authorize(Roles="admin")]
    public IActionResult GetAllProject() {
        try {
            var employees = _projectService.GetProjectList();
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
    [HttpPost("SaveProject/{id}")]
    // [Route("[action]")]
    //  [Authorize(Roles="admin")]
    public IActionResult SaveProject(int id,[FromBody] Project project) {  
        try {
        //  Console.WriteLine("save");
        //  List<Employee> list = new List<Employee>();  
        //  list = _db.Set < Employee > ().ToList(); 
        // var obj =list.FirstOrDefault(x => x.EmployeeId==id);  
        //  if (obj == null)
        // {
        //     return NotFound();
        
        // } 
        var model = _projectService.SaveProject(id,project);
          
        
        return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
}