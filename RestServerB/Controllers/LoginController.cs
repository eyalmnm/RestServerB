using System;
using System.Web.Http;
using RestServerB.Models;
using RestServerB.Managers;
using RestServerB.Data_Manager;
using System.Web;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using RestServerB.Utils;
using RestServerB.MyConfig;

namespace RestServerB.Controllers
{
    public class LoginController : ApiController
    {
        
        // POST: api/Login
        // In case Employee exist return UUID for using as 
        // authorization haeder -
        // https://stackoverflow.com/questions/12732422/adding-header-for-httpurlconnection
        // Reading header's data
        // https://stackoverflow.com/questions/3530041/getting-a-request-headers-value

        public IHttpActionResult Post(LoginData value)
        {
            Console.WriteLine("LoginController");
            LoginPersistance loginPersistance = new LoginPersistance();

            String name = value.getName();
            String pass = value.getPassword();
            String uuid = loginPersistance.Login(name, pass);

            if ((null == uuid) || (0 == uuid.Length))
            {
                Console.WriteLine($"user {name} not found");
                Dictionary<String, object> response = new Dictionary<String, object>();
                response.Add(CsConstatnts.error, ErrorsCode.USER_NOT_FOUND);
                return Ok(JsonUtils.toJsonStr(response));
            }
            else
            {
                Console.WriteLine($"user {name} logged in successfully");
                Dictionary<String, object> response = new Dictionary<String, object>();
                response.Add(CsConstatnts.userName, name);
                response.Add(CsConstatnts.uuid, uuid);

                return Ok(JsonUtils.toJsonStr(response));
            }
        }

        public IHttpActionResult Get(string name, string password)
        {
            LoginPersistance loginPersistance = new LoginPersistance();

            String usr = name; //  value.getName();
            String pass = password; // value.getPassword();
            String uuid = loginPersistance.Login(usr, pass);
            if ((null == uuid) && (0 == uuid.Length))
            {
                Dictionary<String, object> response = new Dictionary<String, object>();
                response.Add(CsConstatnts.error, ErrorsCode.USER_NOT_FOUND);
                return Ok(JsonUtils.toJsonStr(response));
            } else
            {
                Dictionary<String, object> response = new Dictionary<String, object>();
                response.Add(CsConstatnts.userName, name);
                response.Add(CsConstatnts.uuid, uuid);

                return Ok(JsonUtils.toJsonStr(response));
            }
        }
    }
}
