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
            section.CreatedBy = (int)HttpContext.Current.Session["userId"]; 
        }


        public void SetValues(int id)
        {
            Section section = this.Get(id);
            section.DeletedAt = DateTime.Now;
            section.DeletedBy = (int)HttpContext.Current.Session["userId"]; // assign admin user validate using id

        }

        public override void SoftDelete(Section entity)
        {
            base.SoftDelete(entity);
            entity.DeletedAt = base.time;
            base.Update(entity);
        }




    }
}