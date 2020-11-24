using ExamManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Models.UserServices
{
    public static class UserTypes
    {
        public static string Student { get { return "student"; } }
        public static string Teacher { get { return "teacher"; } }
        public static string Admin { get { return "admin"; } }

        public static Dictionary<string, Type> UserDataType = new Dictionary<string, Type>();
    }
}