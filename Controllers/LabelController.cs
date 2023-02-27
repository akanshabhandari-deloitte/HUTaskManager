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
    public LabelController(ILabelService service) {
        _labelService = service;
    }



     [HttpDelete("deletelabel/{label_id}/issue/{issue_id}")]
    
     [Authorize(Roles="admin,manager,user")]
    public IActionResult DeleteLabelFromIssue(int label_id,int issue_id) {
        try {
            var model = _labelService.DeleteLabelFromIssue(label_id,issue_id);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }


    [HttpPost]
    [Route("[action]")]
     [Authorize(Roles="admin")]
    public IActionResult SaveLabel(Label labelModel) {
        try {
            var model = _labelService.SaveLabel(labelModel);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("[action]")]
     [Authorize(Roles="admin")]
    public IActionResult DeleteLabel(int id) {
        try {
            var model = _labelService.DeleteLabel(id);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
     [HttpGet]
    [Authorize(Roles="admin")]
    public IActionResult GetAllLabels() {
        try {
            var employees = _labelService.GetLabelList();
            if (employees == null) return NotFound();
            return Ok(employees);
        } catch (Exception) {
            return BadRequest();
        
        }
    }

    [HttpPut("{id}")]
     [Authorize(Roles="admin")]
    public IActionResult UpdateLabel(int id, [FromBody] Label updatedLabel)
      {
    if (updatedLabel == null)
    {
    return BadRequest();
    }
    try
    { 
    _labelService.UpdateLabel(id,updatedLabel);
    return Ok();
    }
    catch (DbUpdateConcurrencyException)
    {
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
    return Ok(model);
    }
    catch (DbUpdateConcurrencyException)
    {
    return NotFound();
    }
     }


}