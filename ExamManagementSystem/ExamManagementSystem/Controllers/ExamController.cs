using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

            /*foreach(var q in qts)
            {
                if(q.Type=="MCQ" || q.Type=="Check")
                {
                    OptionRepository opt = new OptionRepository();
                    List<Option> op = new List<Option>();
                    op = opt.GetOptionsByQuestionId(q.Id);
                }
            }*/

            return View(qts);
        }
    }
}