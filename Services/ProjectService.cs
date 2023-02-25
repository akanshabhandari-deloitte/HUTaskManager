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
}