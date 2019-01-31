using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASPWebAPIHttpMessagePipeline.Handlers
{
    public class APIResponseTimeHandler : DelegatingHandler
    {
        const string headerName = "x-api-timer"; 
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Incoming requests handling
            Stopwatch watch = new Stopwatch();
            watch.Start(); 


            var response = await base.SendAsync(request, cancellationToken);

            // Outgoing response handling

            watch.Stop();
            response.Headers.Add(headerName, watch.ElapsedMilliseconds + " ms");


            return response; 
        }
    }
}