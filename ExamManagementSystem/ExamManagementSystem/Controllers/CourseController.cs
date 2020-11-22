using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        CourseRepository courseRepo = new CourseRepository();
        // GET: Course
        public ActionResult Index()
        {
            return View(courseRepo.GetAll().ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Cours course)
        {
            courseRepo.SetValues(course);
            courseRepo.Insert(course);
            return RedirectToAction("Create"); 
        }
    }
}