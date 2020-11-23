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
            course.CreatedBy = (int)Session["userId"]; //Convert.ToInt32(HttpContext.Request.Cookies.Get("Id"));
            courseRepo.SetValues(course);
            courseRepo.Insert(course);
            return RedirectToAction("Create"); 
        }


        [HttpGet]
        public ActionResult Delete(int id=-1)
        {
            bool res = courseRepo.GetAll().Any(s => s.Id == id);
            if (id!=-1 && res)
            {
                return View(courseRepo.Get(id));
            }

            Session["wrongId"] = true;
            return RedirectToAction("Index");

        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {



            Cours course = courseRepo.Get(id);
            if (course.DeletedBy == null)
            {
                course.DeletedBy = (int)Session["userId"];
                courseRepo.SoftDelete(course);

                Session["confirmDelete"] = true;
            }
            else
            {
                Session["confirmDelete"] = false;
            }
            
            return RedirectToAction("Index");
        }
    }
}