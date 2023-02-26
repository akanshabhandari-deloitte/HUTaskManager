using TaskManagerApi.Models;
namespace TaskManagerApi.Services;
public interface IIssueService
    {
    
        List<Issue> GetIssueList();
        ResponseModel SaveIssue(int id,int reporter_id,Issue issueModel);
        public Issue GetIssueDetailsById(int issueId);

         public bool DeleteIssue(int projectId);
         public void UpdateIssue(int id,Issue updatedIssue);
          public List<Issue>  GetIssuesByProject(int id);
          public List<Issue> GetDetailsOfIssuesInProject(int projectId,int issue_id);

          public bool DeleteIssueUnderAProject(int projectId,int issueId);
    void UpdateIssueUnderAProject(int project_id, int issue_id, Issue updatedIssue);
      public void AssigneIssueToUser(int issue_id,int user_id);
       public void UpdateStatusOfIssue(int issue_id,Issue _status);
        public List<Issue> SearchOnTitleAndDescription(string _title,string _description);
}