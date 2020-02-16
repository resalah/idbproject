using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ex_09_Project.Models;
using Ex_09_Project.Data;

namespace Ex_09_Project.Controllers
{
    public class HomeController : Controller
    {
        EmployeeDbContext db;
        ApplicationDbContext appDb;
        public HomeController(EmployeeDbContext db, ApplicationDbContext appDb)
        {
            this.db = db;
            this.appDb = appDb;
            this.db.Database.EnsureCreated();
            this.appDb.Database.EnsureCreated();
            if (!db.Departments.Any()) SeedDummy();
        }
        public IActionResult Index()
        {
            

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
