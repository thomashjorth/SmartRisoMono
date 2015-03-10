using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DataAggregator.Controllers
{
    public class RealtimeController : ApiController
    {
        // GET: api/Realtime/5
        public string Get(string id)
        {
            return WS.DownloadXML(id);
        }

        // POST: api/Realtime
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Realtime/5
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}
