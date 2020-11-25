using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Models.ServiceAccess;

namespace ExamManagementSystem.Filters
{
    public class AuthorizeAdmin : ActionFilterAttribute, IAuthenticationFilter
    {
        private int _adminPermissionValue;
        private AdminPermissions _requiredPermission;

        public AuthorizeAdmin(int adminPermissionValue, AdminPermissions requiredPermission)
        {
            _adminPermissionValue = adminPermissionValue;
            _requiredPermission = requiredPermission;
        }

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if(HttpContext.Current.Session["User"] == null)
            {
                filterContext.Result = null;
            }
            else if (!AdminServices.HasPermission(_adminPermissionValue, _requiredPermission))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{{ "controller", "Admin" },
                                          { "action", "Index" }

                                         });
            }
            if (filterContext.Result is null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{{ "controller", "User" },
                                          { "action", "Login" }

                                         });
            }
        }

    }
}