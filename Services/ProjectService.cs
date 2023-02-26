using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services;
public class ProjectService:IProjectService
{
    private TaskManagerContext _context;
    public ProjectService(TaskManagerContext context) {
        _context = context;
    }

    public List<Project> GetProjectList()
    {
          List < Project > projectList;
        try {
            projectList = _context.Projects.Include(s=>s.Creator).ToList();
             List<Employee> list = new List<Employee>();  
         list = _context.Set < Employee > ().ToList(); 
          Console.WriteLine("hibye");
         Console.WriteLine(list);
        } catch (Exception) {
            throw;
        }
        return projectList;
    }

    public ResponseModel SaveProject(int id,Project projectModel)
    {
        ResponseModel model = new ResponseModel();
        Project p=new Project();
         Console.WriteLine("save");
         List<Employee> list = new List<Employee>();  
         list = _context.Set < Employee > ().ToList(); 
          Console.WriteLine("hibye");
         Console.WriteLine(list);
        var obj =list.FirstOrDefault(x => x.EmployeeId==id);  
        
       if (obj != null) {
            p.Description=projectModel.Description;
            p.Creator=obj;
            }

            _context.Add < Project > (p);
                model.Messsage = "Employee Inserted Successfully";
            _context.SaveChanges();
            model.IsSuccess = true;
        
        return model;
    }


      public Project GetProjectDetailsById(int projectId)
    {
        Project? project;
        try {
          project=  _context.Projects.Include(s=>s.Creator).FirstOrDefault(p => p.Id == projectId);
            // project = _context.Find < Project > (projectId);
        } catch (Exception) {
            throw;
        }
        return project;
    }



public void UpdateProject(int id ,Project updatedProject)
{
    var projectToUpdate = _context.Find < Project > (id);
    // Console.WriteLine(issueToUpdate+"--------updatedIssue service"+issueToUpdate.Description+updatedIssue.Description);
    if(projectToUpdate!=null)
    {
        projectToUpdate.Description= updatedProject.Description;
      
        _context.Update < Project > (projectToUpdate);
         _context.SaveChanges();
    }
}

    public ResponseModel DeleteProject(int projectId)
    {
         ResponseModel model = new ResponseModel();
        try {
            Project _temp = GetProjectDetailsById(projectId);
            if (_temp != null) {
                _context.Remove < Project > (_temp);
                _context.SaveChanges();
                model.IsSuccess = true;
                model.Messsage = "Project Deleted Successfully";
            } else {
                model.IsSuccess = false;
                model.Messsage = "Project Not Found";
            }
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    

}