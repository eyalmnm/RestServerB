using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestServerB.Models;
using RestServerB.Data_Manager;
using RestServerB.MyConfig;
using RestServerB.Managers;
using RestServerB.Utils;

namespace RestServerB.Controllers
{
    public class GetFilesController : ApiController
    {
        // POST: api/GetFiles
        public IHttpActionResult Post(GetFiles value)
        {
            Dictionary<String, object> response = new Dictionary<String, object>();

            String directory = value.getDirectory();

            GetFilesPersistance persistance = new GetFilesPersistance(directory);

            String uuid = "";

            IEnumerable<string> headerValues;
            var nameFilter = string.Empty;
            if (Request.Headers.TryGetValues(CsConstatnts.uuid, out headerValues))
            {
                nameFilter = headerValues.FirstOrDefault();
                uuid = nameFilter;
            }
            if (false == ConnectionsManager.IsExist(uuid))
            {
                response.Add(CsConstatnts.error, ErrorsCode.USER_NOT_LOGGED_IN);
                return Ok(JsonUtils.toJsonStr(response));
            }

            String[] filesList = persistance.getFileList();
            if (null == filesList)
            {
                response.Add(CsConstatnts.error, ErrorsCode.TARGET_DIRCTORY_NOT_FOUND);
                return Ok(JsonUtils.toJsonStr(response));
            } else
            {
                response.Add(CsConstatnts.filesList, StringUtils.StringArray2String(filesList))
            }
        }        
    }
}
