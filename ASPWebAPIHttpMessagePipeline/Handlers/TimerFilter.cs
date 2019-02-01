using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ASPWebAPIHttpMessagePipeline.Handlers
{
    public class TimerFilter : ActionFilterAttribute
    {
        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            Trace.WriteLine("timer filter OnActionExecutedAsync called"); 
            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            Trace.WriteLine("timer filter OnActionExecutingAsync called");
            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }
    }
}