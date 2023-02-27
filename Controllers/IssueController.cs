using TaskManagerApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TaskManagerApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)] 
public class IssueController:ControllerBase{
    IIssueService _issueService;
    private readonly ILogger<Issue> _logger;

    public IssueController(IIssueService service,ILogger<Issue> logger) {
        _issueService = service;
        _logger=logger;
    }

    [HttpGet("issue")]
    [Authorize(Roles="admin,manager")]
    public IActionResult GetAllIssues() {
        try {
            var employees = _issueService.GetIssueList();
            _logger.LogInformation("Able to get Issue list");
            if (employees == null){
                 _logger.LogInformation("Issue is Empty Able to get Issue list");
                 return NotFound();
            } 
            return Ok(employees);
        } catch (Exception) {
             _logger.LogError("Error  to get Issue list");
            return BadRequest();
        }
    }

      [HttpPost("SaveIssue/{id}")]
    [Authorize(Roles="admin,manager")]
    public IActionResult SaveIssue(int id,[FromQuery] int Reporter_id,[FromBody] Issue issueModel) {
        try {
           
            var model = _issueService.SaveIssue(id,Reporter_id,issueModel);
             _logger.LogInformation("Able to save Issue list");
            return Ok(model);
        } catch (Exception) {
             _logger.LogError("Not Able to get Issue list");
            return BadRequest();
        }
    }

     [HttpGet]
    [Route("[action]/id")]
    [Authorize(Roles="admin,manager,user")]
    public IActionResult GetIssueById(int id) {
        try {
            var issues = _issueService.GetIssueDetailsById(id);
             _logger.LogInformation("Able to get Issue by id");
            if (issues == null) 
            {
                 _logger.LogError(" Issue Not found by id");
                  return NotFound();
            }
           
            return Ok(issues);
        } catch (Exception) {
           _logger.LogError(" Service not working");
            return BadRequest();
        }
    }

      [HttpDelete]
   [Authorize(Roles="admin,manager")]
    //  [Authorize(Roles="admin")]
    public IActionResult DeleteIssue(int id) {
        try {
            var model = _issueService.DeleteIssue(id);
            _logger.LogInformation(" Delete successfull");
            return Ok(model);
        } catch (Exception) {
            _logger.LogError(" Issue Not found by id for delete");
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles="admin,manager")]
public IActionResult UpdateIssue(int id, [FromBody] Issue updatedIssue)
{
    if (updatedIssue == null)
    {
        _logger.LogError(" Issue Not found by id");
        return BadRequest();

    }
    try
    { 
            _issueService.UpdateIssue(id,updatedIssue);
            _logger.LogError(" Issue update successfull");
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!IssueExists(id))
        {
             _logger.LogError(" Issue Service error");
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
    [Authorize(Roles="admin,manager")]
    public IActionResult GetIssuesByProject(int id) {
        try {
            var issuesbyproject = _issueService.GetIssuesByProject( id);
             _logger.LogInformation(" Issue by project");
            if (issuesbyproject == null) 
            {
                _logger.LogError("issue not found");
                return NotFound();
            }
            return Ok(issuesbyproject);
        } catch (Exception) {
             _logger.LogError("issue service error");
            return BadRequest();
        }
    }


    [HttpGet]
    [Route("[action]/project_id/issue_id")]
    [Authorize(Roles="admin,manager,user")]
    public IActionResult GetDetailsOfIssuesInProject(int project_id,int issue_id) {
        try {
            var issuesbyproject = _issueService.GetDetailsOfIssuesInProject(project_id,issue_id);
             _logger.LogInformation(" details of Issue by project");
            if (issuesbyproject == null)
            {
                 _logger.LogError("issue not found");
                 return NotFound();
            } 
            return Ok(issuesbyproject);
        } catch (Exception) {
              _logger.LogError("issue service error");
            return BadRequest();
        }
    }

       [HttpDelete]
    [Route("[action]/project_id/issue_id")]
    [Authorize(Roles="admin,manager")]
    public IActionResult DeleteIssueUnderAProject(int projectid,int issue_id) {
        try {
            var model = _issueService.DeleteIssueUnderAProject(projectid,issue_id);
               _logger.LogInformation(" delete  Issue under a project");
            return Ok(model);
        } catch (Exception) {
              _logger.LogError("issue service error");
            return BadRequest();
        }
    }

     [HttpPut]
     [Route("[action]/project_id/issue_id")]
     [Authorize(Roles="admin,manager")]
public IActionResult UpdateIssueUnderAProject(int project_id,int issue_id, [FromBody] Issue updatedIssue)
{
    if (updatedIssue == null)
    {
        return BadRequest();
    }
    try
    { 
            _issueService.UpdateIssueUnderAProject(project_id,issue_id,updatedIssue);
              _logger.LogInformation("update issue under a project");
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!IssueExists(project_id))
        {
              _logger.LogError("issue service error");
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
[Authorize(Roles="admin,manager")]
  public IActionResult AssigneIssueToUser(int issue_id,int user_id)
  {
 try {
            _issueService.AssigneIssueToUser(issue_id,user_id);
            _logger.LogInformation("assign issue to user");
            return Ok();
        } catch (Exception) {
              _logger.LogError("issue service error");
            return BadRequest();
        }
  }


[HttpPut]
[Route("[action]/issue_id")]
[Authorize(Roles="admin,manager,user")]
  public IActionResult UpdateStatusOfIssue(int issue_id,[FromBody] Issue status)
  {
 try {
            _issueService.UpdateStatusOfIssue(issue_id,status);
              _logger.LogInformation("update status of issue");
            return Ok();
        } catch (Exception) {
              _logger.LogError("Issue Service Error");
            return BadRequest();
        }
  }

   [HttpGet]
    [Route("[action]")]
    [Authorize(Roles="admin,manager,user")]
   public IActionResult SearchOnTitleAndDescription([FromQuery] string _title,[FromQuery] string _description)
   {
    try{
      var issue= _issueService.SearchOnTitleAndDescription(_title,_description);
         _logger.LogInformation(" Search On Title And Description");
       return Ok(issue);
    }
    catch(Exception){
          _logger.LogError("Issue Service Error");
        return BadRequest();
    }
   }


 [HttpGet]
[Route("[action]")]
[Authorize(Roles="admin,manager")]
   public IActionResult SerachQueryProject_OR_IDAssigneeEmail([FromQuery] int projectId,[FromQuery] string Email)
   {
    try{
      var issue= _issueService.SerachQueryProjectIDAssigneeEmailOR(projectId,Email);
        _logger.LogInformation(" Search On Title OR Description");
       return Ok(issue);
    }
    catch(Exception){
         _logger.LogError("Issue Service Error");
        return BadRequest();
    }
   }

   [HttpGet]
    [Route("[action]")]
    [Authorize(Roles="admin,manager")]
   public IActionResult SerachQueryProject_AND_IDAssigneeEmail([FromQuery] int projectId,[FromQuery] string Email)
   {
    try{
      var issue= _issueService.SerachQueryProjectIDAssigneeEmailAND(projectId,Email);
       _logger.LogInformation(" Serach Query Project_AND_ID AssigneeEmail");
       return Ok(issue);
    }
    catch(Exception){
         _logger.LogError("Issue Service Error");
        return BadRequest();
    }
   }


 
    [HttpGet]
    [Route("[action]")]
    [Authorize(Roles="admin,manager")]
      public IActionResult  SerachByType([FromQuery] Models.Issue.IssueType type)
      { try{
      var issue= _issueService.SerachByType(type);
       _logger.LogInformation(" Serach By Type");
       return Ok(issue);
    }
    catch(Exception){
          _logger.LogError("Issue Service Error");
        
        return BadRequest();
    }

      }


    [HttpGet]
    [Route("[action]")]
    [Authorize(Roles="admin,manager")]
    public IActionResult  SerachByNotAGivenType([FromQuery] Models.Issue.IssueType type)
      { try{

      var issue= _issueService.SerachByNotAGivenType(type);
        _logger.LogInformation(" Serach By Not A Given Type");
       return Ok(issue);
    }
    catch(Exception){
        _logger.LogError("Issue Service Error");
        return BadRequest();
    }

      }

    [HttpGet]
    [Route("[action]")]
    [Authorize(Roles="admin,manager")]
    public ActionResult SearchByCreatedDate([FromQuery] DateTime date)
       {
        try{
      var issue= _issueService.SearchByCreatedDate(date);
       _logger.LogInformation("  Search By Created Date");
       return Ok(issue);
    }
    catch(Exception){
         _logger.LogError("Issue Service Error");
        return BadRequest();
    }
       }

    [HttpGet]
    [Route("[action]")]
    [Authorize(Roles="admin,manager")]
       public ActionResult SearchByUpdatedDate([FromQuery] DateTime date)
       {
        try{
      var issue= _issueService.SearchByUpdatedDate(date);
         _logger.LogInformation(" Search By Updated Date");
       return Ok(issue);
    }
    catch(Exception){
           _logger.LogError("Issue Service Error");
        return BadRequest();
    }
       }


}