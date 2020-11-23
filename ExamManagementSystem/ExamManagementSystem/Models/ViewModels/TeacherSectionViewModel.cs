using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExamManagementSystem.Models.DataAccess;

namespace ExamManagementSystem.Models.ViewModels
{
    public class TeacherSectionViewModel
    {
        public List<Enroll> Stuents { get; set; }
        public List<Exam> UpcomingExams { get; set; }
        public List<Exam> OnGoingExams { get; set; }
        public int Id { get; set; }
        public string SectionName { get; set; }
        public string CourseName { get; set; }
    }
}