using ExamManagementSystem.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Repository
{
    public class OptionRepository:Repository<Option>
    {
        public List<Option> GetOptionsByQuestionId(int qid)
        {
            return this.GetAll().Where(x => x.QuestionId == qid).ToList();
        }
    }
}