using ExamManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Models.UserServices
{
    public static class UserTypes
    {
        public const string Student = "student";
        public const string Teacher = "teacher";
        public const string Admin = "admin";

        public static Dictionary<string, Type> UserDataType = new Dictionary<string, Type>();
    }
}