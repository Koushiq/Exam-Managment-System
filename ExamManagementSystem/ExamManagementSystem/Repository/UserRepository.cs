using ExamManagementSystem.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Repository
{
    public class UserRepository: Repository<User>
    {
        internal User GetByUsername(string username)
        {
            return this.context.Users.Where(u => u.Username == username).FirstOrDefault();
        }

        internal User GetUserByEmail(string email)
        {
            return this.context.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        internal User GetUserByEmailOrUsername(string email, string username)
        {
            return this.context.Users.Where(u => (u.Email == email || u.Username == username)).FirstOrDefault();
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