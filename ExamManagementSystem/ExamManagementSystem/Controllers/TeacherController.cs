using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamManagementSystem.Controllers
{
    public class TeacherController : BaseController
    {
        TeacherRepository teacherRepo = new TeacherRepository();
        UserRepository userRepo = new UserRepository();
        StudentRepository studentRepo = new StudentRepository();
        // GET: Teacher
        public ActionResult Index()
        {
            if((string)Session["userType"]=="admin")
            {
                return View(teacherRepo.GetAll().ToList());
            }
            else if((string)Session["userType"] == "teacher")
            {
                return RedirectToAction("Home","Teacher");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
            
           
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