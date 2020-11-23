using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        AdminRepository adminRepo = new AdminRepository();
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Index()
        {
            int id = (int)Session["userId"];
            Admin admin = adminRepo.Get(id);
            if(admin.PermissionBin>=0) // validate permission bin too see admin list
            {
                return View(adminRepo.GetAll().ToList());
            }
            else
            {
                Session["permissionError"] = true;
            }
            return RedirectToAction("Home");

            
        }
    }
}