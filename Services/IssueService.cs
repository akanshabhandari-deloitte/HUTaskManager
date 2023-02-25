using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services;
public class IssueService:IIssueService
{
    private TaskManagerContext _context;
    public IssueService(TaskManagerContext context) {
        _context = context;
    }

    public List<Issue> GetIssueList()
    {
          List < Issue > list1;
        try {
            list1= _context.Issues.Include(s=>s.Project).ToList();
            // list = _context.Set < Issue > ().ToList();
            Console.WriteLine(list1);
        } catch (Exception) {
            throw;
        }
        return list1;
    }

 
    public ResponseModel SaveIssue(int id,int reporter_id,int assignee_id,Issue issueModel)
    {
         ResponseModel model = new ResponseModel();
        //   Console.Write("------------------------"+issueModel.Type);
        //         Console.Write("hi"+"-----"+id+"------"+issueModel.Description);
                 Console.Write("--assigne----------------------"+assignee_id);

      Issue _issue=new Issue();
      _issue.Description=issueModel.Description;
      _issue.Title=issueModel.Title;
      _issue.Type=issueModel.Type;
      _issue.Status=issueModel.Status;
         

         


               List<Project> list = new List<Project>();  
         list = _context.Set < Project > ().ToList(); 
        //   Console.WriteLine("hibye");
        //  Console.WriteLine(list);
        var obj =list.FirstOrDefault(x => x.Id==id);  
        // Console.WriteLine(obj);
         _issue.Project=obj;



               List<Employee> list1 = new List<Employee>();  
         list1 = _context.Set < Employee > ().ToList(); 
        //   Console.WriteLine("hibye");
        //  Console.WriteLine(list);
        var assignee_obj =list1.FirstOrDefault(x => x.EmployeeId==assignee_id); 
          var reporter_obj =list1.FirstOrDefault(x => x.EmployeeId==reporter_id);  
        // Console.WriteLine(obj);
         _issue.Assginee=assignee_obj;
         _issue.Reporter=reporter_obj;



                _context.Add < Issue > (_issue);
                Console.Write("------------------------"+issueModel.Type);
                model.Messsage = "issue Inserted Successfully";
           // }
            _context.SaveChanges();
            model.IsSuccess = true;
        
        return model;
    }
 public Issue GetIssueDetailsById(int issueId)
    {
        Issue? _issue;
        try {
            // _issue = _context.Find < Issue > (issueId);
             _issue = _context.Issues.Include(s=>s.Assginee).
             Include(s=>s.Reporter).
             Include(s=>s.Project)
             .FirstOrDefault(p => p.Id == issueId);
        } catch (Exception) {
            throw;
        }
        return _issue;
    }

     public bool DeleteIssue(int issueId)
    {
        var issue = _context.Issues.Find(issueId);

        if (issue == null)
        {
            return false;
        }

        _context.Issues.Remove(issue);
        _context.SaveChanges();

        return true;
    }


public void UpdateIssue(int id,Issue updatedIssue,int assignee_id)
{
    var issueToUpdate = _context.Find < Issue > (id);
    Console.WriteLine(issueToUpdate+"--------updatedIssue service"+issueToUpdate.Description+updatedIssue.Description);
    if(issueToUpdate!=null && assignee_id==0)
    {
        issueToUpdate.Title = updatedIssue.Title;
        issueToUpdate.Description = updatedIssue.Description;
        issueToUpdate.Status = updatedIssue.Status;
        issueToUpdate.Type = updatedIssue.Type;
        // issueToUpdate.Assginee= updatedIssue.Assginee;
         Console.WriteLine("--------updatedIssue service1---- ");
        _context.Update < Issue > (issueToUpdate);
         _context.SaveChanges();
          Console.WriteLine("--------updatedIssue service1---- ");
    }
     else 
    {
        issueToUpdate.Title = updatedIssue.Title;
        issueToUpdate.Description = updatedIssue.Description;
        issueToUpdate.Status = updatedIssue.Status;
        issueToUpdate.Type = updatedIssue.Type;
         Console.WriteLine("--------updatedIssue service2");
     
        List<Employee> list1 = _context.Set < Employee > ().ToList(); 
        var assignee_obj =list1.FirstOrDefault(x => x.EmployeeId==assignee_id); 
         issueToUpdate.Assginee=assignee_obj;
         Console.WriteLine("-------------update");
        _context.Update < Issue > (issueToUpdate);
         _context.SaveChanges();
          Console.WriteLine("-------------1111111111111111update");
    }
     Console.WriteLine("--------updatedIssue service out");
}

}
