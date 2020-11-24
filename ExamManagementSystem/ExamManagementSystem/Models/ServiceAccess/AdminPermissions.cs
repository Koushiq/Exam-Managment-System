using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Models.ServiceAccess
{
    public enum AdminPermissions
    {
        //Permissions' orders must not be changed.
        //Permissions' must only be appended to the end of the existing list.
        //Any existing permission name must not be removed or modified.

        AddAdmin,
        ApproveTeacher,
        ApproveStudent,

        CreateCourse,
        CreateSection,

        AssignTeacherToSection,

        DeleteAdmin,
        DeleteTeacher,
        DeleteStudent,

        ModifyAdminPermission,

        ModifySelfInfo,
        ModifyAdminInfo,
        ModifyStudentInfo,
        ModifyTeacherInfo,

        ViewAllUsers,
        ViewAllStudents,
        ViewAllTeachers,
        ViewAllAdmins
    }
}