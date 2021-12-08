using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ITSVoice.Helper
{
    public class AuthAttribute:AuthorizeAttribute
    {
        private dynamic db = DataBaseHelper.GetConnection();
        private readonly string[] allowedroles;
        public AuthAttribute(params string[] roles) 
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            int? userId = Convert.ToInt32(httpContext.Session["userId"]);
            if (userId != null) 
            {
                var userRole = db.Users.Find(db.Users.Id == userId);
                if (userRole != null) 
                {
                    foreach (var v in allowedroles) 
                    {
                        if (v == userRole.UserType) 
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Account" },
                    { "action", "Index" }
               });
        }
    }
}