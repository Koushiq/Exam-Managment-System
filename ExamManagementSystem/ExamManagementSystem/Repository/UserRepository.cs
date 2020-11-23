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
        public User GetByUsername(string username)
        {
            return this.context.Users.Where(u => u.Username == username).FirstOrDefault();
        }

        internal bool UsernameExists(string username)
        {
            return this.context.Users.Where(u => u.Username == username).FirstOrDefault() != null;
        }

        internal bool EmailExists(string email)
        {
            return this.context.Users.Where(u => u.Email == email).FirstOrDefault() != null;
        }
    }
}