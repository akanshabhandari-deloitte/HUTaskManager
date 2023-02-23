using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerApi.Models;
public class Employee
    {
        [Key]
        public int EmployeeId {
            get;
            set;
        }
        public string? EmployeeFirstName {
            get;
            set;
        }
        public string? EmployeeLastName {
            get;
            set;
        }
         public string? Password {
            get;
            set;
        }
         public string? Email {
            get;
            set;
        }
        public decimal Salary {
            get;
            set;
        }
        public string? Designation {
            get;
            set;
        }

    }