using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamManagementSystem.Controllers
{
    public class EnrollController : Controller
    {
        // GET: Enroll
        private List<Enroll> enrolls;
        private int SectionId;

        public bool CheckExist(Student student)
        {
            foreach(Enroll enroll in this.enrolls)
            {
                if(enroll.StudentId == student.UserId && enroll.SectionId == this.SectionId)
                {
                    return false;
                }
            }
            return true;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddStudent(int id)//section id
        {
            this.SectionId = id;
            EnrollRepository enrollRepository = new EnrollRepository();
            this.enrolls = enrollRepository.GetAll();
            StudentRepository studentRepository = new StudentRepository();
            ViewBag.SecId = id;
            List<Student> students = studentRepository.GetAll();
            List<Student> students2 = new List<Student>();

            foreach(Student student in students)
            {
                if(this.CheckExist(student))
                {
                    students2.Add(student);
                }
            }

            return View(students2);
        }
        [HttpPost]
        public ActionResult AddStudent(Enroll enroll)//section id
        {
            if(ModelState.IsValid)
            {
                EnrollRepository enrollRepository = new EnrollRepository();
                enrollRepository.Insert(enroll);
                return RedirectToAction("Section", "Teacher", new { id = enroll.SectionId });
            }

            StudentRepository studentRepository = new StudentRepository();
            return View(studentRepository.GetAll());
        }

        [HttpGet]
        public ActionResult Delete(int id, int secId)//student id
        {
            EnrollRepository enrollRepository = new EnrollRepository();
            Enroll enroll = enrollRepository.GetEnroll(id, secId);
            return View(enroll);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteEnroll(int id, int secId)//student id
        {
            EnrollRepository enrollRepository = new EnrollRepository();
            Enroll enroll = enrollRepository.GetEnroll(id, secId);
            enrollRepository.Delete(enroll.Id);
            return RedirectToAction("Section", "Teacher", new { id = secId});
        }

    }
}