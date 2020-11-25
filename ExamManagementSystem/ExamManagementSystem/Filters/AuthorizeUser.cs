using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Models.UserServices;

namespace ExamManagementSystem.Filters
{
    public class AuthorizeUser : ActionFilterAttribute, IAuthenticationFilter
    {
        string _authorizedUserType;
        public AuthorizeUser(string authorizedUserType)
        {
            _authorizedUserType = authorizedUserType;
        }

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if(((User)System.Web.HttpContext.Current.Session["User"]).Usertype != _authorizedUserType)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if(filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        { "controller", "User" },
                        { "action", "Login" }
                    });
            }
        }

    }
}