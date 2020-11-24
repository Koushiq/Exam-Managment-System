using ExamManagementSystem.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Repository
{
    public class SubmittedAnswerRepository:Repository<SubmittedAnswer>
    {
        public List<SubmittedAnswer> GetAllSAByQuestionId(int qid)
        {
            return this.GetAll().Where(x => x.QuestionId == qid).ToList();
        }

        public List<SubmittedAnswer> GetAllSAByExamId(int eid)
        {
            return this.GetAll().Where(x => x.Question.ExamId == eid).OrderByDescending(x => x.AnswerText).ToList();
        }

        public SubmittedAnswer GetLatestAnswer(SubmittedAnswer sa)
        {
            int at = this.GetAllSAByQuestionId(sa.QuestionId).Max(x => x.AttemptTime);
            return this.GetAllSAByQuestionId(sa.QuestionId).Where(x => x.AttemptTime == at).FirstOrDefault();
        }
    }
}