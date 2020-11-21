using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Models.UserAccess;
using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamManagementSystem.Controllers
{
    public class UserController : Controller
    {
        readonly UserRepository _user_repo;
        public UserController(UserRepository _user_repo)
        {
            this._user_repo = _user_repo;
        }
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection credentials)
        {
            string username = credentials["Username"];
            string password = credentials["Password"];
            if( !string.IsNullOrEmpty(username) && 
                !string.IsNullOrEmpty(password))
            {
                User user = _user_repo.GetByUsername(username);
                if (user?.Password == password)
                {
                    RegisterUser(user);
                    return RedirectToAction("Index", user.Usertype);
                }
            }
            return Content("not found");
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(FormCollection credentials)
        {
            string username = credentials["Username"];
            string password = credentials["Password"];
            if (
                !string.IsNullOrEmpty(username) &&
                !string.IsNullOrEmpty(password) &&
                _user_repo.GetByUsername(username)?.Password == password)
            {
                return Content("found");
            }
            return Content("not found");
        }

        private void RegisterUser(User user)
        {
            Session["User"] = user;

            HttpCookie cookie = new HttpCookie("userData");
            cookie.Expires = DateTime.Now.AddDays(
                int.Parse(ConfigurationManager.AppSettings["cookieExpiryDays"]));

            cookie["Id"] = user.Id.ToString();
            cookie["User"] = user.Username;
            cookie["FirstName"] = user.Firstname;
            cookie["LastName"] = user.Lastname;
            cookie["Gender"] = user.Gender;
            cookie["Type"] = user.Usertype;
        }
    }
}