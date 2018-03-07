using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestServerB.Controllers
{
    public class DummiController : ApiController
    {
        // GET: api/Dummi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Dummi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Dummi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Dummi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Dummi/5
        public void Delete(int id)
        {
        }
    }
}
