using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreAngularSignalR
{

    public class ValidateMimeMultipartContentFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!IsMultipartContentType(context.HttpContext.Request.ContentType!))
            {
                context.Result = new StatusCodeResult(415);
                return;
            }

            base.OnActionExecuting(context);
        }

        private static bool IsMultipartContentType(string contentType)
        {
            return !string.IsNullOrEmpty(contentType) && contentType.Contains("multipart/", StringComparison.OrdinalIgnoreCase);
        }
    }
}
