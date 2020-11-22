using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Models.ViewModel;
using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamManagementSystem.Controllers
{
    public class SectionController : Controller
    {
        SectionRepository sectionRepo = new SectionRepository();
        TeacherRepository teacherRepo = new TeacherRepository();
        CourseRepository courseRepo = new CourseRepository();
        // GET: Section
        public ActionResult Index()
        {
            return View(sectionRepo.GetAll().ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            TeacherViewModel teacher = teacherRepo.GetAllTeachers();
           
            CourseViewModel course = courseRepo.GetAllCourses();
            ViewBag.teacherUsername = teacher.teacherUsername;
            ViewBag.teacherId = teacher.teacherId;
            ViewBag.courseNames = course.courseNames;
            ViewBag.courseIds = course.courseIds;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Section section)
        {
            sectionRepo.SetValues(section);
            sectionRepo.Insert(section);
            return RedirectToAction("Index");
        }
    }
}