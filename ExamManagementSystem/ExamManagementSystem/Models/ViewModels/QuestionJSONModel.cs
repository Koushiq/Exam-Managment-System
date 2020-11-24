using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Models.ViewModels
{
    public class QuestionJSONModel
    {
        public int ExamId { get; set; }
        public List<int> CorrectAnswers { get; set; }
        public double Marks { get; set; }
        public string Statement { get; set; }
        public string Type { get; set; }
        public string AnswerText { get; set; }

    }
}