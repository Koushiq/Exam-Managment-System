using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamManagementSystem.Controllers
{
    public class UserController : Controller
    {
        UserRepository user = new UserRepository();
        // GET: User
        [HttpGet]
        public ActionResult Index()
        {
            return View(user.GetAll());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User u)
        {
            user.Insert(u);
            return RedirectToAction("Index");
        }
    }
}