using System.Web.Mvc;
using System.Web.Routing;

namespace JiYiTunnelSystem.MVCSite.Filters
{
    public class ManagerAuthAttribute: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                if (int.Parse(filterContext.HttpContext.Session["authority"].ToString()) < 1)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
                {
                    {"controller","Home" },
                    {"action","AuthError" }
                });
                }
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
    }
}