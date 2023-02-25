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
public class IssueController:ControllerBase{
    IIssueService _issueService;
    public IssueController(IIssueService service) {
        _issueService = service;
    }

        /// <summary>
    /// get all employess
    /// </summary>
    /// <returns></returns>
    [HttpGet("issue")]
    // [Authorize(Roles="admin")]
    public IActionResult GetAllIssues() {
        try {
            var employees = _issueService.GetIssueList();
            if (employees == null) return NotFound();
            return Ok(employees);
        } catch (Exception) {
            return BadRequest();
        }
    }

      [HttpPost("SaveIssue/{id}")]
    //  [Authorize(Roles="admin")]
    public IActionResult SaveIssue(int id,[FromQuery] int Reporter_id,[FromQuery] int Assignee_id,[FromBody] Issue issueModel) {
        try {
            // Console.WriteLine("assignee-------------"+Assignee_id);
            var model = _issueService.SaveIssue(id,Reporter_id,Assignee_id,issueModel);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

     [HttpGet]
    [Route("[action]/id")]
    //   [Authorize(Roles="user")]
    public IActionResult GetIssueById(int id) {
        try {
            var issues = _issueService.GetIssueDetailsById(id);
            if (issues == null) return NotFound();
            return Ok(issues);
        } catch (Exception) {
            return BadRequest();
        }
    }

      [HttpDelete]
    [Route("[action]")]
    //  [Authorize(Roles="admin")]
    public IActionResult DeleteIssue(int id) {
        try {
            var model = _issueService.DeleteIssue(id);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
public IActionResult UpdateIssue(int id, [FromBody] Issue updatedIssue,[FromQuery] int assignee_id)
{
    if (updatedIssue == null)
    {
        return BadRequest();
    }
    
    try
    { Console.WriteLine("-------------update1");
            _issueService.UpdateIssue(id,updatedIssue,assignee_id);
             Console.WriteLine("-------------update2");
        
       
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

    return NoContent();
}

    private bool IssueExists(int id)
    {
        throw new NotImplementedException();
    }
}