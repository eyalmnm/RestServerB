using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestServerB.Managers;
using RestServerB.Models;
using RestServerB.MyConfig;
using RestServerB.Utils;
using RestServerB.Data_Manager;

namespace RestServerB.Controllers
{
    public class CreateFilesController : ApiController
    {
        // GET: api/CreateFiles
        public IHttpActionResult Get()
        {
            Console.WriteLine("CreateFilesController");
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
                Dictionary<String, object> errResponse = new Dictionary<String, object>();
                errResponse.Add(CsConstatnts.error, ErrorsCode.USER_NOT_LOGGED_IN);
                return Ok(JsonUtils.toJsonStr(errResponse));
            }
            CreateFilesPersistance createFilesPersistance = new CreateFilesPersistance();
            String recId = createFilesPersistance.getFileRecord();
            Dictionary<String, object> response = new Dictionary<String, object>();
            response.Add(CsConstatnts.fileNumber, recId);
            Console.WriteLine($"File {recId} allocated");
            return Ok(JsonUtils.toJsonStr(response));
        }

        // POST: api/CreateFiles
        public IHttpActionResult Post(CreateFileData value)
        {
            Console.WriteLine("CreateFilesController");
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
                Dictionary<String, object> errResponse = new Dictionary<String, object>();
                errResponse.Add(CsConstatnts.error, ErrorsCode.USER_NOT_LOGGED_IN);
                return Ok(JsonUtils.toJsonStr(errResponse));
            }
            CreateFilesPersistance createFilesPersistance = new CreateFilesPersistance();

            String fileNumber = value.getFileNumber();
            long creationDate = value.getCreationDate();
            String insuredName = value.getInsuredName();
            String customer = value.getCustomer();
            String employee = value.getEmployee();
            String suitNumber = value.getSuitNumber();
            String fileStatus = value.getFileStatus();
            Console.WriteLine($"Sercing for file: {fileNumber} {creationDate} {insuredName} {customer} {employee} {suitNumber} {fileStatus}");
            bool creatioResult = createFilesPersistance.addNewRecord(fileNumber, creationDate, insuredName, 
                customer, employee, suitNumber, fileStatus);
            Dictionary<String, object> response = new Dictionary<String, object>();
            if (true == creatioResult)
            {
                response.Add(CsConstatnts.fileNumber, fileNumber);
            } else
            {
                response.Add(CsConstatnts.error, ErrorsCode.FILE_CREATION_FAILED);
            }           
            return Ok(JsonUtils.toJsonStr(response));
        }
    }
}
