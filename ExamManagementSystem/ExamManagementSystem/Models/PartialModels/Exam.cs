using ExamManagementSystem.Models.MetaDataClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Models.PartialModels
{
    [MetadataType(typeof(ExamMetaData))]
    public partial class Exam
    {
        [Required, Range(1, int.MaxValue, ErrorMessage = "Please enter valid duration"), Display(Name = "Exam Duration (In Minutes)")]
        public int Duration { get; set; }
    }
}