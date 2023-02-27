using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services;
public class LabelService:ILabelService
{
    private TaskManagerContext _context;
    public LabelService(TaskManagerContext context) {
        _context = context;
    }

    public ResponseModel AddLabelToIssue(int label_id, int issue_id)
    {
         ResponseModel model = new ResponseModel();
         List<Label> l=new List<Label>();

      
         l = _context.Set < Label > ().ToList(); 
        
          Console.WriteLine(l+"hibye");
        foreach (var item in l)
         {
             Console.WriteLine(item.Id);
         }
        var label_obj =l.FirstOrDefault(x => x.Id==label_id);  

       List<Issue> _issue=new List<Issue>();
        // Label _label =_context.Labels.FirstOrDefault(p=>p.Id==label_id);
        Issue  li = _context.Find < Issue > (issue_id);
          _issue.Add(li);
        Console.WriteLine(label_id+"-------"+issue_id+"--------"+label_obj+" "+_issue);
          if (label_obj!=null && _issue!=null)
          {
            label_obj.issue=_issue;
            _context.SaveChanges();
            model.IsSuccess = true;
            model.Messsage = "Label Added to Issue Successfully";
            return model;
          }
          else
          {
            model.IsSuccess = false;
            model.Messsage = "Label Not Addedd Found";
           return model;
          }    
    }

    public ResponseModel DeleteLabel(int labelId)
    {
         ResponseModel model = new ResponseModel();
        var label = _context.Labels.Find(labelId);

        if (label == null)
        {
            model.IsSuccess=false;
            model.Messsage="No Label";
            return model;

        }
        else{

        _context.Labels.Remove(label);
        _context.SaveChanges();
          model.IsSuccess=true;
            model.Messsage="Successfully Deleted Label";
            return model;
        }
    }

    public string DeleteLabelFromIssue(int label_id,int issue_id)
    {
        try{
        Console.WriteLine("Delte label--------");
        ResponseModel model = new ResponseModel();
        Issue issue_obj=_context.Find < Issue > (issue_id);
        var l1=_context.Labels.Include(s=>s.issue).Where(p=>p.Id==label_id && p.issue.Contains(issue_obj)).ToList();
        l1[0].issue=null;
        _context.SaveChanges();
        return "ok";
        }
        catch(Exception e)
        {
            return("not ok"+e);
        }
    }

    public List<Label> GetLabelList()
    {
      List < Label > labelList;
        try {
            labelList = _context.Labels.Include(s=>s.issue).ToList();
            Console.WriteLine(labelList);
        } catch (Exception) {
            throw;
        }
        return labelList;
    }

    public ResponseModel SaveLabel(Label labelModel)
    {
       ResponseModel model = new ResponseModel();
    
                _context.Add < Label > (labelModel);
                model.Messsage = "Label Added Successfully";
           // }
            _context.SaveChanges();
            model.IsSuccess = true;
        
        return model;
    }

    public void UpdateLabel(int id, Label updatedLabel)
    {
            var labelToUpdate = _context.Find < Label > (id);
    // Console.WriteLine(issueToUpdate+"--------updatedIssue service"+issueToUpdate.Description+updatedIssue.Description);
    if(labelToUpdate!=null)
    {
        labelToUpdate._label= updatedLabel._label;
      
        _context.Update < Label > (labelToUpdate);
         _context.SaveChanges();
    }
    }

}
