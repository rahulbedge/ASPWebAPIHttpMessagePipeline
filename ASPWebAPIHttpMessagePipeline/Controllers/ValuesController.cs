using ASPWebAPIHttpMessagePipeline.Handlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;

namespace ASPWebAPIHttpMessagePipeline.Controllers
{
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        // GET: api/Values
        [Route("")]
        [CacheFilter(ClientCacheControl.Private, 10)]
        [TimerFilter]
        public IEnumerable<string> Get()
        {
            Trace.WriteLine ("Method executed"); 
            return new string[] { "value1", "value2" , DateTime.Now.ToString() };
        }

        // GET: api/Values/5
        [Route("{id}")]
        public string Get(int id)
        {
            return "value " + id.ToString();
        }

        // POST: api/Values
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Values/5
        public void Delete(int id)
        {
        }
    }
}
