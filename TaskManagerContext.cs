using System;  
using System.Collections.Generic;   
using System.Linq;
using TaskManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerApi;
public class TaskManagerContext : DbContext
    {
        public DbSet<Employee>? Employees { get; set; }

        public TaskManagerContext(DbContextOptions options):base(options)
        {
            
        }

    }