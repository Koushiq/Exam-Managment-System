using ExamManagementSystem.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Repository
{
    public class SubmittedAnswerRepository : Repository<SubmittedAnswer>
    {
        public List<SubmittedAnswer> GetAllSAByQuestionId(int qid)
        {
            return this.GetAll().Where(x => x.QuestionId == qid).ToList();
        }
    }
}