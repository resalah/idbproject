using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ex_09_Project.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ex_09_Project.Areas.Angular.Controllers
{
    [Area("Angular")]
    public class SPAController : Controller
    {
        IEmployeeRepository repo;
        public SPAController (IEmployeeRepository repo) { this.repo = repo; }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult ProductList()
        {
            var data = repo.GetEmployees().Select(x => new {
                x.EmployeeId, x.EmployeeName, x.JoinDate, Grade=x.Grade.ToString(), x.DepartmentId, DepartmentName=x.Department.DepartmentName
            }).ToList();
            return Json(data);
        }
    }
}