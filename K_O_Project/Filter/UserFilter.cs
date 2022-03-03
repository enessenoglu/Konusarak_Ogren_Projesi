using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace K_O_Project.Filter
{
    public class UserFilter:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            int? userId = context.HttpContext.Session.GetInt32("Id");
            if (!userId.HasValue)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"action","Login" },
                    {"controller","Login" }
                });
            }
            base.OnActionExecuted(context);
        }
    }
}
