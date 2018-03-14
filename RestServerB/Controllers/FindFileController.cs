using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestServerB.Models;
using RestServerB.Data_Manager;
using RestServerB.Managers;
using RestServerB.Utils;

namespace RestServerB.Controllers
{
    public class FindFileController : ApiController
    {
        // POST: api/FindFile
        public IHttpActionResult Post(FindFilesData value)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("uuid");
            String uuid = headerValues.FirstOrDefault();

            if (false == ConnectionsManager.IsExist(uuid))
            {
                Dictionary<String, object> response = new Dictionary<String, object>();
                response.Add("error", "user not found");
                return Ok(JsonUtils.toJsonStr(response));
            }
            FindFilesPersistance findFilesPersistance = new FindFilesPersistance();

            String fileNumber = value.getFileNumber();
            Dictionary<String, object> fileData = findFilesPersistance.FindFile(fileNumber);

            return Ok(JsonUtils.toJsonStr(fileData));
        }
    }
}
