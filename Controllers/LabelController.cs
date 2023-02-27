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
public class LabelController:ControllerBase{
    ILabelService _labelService;
    private readonly ILogger<Label> _logger;
    public LabelController(ILabelService service,ILogger<Label> logger) {
        _labelService = service;
        _logger=logger;
    }



     [HttpDelete("deletelabel/{label_id}/issue/{issue_id}")]
    
     [Authorize(Roles="admin,manager,user")]
    public IActionResult DeleteLabelFromIssue(int label_id,int issue_id) {
        try {
            var model = _labelService.DeleteLabelFromIssue(label_id,issue_id);
               _logger.LogInformation("Delete Label from issue");
            return Ok(model);
        } catch (Exception) {
              _logger.LogError("Error -: Not able to Delete Label from issue");
            return BadRequest();
        }
    }


    [HttpPost]
    [Route("[action]")]
     [Authorize(Roles="admin")]
    public IActionResult SaveLabel(Label labelModel) {
        try {
            var model = _labelService.SaveLabel(labelModel);
              _logger.LogInformation("Save Label ");
            return Ok(model);
        } catch (Exception) {
              _logger.LogError("Not able to save");
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("[action]")]
     [Authorize(Roles="admin")]
    public IActionResult DeleteLabel(int id) {
        try {
            var model = _labelService.DeleteLabel(id);
              _logger.LogInformation("Delete Label from issue by id");
            return Ok(model);
        } catch (Exception) {
              _logger.LogError("Not able to Delete Label from issue by id");
            return BadRequest();
        }
    }
     [HttpGet]
    [Authorize(Roles="admin")]
    public IActionResult GetAllLabels() {
        try {
            var employees = _labelService.GetLabelList();
              _logger.LogInformation("Get all Label ");
            if (employees == null) return NotFound();
            return Ok(employees);
        } catch (Exception) {
             _logger.LogError("Get all Label ");
            return BadRequest();
        
        }
    }

    [HttpPut("{id}")]
     [Authorize(Roles="admin")]
    public IActionResult UpdateLabel(int id, [FromBody] Label updatedLabel)
      {
    if (updatedLabel == null)
    {
         _logger.LogError("Update Label Not Happen ");
    return BadRequest();
    }
    try
    { 
    _labelService.UpdateLabel(id,updatedLabel);
     _logger.LogInformation("Update By id Label ");
    return Ok();
    }
    catch (DbUpdateConcurrencyException)
    {
          _logger.LogError("Update Label Not Happen  some error");
    return NotFound();
    }
     }

    [HttpPut("Addlabel/{label_id}/to/issue/{issue_id}")]
    [Authorize(Roles="admin,manager,user")]
    public IActionResult AddLabelToIssue(int label_id,int issue_id)
      {
    try
    { 
    var model=  _labelService.AddLabelToIssue(label_id,issue_id);
      _logger.LogInformation("Add label to issue ");
    return Ok(model);
    }
    catch (DbUpdateConcurrencyException)
    {
         _logger.LogError("Not able to Add label to issue ");
    return NotFound();
    }
     }


}