using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OAuth2.Mvc;

namespace MedicalWeb.Controllers
{
    public class LogInController : ApiController
    {

        public static string AccessToken;

        // GET api/login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }



        public OAuthResponse Get(string username, string password)
        {
            var requestToken = OAuthServiceBase.Instance.RequestToken().RequestToken;
            var accessResponse = OAuthServiceBase.Instance.AccessToken(requestToken, "User", username, password, false);
            if (!accessResponse.Success)
            {

                OAuthServiceBase.Instance.UnauthorizeToken(requestToken);
                var requestResponse = OAuthServiceBase.Instance.RequestToken();
                // throw new HttpResponseException(HttpStatusCode.Unauthorized);

                return new OAuthResponse
                {
                    Error = "The username or password was not correct.",
                    Success = false
                    //RequestToken = requestResponse.RequestToken
                };
            }
            AccessToken = accessResponse.AccessToken;
            return accessResponse;

        }

        // GET api/login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/login
        public void Post([FromBody]string value)
        {
        }

        // PUT api/login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/login/5
        public void Delete(int id)
        {
        }
    }

}
