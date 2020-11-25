using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Models.ViewModel;
using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamManagementSystem.Controllers
{
    public class SectionController : BaseController
    {
        SectionRepository sectionRepo = new SectionRepository();
        TeacherRepository teacherRepo = new TeacherRepository();
        CourseRepository courseRepo = new CourseRepository();
        EnrollRepository enrollRepo = new EnrollRepository();
        // GET: Section
        public ActionResult Index()
        {
            return View(sectionRepo.GetAll().ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            TeacherViewModel teacher = teacherRepo.GetAllTeachers();
           
            CourseViewModel course = courseRepo.GetAllCourses("not deleted");
            ViewBag.teacherUsername = teacher.teacherUsername;
            ViewBag.teacherId = teacher.teacherId;
            ViewBag.courseNames = course.courseNames;
            ViewBag.courseIds = course.courseIds;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Section section)
        {
           if(section.SectionName!=null || section.SectionName=="")
            {
                sectionRepo.SetValues(section);
                sectionRepo.Insert(section);
                return RedirectToAction("Index");
            }
           else
            {
                Session["nosection"] = true;
                return RedirectToAction("Index", "Section");
            }
            
        }

        [HttpGet]
        public ActionResult Delete(int id=-1)
        {
            if(id!=-1)
            {
                return View(sectionRepo.Get(id));
            }
            return RedirectToAction("Index");
            
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            Section section = sectionRepo.Get(id);
            if(section.DeletedBy==null)
            {
                Session["DeleteSection"] =true;
                section.DeletedBy = (int)Session["userId"];
                sectionRepo.SoftDelete(section);
            }
            else
            {
                Session["DeleteSection"] = false;
            }
           
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            
            return View(enrollRepo.GetAll().Where(s=>s.SectionId==id));
        }
    }
}