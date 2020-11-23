using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Models.ServiceAccess
{
    public enum AdminPermissions
    {
        AddAdmins = 1,
        ApproveTeachers,
        ApproveUsers,

        CreateCourses,
        CreateSections,

        AssignTeacherToSections,

        DeleteTeachers,
        DeleteStudents,
        DeleteAdmin,

        ModifyAdminPermissions
    }
}