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
            course.CreatedAt = DateTime.Now;
        }

        public CourseViewModel GetAllCourses(string term)
        {
            CourseViewModel course = new CourseViewModel();
            List<string> courseNames = new List<String>();
            List<Cours> courses;
            List<int> couresIds = new List<int>();
            if(term=="not deleted")
            {
                courses = this.GetAll().Where(s=>s.DeletedBy==null).ToList();
            }
            else
            {
                courses = this.GetAll().Where(s => s.DeletedBy != null).ToList();
            }
            
            
            foreach(Cours c in courses)
            {
                courseNames.Add(c.CourseName);
                couresIds.Add(c.Id);

            }
            course.courseNames = courseNames;
            course.courseIds = couresIds;

            return course;
        }

        public override void SoftDelete(Cours entity)
        {
           
            base.SoftDelete(entity);
            entity.DeletedAt = base.time;
            base.Update(entity);

        }
    }
}