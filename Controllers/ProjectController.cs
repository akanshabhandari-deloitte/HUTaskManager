using TaskManagerApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TaskManagerApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

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


     [HttpGet]
    [Route("[action]/id")]
    //   [Authorize(Roles="user")]
    public IActionResult GetProjectsById(int id) {
        try {
            var employees = _projectService.GetProjectDetailsById(id);
            if (employees == null) return NotFound();
            return Ok(employees);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProject(int id, [FromBody] Project updatedProject)
      {
    if (updatedProject == null)
    {
        return BadRequest();
    }
    try
    { 
            _projectService.UpdateProject(id,updatedProject);
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!IssueExists(id))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }

    return Ok();
}

    private bool IssueExists(int id)
    {
        throw new NotImplementedException();
    }


    [HttpDelete]
    [Route("[action]")]
    //  [Authorize(Roles="admin")]
    public IActionResult DeleteProject(int id) {
        try {
            var model = _projectService.DeleteProject(id);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

//  [HttpGet]
//     [Route("[action]/id")]
//     public IActionResult GetIssuesByProject(int id) {
//         try {
//             var issuesbyproject = _projectService.GetIssuesByProject( id);
//             if (issuesbyproject == null) return NotFound();
//             return Ok(issuesbyproject);
//         } catch (Exception) {
//             return BadRequest();
//         }
//     }
}