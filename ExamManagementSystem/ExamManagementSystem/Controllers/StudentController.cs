using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        StudentRepository student = new StudentRepository();
        // GET: Student
        [HttpGet]
        public ActionResult Index()
        {
            return View(student.Get(1));
        }
        
        [HttpGet]
        public ActionResult CourseList()
        {
            EnrollRepository enr = new EnrollRepository();
            return View(enr.GetAllByStudentId(1));
        }

        [HttpGet]
        public ActionResult Gradesheet()
        {
            GradeSheetRepository gsr = new GradeSheetRepository();
            return View(gsr.GetStudentGradesheet(1));
        }
    }
}