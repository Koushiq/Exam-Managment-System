using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Models.ServiceAccess;
using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ExamManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _user_repo;
        private HttpCookie _cookie;

        public UserController(UserRepository user_repo, HttpCookie cookie)
        {
            this._user_repo = user_repo;
            this._cookie = cookie;
        }
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string message = "")
        {
            ViewBag.Message = message;
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
                    return ValidateUserStatus();
                }
                else
                {
                    ViewBag.ErrorMessage = "Incorrect username or password!";
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User userData)
        {
            bool isValid = ValidateUserSignup(userData);

            if (isValid)
            {
                userData.Teacher = new Teacher();
                userData.Student = new Student();
                userData.Admin = new Admin();
                _user_repo.Insert(userData);
                return RedirectToAction("Login", 
                    new { message = $"Welcome, {userData.Firstname} Signup Successful!"});
            }
            return View(userData);
        }

        public ActionResult ValidateUserStatus()
        {
            if(SessionUser.Status == "unverified_email")
            {
                return VerifyEmail();
            }
            else if(SessionUser.Status == "awaiting_approval")
            {
                return UserApprovalMessage();
            }
            return RedirectToAction("Index", _cookie["Type"]);
        }

        private ActionResult UserApprovalMessage()
        {
            return Content("Need admin approval");
        }

        private bool ValidateUserSignup(User userData)
        {
            if (userData.Password != userData.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Password doesn't match");
            }
            if(_user_repo.UsernameExists(userData.Username))
            {
                ModelState.AddModelError("Username", "Username is already taken");
            }
            if (_user_repo.EmailExists(userData.Email))
            {
                ModelState.AddModelError("Email", "Email is already registered");
            }

            if (!ModelState.IsValid) return false;

            userData.CreatedAt = DateTime.Now;
            userData.Status = "valid";      // it will be "unverified_email". Fix it after fixing the Update() method of the repository

            return true;
        }

        [HttpGet]
        public ActionResult VerifyEmail()
        {
            GenerateAndSendVerificationCode();
            return View("EmailVerification");
        }

        [HttpPost]
        public ActionResult VerifyEmail(string verificationCode)
        {
            if((int?)Session["emailVerificationAttemptCount"] >= int.Parse(ConfigurationManager.AppSettings["maximumVerificationAttempts"]))
            {
                ViewBag.Message = "Too many attempts. A new verification code is sent.";
                ClearVerificationCode();
                GenerateAndSendVerificationCode();
            }
            else if (Session["emailVerificationCode"]?.ToString() == verificationCode)
            {
                User user = SessionUser;
                user.Status = "awaiting_approval";
                _user_repo.AddOrUpdate(user);
                ClearVerificationCode();
                return ValidateUserStatus();
            }
            else
            {
                Session["emailVerificationAttemptCount"] = (int?)Session["emailVerificationAttemptCount"] + 1;
                ViewBag.Message = "Invalid verification code. Try again";
            }
            return View("EmailVerification");
        }

        private void ClearVerificationCode()
        {
            Session.Remove("emailVerificationCode");
            Session.Remove("emailVerificationAttemptCount");
        }

        private void GenerateAndSendVerificationCode()
        {
            if (Session["emailVerificationCode"] == null)
            {
                Session["emailVerificationCode"] = UserServices.GenerateRandomNumericCode();
                Session["emailVerificationAttemptCount"] = 0;
            }

            UserServices.SendVerificationCode(
                _cookie["FirstName"],
                _cookie["Email"],
                Session["emailVerificationCode"].ToString(),
                "Email Verification");
        }

        private void RegisterUser(User user)
        {
            SessionUser = user;

            _cookie.Expires = DateTime.Now.AddDays(
                int.Parse(ConfigurationManager.AppSettings["cookieExpiryDays"]));

            _cookie["Id"] = user.Id.ToString();
            _cookie["User"] = user.Username;
            _cookie["FirstName"] = user.Firstname;
            _cookie["LastName"] = user.Lastname;
            _cookie["Gender"] = user.Gender;
            _cookie["Email"] = user.Email;
            _cookie["Type"] = user.Usertype;
        }

        private User SessionUser
        {
            set { Session["User"] = value; }
            get { return (User)Session["User"]; }
        }
    }
}