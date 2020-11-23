using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Models.ServiceAccess;
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
        int uid = 1;
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

        [HttpPost]
        public ActionResult Answer(int questionId, int id)
        {
            SubmittedAnswerRepository sar = new SubmittedAnswerRepository();
            OptionRepository opr = new OptionRepository();
            int OptionId = opr.Get(id).OptionId;
            SubmittedAnswer sa = new SubmittedAnswer();
            sa.StudentId = uid;
            sa.QuestionId = questionId;
            int bin = 0;
            BitwiseServices.SetBit(ref bin, OptionId);
            sa.OptionBin = bin;

            if (sar.GetSAByQuestionId(questionId) == null)
            {
                sa.AttemptTime = 1;
                sar.Insert(sa);
            }
            else
            {
                sa.AttemptTime += 1;
                sar.Update(sa);
            }

            return Content("QId: "+questionId+" OId: "+id);
        }

        [HttpPost]
        public ActionResult TextAnswer(int questionId, string ansText)
        {
            SubmittedAnswerRepository sar = new SubmittedAnswerRepository();
            SubmittedAnswer sa = new SubmittedAnswer();
            sa.StudentId = uid;
            sa.QuestionId = questionId;
            sa.AnswerText = ansText;

            if (sar.GetSAByQuestionId(questionId) == null)
            {
                sa.AttemptTime = 1;
                sar.Insert(sa);
            }
            else
            {
                sa.AttemptTime += 1;
                sar.Update(sa);
            }

            return Content("QId: " + questionId + "AnsText: "+ansText);
        }
    }
}