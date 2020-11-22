using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Repository
{
    public class TeacherRepository:Repository<Teacher>
    {

        public void SetValues()
        {
            
        }
        public TeacherViewModel GetAllTeachers()
        {
            TeacherViewModel teachersInfo = new TeacherViewModel();
            List<Teacher> teachers = this.GetAll().ToList();
            List<string> teacherUsername = new List<string>();
            List<int> teacherId = new List<int>();

            foreach(Teacher t in teachers)
            {
                teacherUsername.Add(t.User.Username);
                teacherId.Add(t.UserId);
            }
            teachersInfo.teacherUsername = teacherUsername;
            teachersInfo.teacherId = teacherId;

            return teachersInfo;
            
        }
    }
}