using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ExamManagementSystem.Models.MetaDataClasses
{
    public class ExamMetaData
    {
        [Required]
        public int SectionId { get; set; }

        [Required, Display(Name = "Exam Name")]
        public string ExamName { get; set; }

        [Required, Display(Name = "Exam Starting Time")]
        public System.DateTime StartTime { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Please enter valid duration"), Display(Name = "Exam Duration (In Minutes)")]
        public int Duration { get; set; }

        [Required, Range(1, double.MaxValue, ErrorMessage = "Please enter valid mark"), Display(Name = "Exam Marks (Can Be Fraction)")]
        public double TotalMarks { get; set; }

        [Required]
        public System.DateTime CreatedAt { get; set; }
    }
}