using ExamManagementSystem.Models.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem
{
    public static class UserConfig
    {
        internal static void RegisterUserDataTypes()
        {
            UserTypes.UserDataType[UserTypes.Student] = typeof(Models.DataAccess.Student);
            UserTypes.UserDataType[UserTypes.Teacher] = typeof(Models.DataAccess.Teacher);
            UserTypes.UserDataType[UserTypes.Admin] = typeof(Models.DataAccess.Admin);
        }
    }
}