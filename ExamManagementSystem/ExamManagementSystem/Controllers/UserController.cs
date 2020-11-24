using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Models.ServiceAccess;
using ExamManagementSystem.Models.UserServices;
using ExamManagementSystem.Repository;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ExamManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _user_repo;
        private readonly HttpCookie _cookie;

        public UserController(
            UserRepository user_repo,
            HttpCookie cookie)
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
            string password = CryptographicServices.MD5Hash(credentials["Password"]);

            if( !string.IsNullOrEmpty(username) && 
                !string.IsNullOrEmpty(password))
            {
                User user = _user_repo.GetByUsername(username);
                if (user?.Password == password)
                {
                    RegisterLogin(user);
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
                OperationalServices.AddUserToDatabase(userData);
                return RedirectToAction("Login", 
                    new { message = $"Welcome, {userData.Firstname} Signup Successful!"});
            }
            return View(userData);
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return PartialView("ForgotPasswordUnameInput");
        }

        [HttpPost]
        public ActionResult ForgotPassword(FormCollection formData)
        {
            ViewBag.usernameOrEmail = formData["usernameOrEmail"];
            string verificationCode = formData["verificationCode"];
            if (string.IsNullOrWhiteSpace(ViewBag.usernameOrEmail))
            {
                ViewBag.Message = "Must enter a valid username or email";
                return PartialView("ForgotPasswordUnameInput");
            }
            User user = _user_repo.GetUserByEmailOrUsername(ViewBag.usernameOrEmail, ViewBag.usernameOrEmail);
            if (user == null)
            {
                ViewBag.Message = "User not found in our system";
                return PartialView("ForgotPasswordUnameInput");
            }

            SessionUser = user;
            Session["Purpose"] = "forgotPasswordEmailVerification";

            OperationalServices.GenerateAndSendVerificationCode(Session);

            ViewBag.Title = "Forgot password email verification";
            ViewBag.Username = user.Username;
            ViewBag.Email = user.Email;
            ViewBag.Purpose = "forgotPasswordEmailVerification";
            return PartialView("EmailVerificationCodeInput", formData);
        }

        public ActionResult ValidateUserStatus()
        {
            if(SessionUser.Status == "unverified_email")
            {
                return VerifyEmail();
            }
            else if(SessionUser.Status == "awaiting_approval")
            {
                return ShowUserApprovalMessage();
            }
            return RedirectToAction("Index", _cookie["Type"]);
        }

        private ActionResult ShowUserApprovalMessage()
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
            userData.Status = "unverified_email";

            return true;
        }

        [HttpGet]
        public ActionResult VerifyEmail()
        {
            Session["Purpose"] = "User Email Verification";
            OperationalServices.GenerateAndSendVerificationCode(Session);
            ViewBag.Title = "New user email verification";
            ViewBag.Username = SessionUser.Username;
            ViewBag.Email = SessionUser.Email;
            ViewBag.ActionName = "VerifyEmail";
            ViewBag.Purpose = "newUserVerification";
            return PartialView("EmailVerificationCodeInput", new FormCollection());
        }

        [HttpPost]
        public ActionResult VerifyEmail(FormCollection formData)
        {
            if((int?)Session["emailVerificationAttemptCount"] >= int.Parse(ConfigurationManager.AppSettings["maximumVerificationAttempts"]))
            {
                ViewBag.Message = "Too many attempts. A new verification code is sent.";
                ClearVerificationCode();

                OperationalServices.GenerateAndSendVerificationCode(Session);
            }
            else if (Session["emailVerificationCode"]?.ToString() == formData["verificationCode"])
            {
                return PerformPostEmailVerificationTasks(formData["purpose"], formData["Username"]);
            }
            else
            {
                Session["emailVerificationAttemptCount"] = (int?)Session["emailVerificationAttemptCount"] + 1;
                ViewBag.Message = "Invalid verification code. Try again";
            }
            return PartialView("EmailVerificationCodeInput", formData);
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            if(SessionUser == null || (bool?)Session["EmailVerified"] != true)
            {
                ModelState.AddModelError("Email", "Email not verified");
            }

            return PartialView("ResetPassword", SessionUser);
        }

        [HttpPost]
        public ActionResult ResetPassword(User user)
        {
            if(string.IsNullOrWhiteSpace(user.Password))
            {
                ModelState.AddModelError("Password", "Password can't be empty");
                return PartialView("ResetPassword", user);
            }

            if(string.IsNullOrWhiteSpace(user.ConfirmPassword))
            {
                ModelState.AddModelError("ConfirmPassword", "Confirm Password can't be empty");
                return PartialView("ResetPassword", user);
            }

            if ((User)Session["User"] == null || (bool?)Session["EmailVerified"] != true)
            {
                ModelState.AddModelError("Email", "Email not verified");
                return PartialView("ResetPassword", user);
            }

            if(user.Password != user.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Password doesn't match");
                return PartialView("ResetPassword", user);
            }

            SessionUser.Password = CryptographicServices.MD5Hash(user.Password);
            _user_repo.AddOrUpdate(SessionUser);

            Session.Remove("User");
            return RedirectToAction("Login", new { message = "Password reset successful." });
        }

        private ActionResult PerformPostEmailVerificationTasks(string purpose, string username = null)
        {
            ActionResult actionResult = null;
            if(purpose == "newUserVerification")
            {
                User user = SessionUser;
                user.Status = "awaiting_approval";
                _user_repo.AddOrUpdate(user);
                actionResult = ValidateUserStatus();
            }
            else if(purpose == "forgotPasswordEmailVerification")
            {
                ViewBag.Title = "Reset Password";
                ViewBag.Username = username;
                Session["EmailVerified"] = true;
                actionResult = ResetPassword();
            }
            ClearVerificationCode();
            return actionResult;
        }

        private void ClearVerificationCode()
        {
            Session.Remove("emailVerificationCode");
            Session.Remove("emailVerificationAttemptCount");
            Session.Remove("Purpose");
        }

        private void RegisterLogin(User user)
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
            set { System.Web.HttpContext.Current.Session["User"] = value; }
            get { return (User)System.Web.HttpContext.Current.Session["User"]; }
        }
    }
}