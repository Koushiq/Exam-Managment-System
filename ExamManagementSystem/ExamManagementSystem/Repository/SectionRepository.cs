using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Repository
{
    public class SectionRepository:Repository<Section>
    {
        public List<Section> GetSectionListForTeacher(int teacherId)
        {
            return this.GetAll().Where(x => x.TeacherId == teacherId).ToList();
        }

        public TeacherSectionViewModel GetSectionForTeacher(int sectionId)
        {
            Section section = this.Get(sectionId);
            TeacherSectionViewModel teacherSection = new TeacherSectionViewModel()
            {
                Id = section.Id,
                SectionName = section.SectionName,
                CourseName = section.Cours.CourseName,
                Stuents = section.Enrolls.ToList(),
                OnGoingExams = section.Exams.Where(exam => exam.StartTime < DateTime.Now && exam.StartTime.AddMinutes(exam.Duration) >= DateTime.Now).ToList(),
                UpcomingExams = section.Exams.Where(exam => exam.StartTime >= DateTime.Now).ToList()
                
            };
            return teacherSection;
        }
    }
}