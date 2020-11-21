using ExamManagementSystem.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Repository
{
    public class UserRepository: Repository<User>
    {
        public User GetByUsername(string username)
        {
            return this.context.Users.Where(u => u.Username == username).FirstOrDefault();
        }
    }
}