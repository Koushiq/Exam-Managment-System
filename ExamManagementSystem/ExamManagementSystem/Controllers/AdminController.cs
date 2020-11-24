using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Models.FileGenerator;
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
        UserRepository userRepo = new UserRepository();
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
        [HttpGet]
        //[Obsolete]
        public ActionResult Details(int id)
        {
            /*string[] buff = { "Mystr", "Mystr2", "Mystr3" };
            PDFReport pdf = new PDFReport("Dummyfile.pdf","arial",10,20);
            pdf.Generate(buff);
            return RedirectToAction("Index");*/
            //userRepo.GetAll().Where(s=>s.Id==idx && s.)
            return View();
        }

        [HttpGet]
        public ActionResult Activity(int id)
        {
            Admin admin = adminRepo.Get((int)Session["userId"]);
            if(admin.PermissionBin>0)
            {
                return RedirectToAction("Activity", "User", new { Id=id }); 
            }
            else
            {
                return RedirectToAction("Home");
            }
            
        }


        public ActionResult CoursesAdded(int id)
        {

            Admin admin = adminRepo.Get((int)Session["userId"]);
            if (admin.PermissionBin > 0)
            {
                return RedirectToAction("CoursesAdded", "Course", new { Id = id });
            }
            else
            {
                return RedirectToAction("Home");
            }
        }

        public ActionResult CoursesSoftDeleted(int id)
        {

            Admin admin = adminRepo.Get((int)Session["userId"]);
            if (admin.PermissionBin > 0)
            {
                return RedirectToAction("CoursesSoftDeleted", "Course", new { Id = id });
            }
            else
            {
                return RedirectToAction("Home");
            }
        }
        public ActionResult SectionsAdded(int id)
        {

            Admin admin = adminRepo.Get((int)Session["userId"]);
            if (admin.PermissionBin > 0)
            {
                return RedirectToAction("SectionsAdded", "Section", new { Id = id });
            }
            else
            {
                return RedirectToAction("Home");
            }
        }

        public ActionResult SectionsSoftDeleted(int id)
        {

            Admin admin = adminRepo.Get((int)Session["userId"]);
            if (admin.PermissionBin > 0)
            {
                return RedirectToAction("SectionsSoftDeleted", "Section", new { Id = id });
            }
            else
            {
                return RedirectToAction("Home");
            }
        }

    }
}