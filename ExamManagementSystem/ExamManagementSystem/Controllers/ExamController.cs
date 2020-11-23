using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamManagementSystem.Controllers
{
    namespace ExamManagementSystem.Controllers
    {
        public class ExamController : Controller
        {
            // GET: Exam
            [HttpGet]
            public ActionResult Answer(int eid)
            {
                ExamRepository exam = new ExamRepository();
                ViewBag.Exm = exam.Get(eid);
                ViewBag.Course = exam.Get(eid).Section.Cours.CourseName;

                QuestionRepository qtr = new QuestionRepository();
                List<Question> qts = new List<Question>();
                qts = qtr.GetQuestionsByExamId(eid);

                return View(qts);
            }
            [HttpGet]
            public ActionResult Create(int id)
            {
                ViewBag.SectionId = id;
                Section section = new SectionRepository().Get(id);
                //if section tecaher mismatch return

                ViewBag.CourseSection = section.Cours.CourseName + "[ "+ section.SectionName + " ]";
                return View();
            }
            [HttpPost]
            public ActionResult Create(Exam exam)
            {
                if(ModelState.IsValid)
                {
                    ExamRepository examRepository = new ExamRepository();
                    examRepository.Insert(exam);
                    return RedirectToAction("Section", "Teacher", new { id = exam.SectionId });
                }
                return View("Create");
            }
            public ActionResult CreateQuestions(int id)
            {
                //if section tecaher mismatch return
                ExamRepository examRepository = new ExamRepository();
                Exam exam = examRepository.Get(id);
                return View(exam);
            }
        }
    }
}