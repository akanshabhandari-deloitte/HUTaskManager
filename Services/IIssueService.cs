using TaskManagerApi.Models;
namespace TaskManagerApi.Services;
public interface IIssueService
    {
    
        List<Issue> GetIssueList();
        ResponseModel SaveIssue(int id,int reporter_id,int assignee_id,Issue issueModel);
        public Issue GetIssueDetailsById(int issueId);

         public bool DeleteIssue(int projectId);
         public void UpdateIssue(int id,Issue updatedIssue,int assignee_id);
    }