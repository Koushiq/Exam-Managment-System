using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamManagementSystem.Repository
{
    public class TrashboxController : Controller
    {
        CourseRepository course = new CourseRepository();
        SectionRepository section = new SectionRepository();
        // GET: Trashbox
        public ActionResult Index()
        {
            if ((string)Session["userType"] != "admin")
            {
                return RedirectToAction("Login", "User");
            }
            
            return View();
        }

        public ActionResult Course()
        {
            if ((string)Session["userType"] != "admin")
            {
                return RedirectToAction("Login", "User");
            }
            
            return View(course.GetAll().Where(s=>s.DeletedAt!=null));
        }

        public ActionResult Section()
        {
            if ((string)Session["userType"] != "admin")
            {
                return RedirectToAction("Login", "User");
            }

            return View(section.GetAll().Where(s => s.DeletedAt != null));
        }
    }
}