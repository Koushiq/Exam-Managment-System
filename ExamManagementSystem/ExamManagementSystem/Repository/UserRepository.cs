using ExamManagementSystem.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Repository
{
    public class UserRepository: Repository<User>
    {
        public List<User> GetAllByUserType(string usertype)
        {
            return this.GetAll().Where(s => s.Usertype == usertype && s.Status=="approved").ToList();
        }
    }
}