using TaskManagerApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TaskManagerApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using log4net;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)] 
public class ProjectController:ControllerBase{
    IProjectService _projectService;
    private TaskManagerContext _db;
    
    private readonly ILogger<Project> _logger;

    
    public ProjectController(IProjectService service,TaskManagerContext db,ILogger<Project> logger) {
        _projectService = service;
        _db=db;
        _logger=logger;

    }


        /// <summary>
    /// get all employess
    /// </summary>
    /// <returns></returns>
    [HttpGet("project")]
    [Authorize(Roles="admin")]
    public IActionResult GetAllProject() {
        try {
            var employees = _projectService.GetProjectList();
            _logger.LogInformation("Get All Project List");
            if (employees == null) return NotFound();

            return Ok(employees);
        } catch (Exception) {
              _logger.LogError("Some Error List");
            return BadRequest();
        }
    }
    [HttpPost("SaveProject/{id}")]
     [Authorize(Roles="admin")]
    public IActionResult SaveProject(int id,[FromBody] Project project) {  
        try {
        var model = _projectService.SaveProject(id,project); 
          _logger.LogInformation("Save All Project List");
        return Ok(model);
        } catch (Exception) {
               _logger.LogError("Some Error List");
            return BadRequest();
        }
    }


     [HttpGet]
    [Route("[action]/id")]
      [Authorize(Roles="admin")]
    public IActionResult GetProjectsById(int id) {
        try {
            var employees = _projectService.GetProjectDetailsById(id);
              _logger.LogInformation("Get  Project By Id");
            if (employees == null)
            {
                 _logger.LogInformation("Project Not Found");
                 return NotFound();
            } 
            return Ok(employees);
        } catch (Exception) {
             _logger.LogError("DeleteProject By Id");
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles="admin")]
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
     [Authorize(Roles="admin")]
    public IActionResult DeleteProject(int id) {
        try {

            var model = _projectService.DeleteProject(id);
             _logger.LogInformation("Delete Project By ID");
            return Ok(model);
        } catch (Exception) {
             _logger.LogError("Delete Project By Id");
            return BadRequest();
        }
    }
}