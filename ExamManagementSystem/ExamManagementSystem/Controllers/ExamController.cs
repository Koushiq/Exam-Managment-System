using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Models.ServiceAccess;
using ExamManagementSystem.Repository;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
            QuestionRepository qtr = new QuestionRepository();
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

            int eid = qtr.Get(questionId).ExamId;
            return RedirectToAction("Answer", new { eid = eid});
        }

        [HttpGet]
        public ActionResult Completed(int eid)
        {
            string pdf = this.GeneratePDF(eid);
            byte[] FileBytes = System.IO.File.ReadAllBytes(pdf);
            return File(FileBytes, "application/pdf");
        }

        [NonAction]
        private List<int> ParseSelection(string selected)
        {
            List<int> OpId = new List<int>();

            string[] split = selected.Split(new char[] {'=','&'});
            Regex rgx = new Regex(@"^[0-9]*$");

            foreach(var s in split)
            {
                s.Trim();
                if (rgx.IsMatch(s.Trim())==true)
                {
                    OpId.Add(Convert.ToInt32(s));
                }
            }

            OpId.RemoveAt(0);

            return OpId;
        }

        [NonAction]
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

        [NonAction]
        private string GeneratePDF(int eid)
        {
            ExamRepository exr = new ExamRepository();
            Exam exam = exr.Get(eid);
            List<SubmittedAnswer> answers = GetAnswerList(eid);
            string pdfFolder = @"C:\Users\Habiba\Desktop\APWDN\Project\Exam Management System\ExamManagementSystem\ExamManagementSystem\PDFs\"+uid+"-"+eid+".pdf";
            
            PdfWriter writer = new PdfWriter(pdfFolder);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            //Header
            Paragraph header = new Paragraph(exam.ExamName).SetTextAlignment(TextAlignment.CENTER).SetFontSize(20);
            //New Line
            Paragraph newline = new Paragraph(new Text("\n"));
            //Adding Header & New Line
            document.Add(newline);
            document.Add(header);
            //Line Separator
            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);

            foreach(var a in answers)
            {
                Paragraph question = new Paragraph(new Text(a.Question.Statement));
                document.Add(question);
                document.Add(newline);

                if (a.Question.Type == "Text")
                {
                    Paragraph answer = new Paragraph(new Text(a.AnswerText));
                    document.Add(answer);
                    document.Add(newline);
                }
                else if (a.Question.Type == "Check" || a.Question.Type == "MCQ")
                {
                    List<Option> opt = a.Question.Options.ToList();
                    List<int> selected = BitwiseServices.GetEnabledIndexList(a.OptionBin);
                    foreach (var o in opt)
                    {
                        foreach (var s in selected)
                        {
                            if (o.OptionId == s)
                            {
                                Paragraph answer = new Paragraph(new Text(o.OptionText));
                                document.Add(answer);
                                document.Add(newline);
                            }   
                        }
                    }
                }
                else if (a.Question.Type == "Descriptive")
                {
                    Image img = new Image(ImageDataFactory.Create(a.Filepath)).SetTextAlignment(TextAlignment.CENTER);
                    document.Add(img);
                }
                else { break; }
            }

            document.Close();
            return pdfFolder;
        }

        [NonAction]
        public List<SubmittedAnswer> GetAnswerList(int eid)
        {
            List<SubmittedAnswer> sa = new List<SubmittedAnswer>();
            QuestionRepository qtr = new QuestionRepository();
            List<Question> qList = qtr.GetQuestionsByExamId(eid);
            
            foreach(var q in qList)
            {
                sa.Add(qtr.GetLatestAnswer(q.Id));
            }

            return sa;
        }
    }
}