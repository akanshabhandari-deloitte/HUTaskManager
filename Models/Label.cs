using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TaskManagerApi.Models;
public class Label
    {
        [Key]
        public int Id{
            get;
            set;
        }
        public string? _label{
            get;
            set;
        }

        public Issue? issue{
            get;set;
        }
    }