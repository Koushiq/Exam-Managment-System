using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Models.ServiceAccess;
using ExamManagementSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Repository
{
    public class QuestionRepository:Repository<Question>
    {
        public List<Question> GetQuestionsByExamId(int eid)
        {
            return this.GetAll().Where(x => x.ExamId == eid).ToList();
        }
        public int InsertFromJSON(QuestionJSONModel questionJSON)
        {
            Question question = new Question();

            question.Marks = questionJSON.Marks;
            question.ExamId = questionJSON.ExamId;
            question.Statement = questionJSON.Statement;
            question.Type = questionJSON.Type;

            if(question.Type == "Text")
            {
                question.AnswerText = questionJSON.AnswerText;
            }
            else if(question.Type == "MCQ" || question.Type == "Check")
            {
                question.CorrectAnswerBin = BitwiseServices.GetIntegerValue(questionJSON.CorrectAnswers);
            }

            this.Insert(question);

            return question.Id;
        }

        public void UpdateFromJSON(QuestionJSONModel questionJSON, int questionId)
        {
            //Question question = new Question();
            Question question = this.Get(questionId);
            //question.Id = questionId;
            question.Marks = questionJSON.Marks;
            //question.ExamId = questionJSON.ExamId;
            question.Statement = questionJSON.Statement;
            question.Type = questionJSON.Type;

            if (question.Type == "Text")
            {
                question.AnswerText = questionJSON.AnswerText;
            }
            else if (question.Type == "MCQ" || question.Type == "Check")
            {
                question.CorrectAnswerBin = BitwiseServices.GetIntegerValue(questionJSON.CorrectAnswers);
            }

            this.Update(question);

        }
    }
}