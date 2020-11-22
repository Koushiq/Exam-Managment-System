using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Repository
{
    public class CourseRepository:Repository<Cours>
    {
        public void SetValues(Cours course)
        {
            course.CreatedBy = 1;
            course.CreatedAt = DateTime.Now;
        }

        public CourseViewModel GetAllCourses()
        {
            CourseViewModel course = new CourseViewModel();
            List<string> courseNames = new List<String>();

            List<int> couresIds = new List<int>();

            List<Cours> courses = this.GetAll().ToList(); 
            
            foreach(Cours c in courses)
            {
                courseNames.Add(c.CourseName);
                couresIds.Add(c.Id);

            }
            course.courseNames = courseNames;
            course.courseIds = couresIds;

            return course;
        }
    }
}