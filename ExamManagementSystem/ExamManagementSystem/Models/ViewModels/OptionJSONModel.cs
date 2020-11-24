using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Models.ViewModels
{
    public class OptionJSON
    {
        public int? Id { get; set; }
        public string OptionText { get; set; }
        public int OptionId { get; set; }
    }
    public class OptionJSONModel
    {
        public List<OptionJSON> Options { get; set; }
    }
}