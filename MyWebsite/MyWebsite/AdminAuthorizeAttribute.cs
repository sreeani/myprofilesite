using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebsite
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AdminAuthorizeAttribute: AuthorizeAttribute
    { 
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            using (var ctx = new StudentEntities())
            {
                var email = httpContext.User.Identity.Name;
               
                var isUserAuthenticatedAndAnAdmin = ctx.People.Any(p => p.Email == email
                                                    && p.IsAdmin && httpContext.User.Identity.IsAuthenticated);
                if (isUserAuthenticatedAndAnAdmin) return true;

                return base.AuthorizeCore(httpContext);
            }
        }
    }
}