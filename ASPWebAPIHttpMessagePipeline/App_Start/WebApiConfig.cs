using ASPWebAPIHttpMessagePipeline.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ASPWebAPIHttpMessagePipeline
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            // add the delegating message handler. 
            config.MessageHandlers.Add(new APIResponseTimeHandler());

            //config.Filters.Add(new RouteTimerFilterAttribute()); 
        }
    }
}
