using TaskManagerApi.Models;
namespace TaskManagerApi.Services;
public interface IProjectService
    {
        List<Project> GetProjectList();
        ResponseModel SaveProject(int id,Project projectModel);
           public Project GetProjectDetailsById(int projectId);

           public void UpdateProject(int id ,Project updatedProject);
          public ResponseModel DeleteProject(int projectId);

}