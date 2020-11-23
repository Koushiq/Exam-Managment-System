using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        StudentRepository studentRepo = new StudentRepository();
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
       
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(studentRepo.Get(id));
        }
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            studentRepo.Update(student);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(studentRepo.Get(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            //studentRepo.Delete(id); // needs fix
            return RedirectToAction("Index");

        }
    }
}