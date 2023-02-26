using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TaskManagerApi.Models;
public class Issue
    {

           public enum IssueType{
             Bug,Task,Story,Epic
           }

          public enum IssueStatus
{
    Open,
    InProgress,
    InReview,
    CodeComplete,
    QATesting,
    Done
}
        [Key]
        public int Id {
            get;
            set;
        }
       

            public IssueType Type {
            get;
            set;
        }

    public string? Title {
            get;
            set;
        }

    public string? Description {
            get;
            set;
        }

     
         public IssueStatus Status {
            get;
            set;
        }
          public Project? Project{
            get;
            set;
        } 
        public Employee? Reporter{
            get;
            set;
        }
           public Employee? Assginee{
            get;
            set;
        }

        public List<Label>? Label{
            get;
            set;
        }
 
    }