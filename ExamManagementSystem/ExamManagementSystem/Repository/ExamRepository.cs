using ExamManagementSystem.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Repository
{
    public class ExamRepository:Repository<Exam>
    {
        public void DeleteExam(int examId)
        {
            //Exam exam = this.Get(examId);
            //    foreach (GradeSheet gradeSheet in exam.GradeSheets)
            //    {
            //        new GradeSheetRepository().Delete(gradeSheet.Id);
            //    }

            //    foreach (Question question in exam.Questions)
            //    {
            //        foreach (Option option in question.Options)
            //        {
            //            new OptionRepository().Delete(option.Id);
            //        }
            //        foreach (SubmittedAnswer submitted in question.SubmittedAnswers)
            //        {
            //            new SubmittedAnswerRepository().Delete(submitted.Id);
            //        }
            //        new QuestionRepository().Delete(question.Id);
            //    }
                this.Delete(examId);
            
        }
    }
}