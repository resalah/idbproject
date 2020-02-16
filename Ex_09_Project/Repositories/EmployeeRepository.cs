using Ex_09_Project.Models;
using Ex_09_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ex_09_Project.Repositories
{
    public class EmployeeRepository:IEmployeeRepository
    {
        EmployeeDbContext db = null;
        public EmployeeRepository(EmployeeDbContext db) {
            this.db = db;
            //this.db.Database.EnsureCreated();
            if (!this.db.Departments.Any()) this.SeedDummy();
        }

        public void DeleteDepartment(int id)
        {
            var dept = new Department { DepartmentId = id };
            db.Entry(dept).State = EntityState.Deleted;
            db.SaveChanges();
        }

        public void EditDepartment(Department dept)
        {
            db.Entry(dept).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Department GetDepartmentById(int id)
        {
            return db.Departments.FirstOrDefault(x => x.DepartmentId == id);
        }

        public IQueryable<Department> GetDepartments()
        {
            return db.Departments;
        }

        public IQueryable<DepartmentVM> GetSummary()
        {
            return db.Departments.Include(x => x.Employees)
                 .Select(x => new DepartmentVM
                 {
                     DepartmentId = x.DepartmentId,
                     DepartmentName = x.DepartmentName,
                     EmployeeCount = x.Employees.Count
                 }).AsQueryable();
        }

        public void InsertDepartment(Department dept)
        {
            db.Departments.Add(dept);
            db.SaveChanges();
        }
        ///////////////////////////////////////////////////////
        ///
        public IEnumerable<Employee> GetEmployees()
        {
            return db.Employees.Include(x=> x.Department);
        }
        public Employee GetEmployeeById(int id)
        {
            return db.Employees.Include(x=> x.Department).FirstOrDefault(x => x.EmployeeId == id);
        }
        public void EditEmployee(Employee emp)
        {
            db.Entry(emp).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void InsertEmployee(Employee emp)
        {
            db.Employees.Add(emp);
            db.SaveChanges();
        }
        public void DeleteEmployee(int id)
        {
            var emp = new Employee { EmployeeId = id };
            db.Entry(emp).State = EntityState.Deleted;
            db.SaveChanges();
        }
        public string[] GradeList()
        {
            return Enum.GetNames(typeof(Grade));
        }
        private void SeedDummy()
        {
            
                db.Departments.AddRange(new Department[]
                {
                    new Department{DepartmentName="IT"},
                    new Department{DepartmentName="Accounts"},
                    new Department{DepartmentName="Production"},
                    new Department{DepartmentName="HR"},
                    new Department{DepartmentName="Marketing"},
                    new Department{DepartmentName="Finance"}
                });
                db.SaveChanges();
                db.Employees.AddRange(new Employee[]
                {
                    new Employee{ EmployeeName="E1", DepartmentId=1, Grade=Grade.G01, JoinDate=DateTime.Now.AddYears(-2)},
                    new Employee{ EmployeeName="E2", DepartmentId=2, Grade=Grade.G01, JoinDate=DateTime.Now.AddYears(-1)}
                });
                db.SaveChanges();
           
        }
    }
}
