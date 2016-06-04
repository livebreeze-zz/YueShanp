using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using YueShanp.Helper;

namespace YueShanp.Filter
{
    /// <summary>
    /// if not login, redirect login page
    /// </summary>
    public class WebAuthorizeAttribute : AuthorizeAttribute
    {
        private const string ControllerStr = "controller";
        private const string ActionStr = "action";
        private const string DefaultController = "Account";
        private const string DefaultAction = "Login";

        /// <summary>
        /// check if log in by session
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Session[SessionKey.Account.LoginName] != null;
        }

        /// <summary>
        /// generate next page and redirect
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var urlHelper = new UrlHelper(filterContext.RequestContext);
            var request = filterContext.RequestContext.HttpContext.Request;

            if (AjaxRequestExtensions.IsAjaxRequest(request))
            {
                filterContext.Result = new ContentResult() { Content = "plzlogin" };
            }
            else
            {
                var requestControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                var requestActionName = filterContext.ActionDescriptor.ActionName;

                ////get all querstring
                var parameters = new StringBuilder();
                var querystring = request.QueryString;
                foreach (var key in querystring.AllKeys)
                {
                    parameters.Append(key)
                        .Append("=")
                        .Append(HttpUtility.UrlEncode(querystring[key]))
                        .Append("&");
                }


                var nextpage = string.IsNullOrEmpty(parameters.ToString()) ? request.Url.LocalPath : request.Url.LocalPath + "?" + parameters;

                if (urlHelper.IsLocalUrl(nextpage))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
                    {
                        {ControllerStr, DefaultController},
                        {ActionStr, DefaultAction},
                        {"NextPage", nextpage}
                    });
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
                    {
                        {ControllerStr, DefaultController},
                        {ActionStr, DefaultAction}
                    });
                }
            }
        }

    }
}