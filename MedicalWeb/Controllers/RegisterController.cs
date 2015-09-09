using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalWeb.Models;
using System.Net.Http;
using System.Net;
using System.Web.Http;

namespace MedicalWeb.Controllers
{
    public class RegisterController : ApiController
    {
        private PharmciRepositories db = new PharmciRepositories();

        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage()
           {
               Content = new StringContent("Success")
           };

       }

        
       public HttpResponseMessage Post(MyRegisterModel model)
       {

           var oldDonor = db.GetByUserName(model.UserName);
           if (oldDonor != null)
           {
               var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
               {
                   Content = new StringContent("Email already exists"),
                   ReasonPhrase = "Email already exists"
               };
               return resp; //throw new HttpResponseException(resp);
           }
           var donor = new Pharmci()
           {
               Id = Guid.NewGuid(),
               UserName = model.UserName,
               DisplayName = model.DisplayName,
               Phone = model.Phone,
               BloodType = model.BloodType,
               ZoneName = model.Zone,
           };
           donor.SetPassword(model.Password);
           try
           {
               db.CreateUser(donor);
               return new HttpResponseMessage()
               {
                   Content = new StringContent("Success")
               };
           }
           catch (Exception exc)
           {
               var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
               {
                   Content = new StringContent("Failed to register a new user:" + exc.InnerException),
                   ReasonPhrase = "Failed to register a new user"
               };
               return resp;  //throw new HttpResponseException(resp);
           }
       }

       public void Method()
       {
           throw new System.NotImplementedException();
       }


    }
}
