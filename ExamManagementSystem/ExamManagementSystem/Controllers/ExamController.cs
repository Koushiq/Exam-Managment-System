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
                    return RedirectToAction("Section", "Teacher", new { id = exam.SectionId });
                }
                return View("Create");
            }
            //public ActionResult CreateQuestions(int id)
            //{
            //    //if section tecaher mismatch return
            //    ExamRepository examRepository = new ExamRepository();
            //    Exam exam = examRepository.Get(id);
            //    return View(exam);
            //}
            //[HttpPost]
            //public ActionResult CreateMQuestions(string Question, string Options, string CorrectAnswers)
            //{
            //    //if section tecaher mismatch return

            //    QuestionJSONModel questionJSON = JsonConvert.DeserializeObject<QuestionJSONModel>(Question);
            //    List<OptionJSON> optionJSONList = JsonConvert.DeserializeObject<List<OptionJSON>>(Options);
            //    List<int> correctAnswerList = JsonConvert.DeserializeObject<List<int>>(CorrectAnswers);

            //    questionJSON.CorrectAnswers = correctAnswerList;

            //    QuestionRepository questionRepository = new QuestionRepository();
            //    int id = questionRepository.InsertFromJSON(questionJSON);

            //    OptionRepository optionRepository = new OptionRepository();
            //    optionRepository.InsertOptionList(optionJSONList, id);

            //    dynamic obj = new
            //    {
            //        Question = questionJSON,
            //        Options = optionJSONList
            //    };

            //    return Json(questionJSON.CorrectAnswers, JsonRequestBehavior.AllowGet);
            //}
            //[HttpPost]
            //public ActionResult CreateQuestions(string Question)
            //{
            //    //if section tecaher mismatch return

            //    QuestionJSONModel questionJSON = JsonConvert.DeserializeObject<QuestionJSONModel>(Question);

            //    QuestionRepository questionRepository = new QuestionRepository();
            //    int id = questionRepository.InsertFromJSON(questionJSON);

            //    dynamic obj = new
            //    {
            //        Question = questionJSON,
            //    };

            //    return Json(questionJSON, JsonRequestBehavior.AllowGet);
            //}
        }
    }
}