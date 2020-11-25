using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Models.ViewModels;
using ExamManagementSystem.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamManagementSystem.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create(int id)
        {
            //if section tecaher mismatch return
            ExamRepository examRepository = new ExamRepository();
            Exam exam = examRepository.Get(id);
            return View(exam);
        }
        [HttpPost]
        public ActionResult CreateM(string Question, string Options, string CorrectAnswers)
        {
            //if section tecaher mismatch return

            QuestionJSONModel questionJSON = JsonConvert.DeserializeObject<QuestionJSONModel>(Question);
            List<OptionJSON> optionJSONList = JsonConvert.DeserializeObject<List<OptionJSON>>(Options);
            List<int> correctAnswerList = JsonConvert.DeserializeObject<List<int>>(CorrectAnswers);

            questionJSON.CorrectAnswers = correctAnswerList;

            QuestionRepository questionRepository = new QuestionRepository();
            int id = questionRepository.InsertFromJSON(questionJSON);

            OptionRepository optionRepository = new OptionRepository();
            optionRepository.InsertOptionList(optionJSONList, id);

            dynamic obj = new
            {
                Question = questionJSON,
                Options = optionJSONList
            };

            return Json(questionJSON.CorrectAnswers, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Create(string Question)
        {
            //if section tecaher mismatch return

            QuestionJSONModel questionJSON = JsonConvert.DeserializeObject<QuestionJSONModel>(Question);

            QuestionRepository questionRepository = new QuestionRepository();
            int id = questionRepository.InsertFromJSON(questionJSON);

            dynamic obj = new
            {
                Question = questionJSON,
            };

            return Json(questionJSON, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            QuestionRepository questionRepository = new QuestionRepository();
            Question question = questionRepository.Get(id);

            return View(question);
        }
        [HttpPost]
        public ActionResult EditM(string Id, string Question, string Options, string CorrectAnswers)
        {
            //if section tecaher mismatch return

            QuestionJSONModel questionJSON = JsonConvert.DeserializeObject<QuestionJSONModel>(Question);
            List<OptionJSON> optionJSONList = JsonConvert.DeserializeObject<List<OptionJSON>>(Options);
            List<int> correctAnswerList = JsonConvert.DeserializeObject<List<int>>(CorrectAnswers);
            int QuestionId = JsonConvert.DeserializeObject<int>(Id);

            questionJSON.CorrectAnswers = correctAnswerList;

            QuestionRepository questionRepository = new QuestionRepository();

            questionRepository.UpdateFromJSON(questionJSON,QuestionId);

            OptionRepository optionRepository = new OptionRepository();
            optionRepository.UpdateOptionList(optionJSONList, QuestionId);

            dynamic obj = new
            {
                Question = questionJSON,
                Options = optionJSONList
            };

            return Json(questionJSON.CorrectAnswers, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Edit(string Id,string Question)
        {
            //if section tecaher mismatch return

            QuestionJSONModel questionJSON = JsonConvert.DeserializeObject<QuestionJSONModel>(Question);
            int QuestionId = JsonConvert.DeserializeObject<int>(Id);

            QuestionRepository questionRepository = new QuestionRepository();
            questionRepository.UpdateFromJSON(questionJSON, QuestionId);

            dynamic obj = new
            {
                Question = questionJSON,
            };

            return Json(questionJSON, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Delete(int id)//question id
        {
            //Exam exam = new ExamRepository().Get(id);
            Question question = new QuestionRepository().Get(id);
            return View(question);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteExam(int id)
        {
            //return Content(exam.StartTime.ToString());
            //exam.StartTime = Convert.ToDateTime(exam.StartTime.ToString());
            ExamRepository examRepository = new ExamRepository();

            QuestionRepository questionRepository = new QuestionRepository();

            int examId = questionRepository.Get(id).ExamId;

            questionRepository.Delete(id);

            return RedirectToAction("Index","Exam",new { id = examId });
        }
    }
}