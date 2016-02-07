using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CooperatorsDirect.Models;

namespace CooperatorsDirect.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public Roles[] UserRoles { get; set; }

        public CustomAuthorizeAttribute(params Roles[] userRoles)
        {
            UserRoles = userRoles;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (SessionPersiter.User == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "Index" }));
            }
            else
            {
                UserModel userMod = new UserModel();

                CustomPrincipal customPrinc = new CustomPrincipal(userMod.Find(SessionPersiter.User.Email));

                if (!customPrinc.IsInRole(UserRoles))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
                }

            }
        }
    }
}