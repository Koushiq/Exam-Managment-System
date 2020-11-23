using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {  
            return View(new SectionRepository().GetSectionListForTeacher(12));
        }
        public ActionResult Section(int id)
        {
            return View(new SectionRepository().GetSectionForTeacher(id));
        }
    }
}