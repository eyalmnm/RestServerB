using System.Web.Http;
using RestServerB.Models;
using RestServerB.Data_Manager;
using System;
using RestServerB.Utils;
using System.Collections.Generic;
using RestServerB.MyConfig;
using System.Linq;
using RestServerB.Managers;

// Ref: https://ilclubdellesei.wordpress.com/2018/02/14/how-to-upload-images-to-an-asp-net-core-rest-service-with-xamarin-forms/

namespace RestServerB.Controllers
{
    public class ImageUploadController : ApiController
    {
        public IHttpActionResult Post(UploadedFile value)
        {
            Dictionary<String, object> response = new Dictionary<String, object>();

            string directory = value.getFilePath();
            string fileName = value.getFileName();
            string fileContent = value.getFileContent();            

            UploadFilePersistance persistance = new UploadFilePersistance(directory, fileName, fileContent);

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

            try
            {
                String path = persistance.SaveInDirectory();
                if (true == StringUtils.IsNullOrEmpty(path))
                {
                    response.Add(CsConstatnts.error, ErrorsCode.TARGET_FILE_FAILED_TO_RESTORED);
                } else
                {
                    response.Add(CsConstatnts.fileFullPath, path);
                }
            } catch (Exception e)
            {
                int errorCode = int.Parse(e.Message);
                response.Add(CsConstatnts.error, errorCode);
            }
            return Ok(JsonUtils.toJsonStr(response));
        }
    }
}