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
            list1= _context.Issues.Include(p=>p.Project).Include(p=>p.Assginee).Include(p=>p.Reporter).ToList();
            // list = _context.Set < Issue > ().ToList();
            Console.WriteLine(list1);
        } catch (Exception) {
            throw;
        }
        return list1;
    }

 
    public ResponseModel SaveIssue(int id,int reporter_id,Issue issueModel)
    {
         ResponseModel model = new ResponseModel();

      Issue _issue=new Issue();
      _issue.Description=issueModel.Description;
      _issue.Title=issueModel.Title;
      _issue.Type=issueModel.Type;
      _issue.Status=issueModel.Status;
      _issue.Created_At=DateTime.Now;
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
        // var assignee_obj =list1.FirstOrDefault(x => x.EmployeeId==assignee_id); 
          var reporter_obj =list1.FirstOrDefault(x => x.EmployeeId==reporter_id);  
        // Console.WriteLine(obj);
        //  _issue.Assginee=assignee_obj;
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


public void UpdateIssue(int id,Issue updatedIssue)
{
    // Console.WriteLine("--------updatedIssue 1 service");
    var issueToUpdate = _context.Find < Issue > (id);
    // Console.WriteLine(issueToUpdate+"--------updatedIssue service"+issueToUpdate.Description+updatedIssue.Description);
    if(issueToUpdate!=null)
    {
        issueToUpdate.Title = updatedIssue.Title;
        issueToUpdate.Description = updatedIssue.Description;
        issueToUpdate.Status = updatedIssue.Status;
        issueToUpdate.Type = updatedIssue.Type;
        issueToUpdate.Updated_At=DateTime.Now;
        _context.Update < Issue > (issueToUpdate);
         _context.SaveChanges();
    }
}
public List<Issue> GetIssuesByProject(int id)
    {
        Issue? issue=new Issue();
          IEnumerable<Issue> list=new List<Issue>();
          

        list=_context.Issues.Include(p=>p.Project).Include(p=>p.Assginee).Include(p=>p.Reporter).Where(p=>p.Project.Id==id);
         Console.WriteLine("---------"+list.ToList());
        return list.ToList();
    }

    public List<Issue> GetDetailsOfIssuesInProject(int project_id,int issue_id)
    {
        IEnumerable<Issue> list;
        // IEnumerable<Issue> list=new List<Issue>();
          

        list= _context.Issues.Include(p=>p.Project).Include(p=>p.Assginee).Include(p=>p.Reporter).Where(p=>p.Project.Id==project_id && p.Id==issue_id);
        //  Console.WriteLine("---------"+list.ToList());
        return list.ToList();
    }


     public bool DeleteIssueUnderAProject(int projectId,int issueId)
    {
        var issue =_context.Issues.Include(p=>p.Project).Include(p=>p.Assginee).Include(p=>p.Reporter).FirstOrDefault(p=>p.Project.Id==projectId && p.Id==issueId);

        if (issue == null)
        {
            return false;
        }

        _context.Issues.Remove(issue);
        _context.SaveChanges();

        return true;
    }

    public void UpdateIssueUnderAProject(int project_id, int issue_id, Issue updatedIssue)
    {
    var issueToUpdate =_context.Issues.Include(p=>p.Project).Include(p=>p.Assginee).Include(p=>p.Reporter).FirstOrDefault(p=>p.Project.Id==project_id && p.Id==issue_id);
    // Console.WriteLine(issueToUpdate+"--------updatedIssue service"+issueToUpdate.Description+updatedIssue.Description);
    if(issueToUpdate!=null)
    {
        issueToUpdate.Title = updatedIssue.Title;
        issueToUpdate.Description = updatedIssue.Description;
        issueToUpdate.Status = updatedIssue.Status;
        issueToUpdate.Type = updatedIssue.Type;
        _context.Update < Issue > (issueToUpdate);
         _context.SaveChanges();
    }
    }

    public void AssigneIssueToUser(int issue_id,int user_id)
    {
         var issueToUpdate =_context.Issues.Include(p=>p.Project).Include(p=>p.Assginee).Include(p=>p.Reporter).FirstOrDefault(p=>p.Id==issue_id);
         
       List<Employee> list1 = new List<Employee>();  
         list1 = _context.Set < Employee > ().ToList(); 
       
        var assignee_obj =list1.FirstOrDefault(x => x.EmployeeId==user_id);  
        issueToUpdate.Assginee=assignee_obj;
           _context.Update < Issue > (issueToUpdate);
         _context.SaveChanges();
    }

     public void UpdateStatusOfIssue(int issue_id,Issue _status)
    {
        var issueToUpdate =_context.Issues.Include(p=>p.Project).Include(p=>p.Assginee).Include(p=>p.Reporter).FirstOrDefault(p=>p.Id==issue_id);
        issueToUpdate.Status=_status.Status;
          _context.Update < Issue > (issueToUpdate);
         _context.SaveChanges();
    }

    public List<Issue> SearchOnTitleAndDescription(string _title,string _description)
    {
          List<Issue> matchingIssues = _context.Issues.Include(p=>p.Project).Include(p=>p.Assginee).Include(p=>p.Reporter)
        .Where(issue => issue.Title.Contains(_title) && issue.Description.Contains(_description))
        .ToList();
        return matchingIssues;
    }
       
       
        //--------------------------  Search Query Language for Issues
       public List<Issue>  SerachQueryProjectIDAssigneeEmailOR(int project,string email)
       {
            List<Issue> list=new List<Issue>();
            Console.Write("--------"+project+" "+ email);
             list=_context.Issues.Include(p=>p.Project).Include(p=>p.Assginee).Include(p=>p.Reporter).Where(p=>p.Project.Id==project || p.Assginee.Email.Contains(email)).ToList();
        //    Console.Write("--------"+li);
            return list;
       }

         public List<Issue>  SerachQueryProjectIDAssigneeEmailAND(int project,string email)
       {
            List<Issue> list=new List<Issue>();
            Console.Write("--------"+project+" "+ email);
             list=_context.Issues.Include(p=>p.Project).Include(p=>p.Assginee).Include(p=>p.Reporter).Where(p=>p.Project.Id==project && p.Assginee.Email.Contains(email)).ToList();
        //    Console.Write("--------"+li);
            return list;
       }


    public List<Issue> SerachByType(Models.Issue.IssueType type)
    {
            List<Issue> list=new List<Issue>();
            try{
                list=_context.Issues.Include(p=>p.Project).Include(p=>p.Assginee).Include(p=>p.Reporter).Where(p=>p.Type==type).ToList();
                Console.Write("--------"+list);
            }
              catch(Exception e)
              {
                    Console.Write("--------"+e);
              }
            return list;
    }


     public List<Issue> SerachByNotAGivenType(Models.Issue.IssueType type)
    {
            List<Issue> list=new List<Issue>();
            try{
                list=_context.Issues.Include(p=>p.Project).Include(p=>p.Assginee).Include(p=>p.Reporter).Where(p=>p.Type!=type).ToList();
                Console.Write("--------"+list);
            }
              catch(Exception e)
              {
                    Console.Write("--------"+e);
              }
            return list;
    }

        public List<Issue> SearchByCreatedDate(DateTime date)
    {
            List<Issue> list=new List<Issue>();
            try{
                list=_context.Issues.Include(p=>p.Project).Include(p=>p.Assginee).Include(p=>p.Reporter).Where(p=>p.Created_At>=date).ToList();
                Console.Write("--------"+list);
            }
              catch(Exception e)
              {
                    Console.Write("--------"+e);
              }
            return list;
    }

     public List<Issue> SearchByUpdatedDate(DateTime date)
    {
            List<Issue> list=new List<Issue>();
            try{
                list=_context.Issues.Include(p=>p.Project).Include(p=>p.Assginee).Include(p=>p.Reporter).Where(p=>p.Created_At<=date).ToList();
                Console.Write("--------"+list);
            }
              catch(Exception e)
              {
                    Console.Write("--------"+e);
              }
            return list;
    }

   
            
            
         







}
