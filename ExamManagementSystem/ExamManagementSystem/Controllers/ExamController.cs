using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Models.ServiceAccess;
using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
        public ActionResult CheckBoxAnswer(int questionId, string selected)
        {
            SubmittedAnswerRepository sar = new SubmittedAnswerRepository();
            SubmittedAnswer sa = new SubmittedAnswer();
            List<SubmittedAnswer> sal = sar.GetAllSAByQuestionId(questionId);
            OptionRepository opr = new OptionRepository();
            List<int> OptionList = new List<int>();

            List<int> OpId = this.ParseSelection(selected);
            foreach(var id in OpId)
            {
               OptionList.Add(opr.Get(id).OptionId);
            }

            sa.StudentId = uid;
            sa.QuestionId = questionId;
            sa.OptionBin = BitwiseServices.GetIntegerValue(OptionList);

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

            return Content("QId: "+questionId+" OptionBin: "+sa.OptionBin+" List: " +selected);
        }

        [HttpPost]
        public ActionResult TextAnswer(int questionId, string ansText)
        {
            SubmittedAnswerRepository sar = new SubmittedAnswerRepository();
            SubmittedAnswer sa = new SubmittedAnswer();
            List<SubmittedAnswer> sal = sar.GetAllSAByQuestionId(questionId);
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

        [HttpPost]
        public ActionResult FileAnswer(ImageModel img)
        {
            SubmittedAnswerRepository sar = new SubmittedAnswerRepository();
            SubmittedAnswer sa = new SubmittedAnswer();
            int questionId = Convert.ToInt32(Request.Form["QId"]);
            List<SubmittedAnswer> sal = sar.GetAllSAByQuestionId(questionId);
            sa.StudentId = uid;
            sa.QuestionId = questionId;
            sa.Filepath = ImageHandler(questionId, img);

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

            int eid = Convert.ToInt32(Request.Form["EId"]);
            return RedirectToAction("Answer", new { eid = eid});
        }

        private List<int> ParseSelection(string selected)
        {
            List<int> OpId = new List<int>();
            string[] split = selected.Split(new char[] {'=','&'});
            
            foreach(var s in split)
            {
                s.Trim();
                if (s.Trim() != "selected")
                {
                    OpId.Add(Convert.ToInt32(s));
                }
            }

            return OpId;
        }

        private string ImageHandler(int questionId, ImageModel img)
        {
            string Filename = Path.GetFileNameWithoutExtension(img.File.FileName);
            string FileExt = Path.GetExtension(img.File.FileName);
            Filename = questionId + "-" + uid + "-" + DateTime.Now.ToString("yyyyMMdd HHmmss") + FileExt;
            string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
            img.ImagePath = UploadPath + Filename;
            img.File.SaveAs(img.ImagePath);
            return img.ImagePath;
        }
    }
}