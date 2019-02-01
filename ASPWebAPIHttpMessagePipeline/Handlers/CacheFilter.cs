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
    public enum ClientCacheControl
    {
        NoCache,
        Private,
        Public,
    }
    public class CacheFilter : ActionFilterAttribute
    {
        private ClientCacheControl CacheControl;
        private double Duration;
        public CacheFilter( ClientCacheControl cacheControl, double duration)
        {
            CacheControl = cacheControl;
            Duration = duration; 
        }
        
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {

            await base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        public override async Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            await base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);

            if ( CacheControl == ClientCacheControl.NoCache )
            {
                actionExecutedContext.Response.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue
                {
                    NoStore = true
                };
                actionExecutedContext.Response.Headers.Pragma.TryParseAdd("no-cache");
                if (!actionExecutedContext.Response.Headers.Date.HasValue)
                    actionExecutedContext.Response.Headers.Date = DateTimeOffset.UtcNow;

                actionExecutedContext.Response.Content.Headers.Expires = actionExecutedContext.Response.Headers.Date;
            }
            else
            {
                actionExecutedContext.Response.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue
                {
                    Public = (CacheControl == ClientCacheControl.Public),
                    Private = (CacheControl == ClientCacheControl.Private),
                    NoCache = false,
                    MaxAge = TimeSpan.FromSeconds(Duration) 
                };
                if (!actionExecutedContext.Response.Headers.Date.HasValue)
                {
                    actionExecutedContext.Response.Headers.Date = DateTimeOffset.UtcNow; 
                }

                actionExecutedContext.Response.Content.Headers.Expires = actionExecutedContext.Response.Headers.Date.Value.AddSeconds(Duration); 
            }
        }

    }
}