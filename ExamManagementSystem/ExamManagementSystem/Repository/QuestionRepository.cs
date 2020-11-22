using ExamManagementSystem.Models.DataAccess;
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
    }
}