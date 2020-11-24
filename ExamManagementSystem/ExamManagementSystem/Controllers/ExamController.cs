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
            List<Question> qts = qtr.GetQuestionsByExamId(eid);
            
            return View(qts);
        }

        [HttpPost]
        public ActionResult Answer(int questionId, int id)
        {
            SubmittedAnswerRepository sar = new SubmittedAnswerRepository();
            List<SubmittedAnswer> sal = sar.GetAllSAByQuestionId(questionId);
            OptionRepository opr = new OptionRepository();
            int OptionId = opr.Get(id).OptionId;
            SubmittedAnswer sa = new SubmittedAnswer();
            sa.StudentId = uid;
            sa.QuestionId = questionId;
            int bin = 0;
            BitwiseServices.SetBit(ref bin, OptionId);
            sa.OptionBin = bin;

            if (sal.Count == 0)
            {
                sa.AttemptTime = 1;
                sar.Insert(sa);
            }
            else
            {
                sa.AttemptTime = sal.Count + 1;
                sar.Insert(sa);
            }

            return Content("QId: "+questionId+" OId: "+id + " Count: " + sal.Count);
        }

        [HttpPost]
        public ActionResult TextAnswer(int questionId, string ansText)
        {
            SubmittedAnswerRepository sar = new SubmittedAnswerRepository();
            SubmittedAnswer sa = new SubmittedAnswer();
            List<SubmittedAnswer> sal = sar.GetAllSAByQuestionId(questionId); ;
            sa.StudentId = uid;
            sa.QuestionId = questionId;
            sa.AnswerText = ansText;

            if (sal.Count == 0)
            {
                sa.AttemptTime = 1;
                sar.Insert(sa);
            }
            else
            {
                sa.AttemptTime = sal.Count + 1;
                sar.Insert(sa);
            }

            return Content("QId: " + questionId + "AnsText: "+ansText+" Count: "+ sal.Count);
        }
    }
}