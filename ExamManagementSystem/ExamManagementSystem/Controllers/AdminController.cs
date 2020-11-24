using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Models.ServiceAccess;
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
        readonly AdminRepository _adminRepo;
        readonly UserRepository _userRepo;
        readonly HttpCookie _cookie;

        public AdminController(IRepository<User> userRepo, IRepository<Admin> adminRepo, HttpCookie cookie)
        {
            _userRepo = (UserRepository)userRepo;
            _adminRepo = (AdminRepository)adminRepo;

            _cookie = cookie;
            SessionAdmin = _adminRepo.Get(SessionUser.Id);
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View(SessionAdmin);
        }

        private User SessionUser
        {
            set { System.Web.HttpContext.Current.Session["User"] = value; }
            get { return (User)System.Web.HttpContext.Current.Session["User"]; }
        }

        private Admin SessionAdmin
        {
            set { System.Web.HttpContext.Current.Session["Admin"] = value; }
            get { return (Admin)System.Web.HttpContext.Current.Session["Admin"]; }
        }
    }
}