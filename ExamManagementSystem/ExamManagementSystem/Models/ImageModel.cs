using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Models
{
    public class ImageModel
    {
        public string ImagePath { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}