using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ex_09_Project.ViewModels
{
    public class DepartmentVM
    {
        public int DepartmentId { get; set; }
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        [Display(Name = "Number of Employees")]
        public int EmployeeCount { get; set; }
        
    }
}
