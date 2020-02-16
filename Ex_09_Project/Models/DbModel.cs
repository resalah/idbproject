using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ex_09_Project.Models
{
    public enum Grade { Ex01, Ex02, M01, M03, G01, G02 }
    public class Department
    {
        public int DepartmentId { get; set; }
        [Required, StringLength(40), Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        //Navigation
        public virtual ICollection<Employee> Employees { get; set; }
    }
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required, StringLength(40), Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Join Date"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime JoinDate { get; set; }
        [EnumDataType(typeof(Grade))]
        public Grade? Grade { get; set; }
        //FK
        [Required, ForeignKey("Department")]
        public int DepartmentId { get; set; }
        //Navigation
        public virtual Department Department { get; set; }
    }
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
