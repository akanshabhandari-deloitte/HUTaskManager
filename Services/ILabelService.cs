using TaskManagerApi.Models;
namespace TaskManagerApi.Services;
public interface ILabelService
    {
    ResponseModel AddLabelToIssue(int label_id,int issue_id);
     string DeleteLabelFromIssue(int label_id,int issue_id);
      public void UpdateLabel(int id ,Label updatedLabel);
       public ResponseModel DeleteLabel(int labelId);
       public List<Label> GetLabelList();
        public ResponseModel SaveLabel(Label labelModel);
    }