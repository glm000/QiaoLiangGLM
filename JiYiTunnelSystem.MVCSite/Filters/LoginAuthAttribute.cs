using System.Web.Mvc;
using System.Web.Routing;

namespace JiYiTunnelSystem.MVCSite.Filters
{
    public class LoginAuthAttribute:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if(filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute),true)||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }

            if(filterContext.HttpContext.Request.Cookies["userId"]!=null&&
                filterContext.HttpContext.Session["userId"] == null)
            {
                filterContext.HttpContext.Session["userId"] = filterContext.HttpContext.Request.Cookies["userId"].Value;
                try
                {
                    filterContext.HttpContext.Session["authority"] = filterContext.HttpContext.Request.Cookies["authority"].Value;
                }
                catch
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
                    {
                        {"controller","User" },
                        {"action","Login" }
                    });
                }
            }

            if (!(filterContext.HttpContext.Request.Cookies["userId"]!=null||
                filterContext.HttpContext.Session["userId"] != null))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
                {
                    {"controller","User" },
                    {"action","Login" }
                });
            }
        }
    }
}