using ExamManagementSystem.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Repository
{
    public class EnrollRepository : Repository<Enroll>
    {
        public List<Enroll> GetAllByStudentId(int id)
        {
            return this.GetAll().Where(x => x.StudentId == id).ToList();
        }
    }
}