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
    public IActionResult SaveIssue(int id,[FromQuery] int Reporter_id,[FromBody] Issue issueModel) {
        try {
            // Console.WriteLine("assignee-------------"+Assignee_id);
            var model = _issueService.SaveIssue(id,Reporter_id,issueModel);
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
public IActionResult UpdateIssue(int id, [FromBody] Issue updatedIssue)
{
    if (updatedIssue == null)
    {
        return BadRequest();
    }
    try
    { 
            _issueService.UpdateIssue(id,updatedIssue);
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


    [HttpGet]
    [Route("[action]/id")]
    public IActionResult GetIssuesByProject(int id) {
        try {
            var issuesbyproject = _issueService.GetIssuesByProject( id);
            if (issuesbyproject == null) return NotFound();
            return Ok(issuesbyproject);
        } catch (Exception) {
            return BadRequest();
        }
    }


    [HttpGet]
    [Route("[action]/project_id/issue_id")]
    public IActionResult GetDetailsOfIssuesInProject(int project_id,int issue_id) {
        try {
            var issuesbyproject = _issueService.GetDetailsOfIssuesInProject(project_id,issue_id);
            if (issuesbyproject == null) return NotFound();
            return Ok(issuesbyproject);
        } catch (Exception) {
            return BadRequest();
        }
    }

       [HttpDelete]
    [Route("[action]/project_id/issue_id")]
    //  [Authorize(Roles="admin")]
    public IActionResult DeleteIssueUnderAProject(int projectid,int issue_id) {
        try {
            var model = _issueService.DeleteIssueUnderAProject(projectid,issue_id);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

     [HttpPut]
     [Route("[action]/project_id/issue_id")]
public IActionResult UpdateIssueUnderAProject(int project_id,int issue_id, [FromBody] Issue updatedIssue)
{
    if (updatedIssue == null)
    {
        return BadRequest();
    }
    try
    { 
            _issueService.UpdateIssueUnderAProject(project_id,issue_id,updatedIssue);
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!IssueExists(project_id))
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


[HttpPut]
[Route("[action]/user_id/issue_id")]
  public IActionResult AssigneIssueToUser(int issue_id,int user_id)
  {
 try {
            _issueService.AssigneIssueToUser(issue_id,user_id);
            return Ok();
        } catch (Exception) {
            return BadRequest();
        }
  }


[HttpPut]
[Route("[action]/issue_id")]
  public IActionResult UpdateStatusOfIssue(int issue_id,[FromBody] Issue status)
  {
 try {
            _issueService.UpdateStatusOfIssue(issue_id,status);
            return Ok();
        } catch (Exception) {
            return BadRequest();
        }
  }

   [HttpGet]
    [Route("[action]")]
   public IActionResult SearchOnTitleAndDescription([FromQuery] string _title,[FromQuery] string _description)
   {
    try{
      var issue= _issueService.SearchOnTitleAndDescription(_title,_description);
       return Ok(issue);
    }
    catch(Exception){
        return BadRequest();
    }
   }


 [HttpGet]
    [Route("[action]")]
   public IActionResult SerachQueryProject_OR_IDAssigneeEmail([FromQuery] int projectId,[FromQuery] string Email)
   {
    try{
      var issue= _issueService.SerachQueryProjectIDAssigneeEmailOR(projectId,Email);
       return Ok(issue);
    }
    catch(Exception){
        return BadRequest();
    }
   }

   [HttpGet]
    [Route("[action]")]
   public IActionResult SerachQueryProject_AND_IDAssigneeEmail([FromQuery] int projectId,[FromQuery] string Email)
   {
    try{
      var issue= _issueService.SerachQueryProjectIDAssigneeEmailAND(projectId,Email);
       return Ok(issue);
    }
    catch(Exception){
        return BadRequest();
    }
   }


 
    [HttpGet]
    [Route("[action]")]
      public IActionResult  SerachByType([FromQuery] Models.Issue.IssueType type)
      { try{
      var issue= _issueService.SerachByType(type);
       return Ok(issue);
    }
    catch(Exception){
        return BadRequest();
    }

      }


      [HttpGet]
    [Route("[action]")]
      public IActionResult  SerachByNotAGivenType([FromQuery] Models.Issue.IssueType type)
      { try{
      var issue= _issueService.SerachByNotAGivenType(type);
       return Ok(issue);
    }
    catch(Exception){
        return BadRequest();
    }

      }

     [HttpGet]
    [Route("[action]")]
       public ActionResult SearchByCreatedDate([FromQuery] DateTime date)
       {
        try{
      var issue= _issueService.SearchByCreatedDate(date);
       return Ok(issue);
    }
    catch(Exception){
        return BadRequest();
    }
       }

   [HttpGet]
    [Route("[action]")]
       public ActionResult SearchByUpdatedDate([FromQuery] DateTime date)
       {
        try{
      var issue= _issueService.SearchByUpdatedDate(date);
       return Ok(issue);
    }
    catch(Exception){
        return BadRequest();
    }
       }


}