using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ex_09_Project.Models;
using Ex_09_Project.Repositories;

namespace Ex_09_Project.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository repo;

        public EmployeesController(IEmployeeRepository repo)
        {
            this.repo  = repo;
        }
        // GET: Employees
        public IActionResult Index()
        {
            
            return View(this.repo.GetEmployees().ToList());
        }

        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(repo.GetDepartments(), "DepartmentId", "DepartmentName");
            ViewData["GradeList"] = new SelectList(repo.GradeList());
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EmployeeId,EmployeeName,JoinDate,Grade,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                repo.InsertEmployee(employee);
               
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(repo.GetDepartments(), "DepartmentId", "DepartmentName", employee.DepartmentId);
            ViewData["GradeList"] = new SelectList(repo.GradeList());
            return View(employee);
        }

        // GET: Employees/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee =  repo.GetEmployeeById(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(repo.GetDepartments(), "DepartmentId", "DepartmentName", employee.DepartmentId);
            ViewData["GradeList"] = new SelectList(repo.GradeList());
            return View(employee);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("EmployeeId,EmployeeName,JoinDate,Grade,DepartmentId")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                 repo.EditEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(repo.GetDepartments(), "DepartmentId", "DepartmentName", employee.DepartmentId);
            ViewData["GradeList"] = new SelectList(repo.GradeList());
            return View(employee);
        }

       
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = repo.GetEmployeeById(id.Value);
                
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            repo.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool EmployeeExists(int id)
        //{
        //    return _context.Employees.Any(e => e.EmployeeId == id);
        //}
    }
}
