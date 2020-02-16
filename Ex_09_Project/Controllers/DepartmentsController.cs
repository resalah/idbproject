using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ex_09_Project.Models;
using Ex_09_Project.Repositories;
using Ex_09_Project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ex_09_Project.Controllers
{
    //[Authorize]
    public class DepartmentsController : Controller
    {
        IEmployeeRepository repo;
        public DepartmentsController(IEmployeeRepository repo) { this.repo = repo; }
        public IActionResult Index(int pg =1)
        {
            var data = repo.GetDepartments().OrderBy(x => x.DepartmentId);
            ViewBag.Pager = new PagerModel
            {
                TotalPages = (int)Math.Ceiling((double)data.Count() / 5),
                CurrentPage = pg
            };
            return View(data.Skip((pg-1)*5).Take(5).ToList());
        }
        public IActionResult Summary(int pg=1, string sort="", string search="")
        {
            Thread.Sleep(2000);
            var data = repo.GetSummary();
            ViewBag.Pager = new PagerModel
            {
                TotalPages=(int)Math.Ceiling((double)data.Count()/5),
                CurrentPage=pg
            };
            if (sort == "") { ViewBag.NameSort = "name"; }
            ViewBag.NameSort = sort == "name" ? "name_desc" : "name";
            ViewBag.CountSort = sort == "count" ? "count_desc" : "count";

            ViewBag.CurrentSort = sort == "" ? "name" : sort;
            ViewBag.Search = search;
            switch (sort)
            {
                case "name":
                    data = data.OrderBy(x => x.DepartmentName);
                    break;
                case "name_desc":
                    data = data.OrderByDescending(x => x.DepartmentName);
                    break;
                case "count":
                    data = data.OrderBy(x => x.EmployeeCount);
                    break;
                case "count_desc":
                    data = data.OrderByDescending(x => x.EmployeeCount);
                    break;
                default:
                    data = data.OrderBy(x => x.DepartmentId);
                    break;
            }
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.DepartmentName.ToLower().StartsWith(search.ToLower()));
            }
            var modelData = data.Skip((pg - 1) * 5).Take(5).ToList();
            ///////////////////////////////////////////////////////////////////////
            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_DepartmentSummary", modelData);
            }
            ///////////////////////////////////////////////////////////////////////
            return View(modelData);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department dept)
        {
            if (ModelState.IsValid) {
                repo.InsertDepartment(dept);
                return RedirectToAction("Index");
            }
            return View(dept);
        }
        public IActionResult Edit(int id)
        {
            var dept = repo.GetDepartmentById(id);
            if(dept == null)
            {
                return NotFound();
            }
            return View(dept);
        }
        [HttpPost]
        public IActionResult Edit(Department dept)
        {
            if (ModelState.IsValid)
            {
                repo.EditDepartment(dept);
                return RedirectToAction("Index");
            }
            return View(dept);
        }
        public IActionResult Delete(int id)
        {
            var dept = repo.GetDepartmentById(id);
            if (dept == null)
            {
                return NotFound();
            }
            return View(dept);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ApplyDelete(int id)
        {
            
            repo.DeleteDepartment(id);
            return RedirectToAction("Index");
           
            
        }
    }
}