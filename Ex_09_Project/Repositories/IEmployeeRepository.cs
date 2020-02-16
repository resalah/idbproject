using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ex_09_Project.Models;
using Ex_09_Project.ViewModels;

namespace Ex_09_Project.Repositories
{
    public interface IEmployeeRepository
    {
        IQueryable<DepartmentVM> GetSummary();
        IQueryable<Department> GetDepartments();
        Department GetDepartmentById(int id);
        void InsertDepartment(Department dept);
        void EditDepartment(Department dept);
        void DeleteDepartment(int id);
        ///////////////////////////////
        ///
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeeById(int id);
        void EditEmployee(Employee emp);
        void InsertEmployee(Employee emp);
        void DeleteEmployee(int id);
         string[] GradeList();
    }
}
