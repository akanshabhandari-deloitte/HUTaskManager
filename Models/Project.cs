using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TaskManagerApi.Models;
public class Project
    {
      
        [Key]
        public int Id {
            get;
            set;
        }
        public string? Description {
            get;
            set;
        }


     
        public Employee? Creator{
            get;
            set;
        } 
         [JsonIgnore]
          public  List<Issue>? Issues { get; set; }   
    
    }