using ExamManagementSystem.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Repository
{
    public class SectionRepository:Repository<Section>
    {
        
        public void SetValues(Section section)
        {
            //section.CourseId = 5; //
            section.CreatedAt = DateTime.Now;
            section.CreatedBy = 2;  // insert admin id using session
        }

       

     

        
    }
}