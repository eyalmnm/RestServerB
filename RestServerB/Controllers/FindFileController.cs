﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestServerB.Models;
using RestServerB.Data_Manager;
using RestServerB.Managers;
using RestServerB.Utils;
using RestServerB.MyConfig;

namespace RestServerB.Controllers
{
    public class FindFileController : ApiController
    {
        // POST: api/FindFile
        public IHttpActionResult Post(FindFilesData value)
        {
            Console.WriteLine("FindFileController");
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
                Console.WriteLine("user not logged in");
                Dictionary<String, object> response = new Dictionary<String, object>();
                response.Add(CsConstatnts.error, ErrorsCode.USER_NOT_LOGGED_IN);
                return Ok(JsonUtils.toJsonStr(response));
            }
            FindFilesPersistance findFilesPersistance = new FindFilesPersistance();

            String fileNumber = value.getFileNumber();
            Console.WriteLine($"Sercing for file: {fileNumber}");
            List<Dictionary<String, object>> dataList = findFilesPersistance.FindFile(fileNumber);
            if (null == dataList)
            {
                Console.WriteLine($"file {fileNumber} not found");
                Dictionary<String, object> response = new Dictionary<String, object>();
                response.Add(CsConstatnts.error, ErrorsCode.FILE_NOT_FOUND);
                return Ok(JsonUtils.toJsonStr(response));
            }
            Console.WriteLine($"File {fileNumber} found");
            return Ok(JsonUtils.toJsonStr(dataList));
        }
    }
}
