namespace CcsWeb.Helpers
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    public class RequiresSSL : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;
            HttpResponseBase response = filterContext.HttpContext.Response;
            if (!request.IsSecureConnection && !request.IsLocal)
            {
                UriBuilder builder = new UriBuilder(request.Url) {
                    Scheme = Uri.UriSchemeHttps,
                    Port = 0x1bb
                };
                response.Redirect(builder.Uri.ToString());
            }
            base.OnActionExecuting(filterContext);
        }
    }
}

