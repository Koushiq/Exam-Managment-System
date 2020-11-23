using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        TeacherRepository teacherRepo = new TeacherRepository();
        UserRepository userRepo = new UserRepository();
        StudentRepository studentRepo = new StudentRepository();
        // GET: Teacher
        public ActionResult Index()
        {
            return View(teacherRepo.GetAll().ToList());
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(teacherRepo.Get(id));
        }
        [HttpPost]
        public ActionResult Edit(Teacher teacher)
        {
            teacherRepo.Update(teacher);
            return RedirectToAction("Index");
        }
    }
}