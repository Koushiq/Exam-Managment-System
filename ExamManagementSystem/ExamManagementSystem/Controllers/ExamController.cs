using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ExamManagementSystem.Models.ViewModels;

namespace ExamManagementSystem.Controllers
{
    namespace ExamManagementSystem.Controllers
    {
        public class ExamController : Controller
        {
            public object QuestionJSONModel { get; private set; }

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
            public ActionResult List()//exam list
            {
                ExamRepository examRepository = new ExamRepository();
                List<Exam> examList = examRepository.GetAll().Where(e => e.Section.TeacherId == 12).ToList().OrderByDescending(e => e.StartTime).ToList(); //session

                return View(examList);
            }

            [HttpGet]
            public ActionResult Index(int id)//exam id
            {
                ExamRepository examRepository = new ExamRepository();
                Exam exam = examRepository.Get(id);
                int sectionId = exam.SectionId;
                int? teacherId = exam.Section.TeacherId;

                ViewBag.SectionId = id;
                Section section = new SectionRepository().Get(sectionId);
                //if section tecaher mismatch return
                return View(exam);
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
                    return RedirectToAction("Create", "Question", new { id = exam.Id });
                }
                return View("Create");
            }

            [HttpGet]
            public ActionResult Edit(int id)//exam id
            {
                ViewBag.SectionId = id;
                Exam exam = new ExamRepository().Get(id);
                //if section tecaher mismatch return

                //ViewBag.CourseSection = section.Cours.CourseName + "[ " + section.SectionName + " ]";
                return View(exam);
            }

            [HttpPost]
            public ActionResult Edit(Exam exam)
            {
                //return Content(exam.StartTime.ToString());
                //exam.StartTime = Convert.ToDateTime(exam.StartTime.ToString());
                if (ModelState.IsValid)
                {
                    ExamRepository examRepository = new ExamRepository();
                    Exam exam2 = examRepository.Get(exam.Id);
                    exam2.ExamName = exam.ExamName;
                    exam2.StartTime = exam.StartTime;
                    exam2.Duration = exam.Duration;
                    examRepository.Update(exam2);
                    return RedirectToAction("Index", new { id = exam.Id });
                }

                return View("Create");
            }
            [HttpGet]
            public ActionResult Delete(int id)//exam id
            {
                Exam exam = new ExamRepository().Get(id);

                return View(exam);
            }

            [HttpPost, ActionName("Delete")]
            public ActionResult DeleteExam(int id)
            {
                //return Content(exam.StartTime.ToString());
                //exam.StartTime = Convert.ToDateTime(exam.StartTime.ToString());
                ExamRepository examRepository = new ExamRepository();
                examRepository.DeleteExam(id);

                return RedirectToAction("List");
            }

        }
    }
}