using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamManagementSystem.Controllers
{
    public class StudentController : BaseController
    {
        private int id;
        
        StudentRepository student = new StudentRepository();
        UserRepository userRepo = new UserRepository();
        // GET: Student
        [HttpGet]
        public ActionResult Index()
        {
            this.id = (int)Session["userId"];
            User user = this.userRepo.Get(this.id);
            if(user==null || user.Usertype!="student")
            {
                return RedirectToAction("Logout", "User");
            }
            return View(student.Get(id));
        }

        [HttpGet]
        public ActionResult CourseList()
        {
            this.id = (int)Session["userId"];
            EnrollRepository enr = new EnrollRepository();
            return View(enr.GetAllByStudentId(id));
        }

        [HttpGet]
        public ActionResult NewExams(int sid)
        {
            ExamRepository exams = new ExamRepository();
            return View(exams.GetFutureExamsBySectionId(sid));
        }

        [HttpGet]
        public ActionResult StartExam(int eid)
        {
            return RedirectToAction("Answer", "Exam", new { eid = eid });
        }

        [HttpGet]
        public ActionResult PastExams(int sid)
        {
            ExamRepository exams = new ExamRepository();
            return View(exams.GetPastExamsBySectionId(sid));
        }

        [HttpGet]
        public ActionResult Gradesheet()
        {
            this.id = (int)Session["userId"];
            GradeSheetRepository gsr = new GradeSheetRepository();
            return View(gsr.GetStudentGradesheet(id));
        }
    }
}