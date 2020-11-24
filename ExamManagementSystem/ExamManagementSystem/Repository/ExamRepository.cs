using ExamManagementSystem.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Repository
{
    public class ExamRepository : Repository<Exam>
    {
        public List<Exam> GetExamsBySectionId(int sid)
        {
            return this.GetAll().Where(x => x.SectionId == sid).ToList();
        }

        public List<Exam> GetFutureExamsBySectionId(int sid)
        {
            return this.GetAll().Where(x => x.SectionId == sid && x.StartTime.AddMinutes(x.Duration) >= DateTime.Now).ToList();
        }

        public List<Exam> GetPastExamsBySectionId(int sid)
        {
            return this.GetAll().Where(x => x.SectionId == sid && x.StartTime.AddMinutes(x.Duration) <= DateTime.Now).ToList();
        }
    }
}