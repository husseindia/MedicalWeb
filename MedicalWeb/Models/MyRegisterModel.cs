using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalWeb.Models
{
    public class MyRegisterModel
    {
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Zone { get; set; }
        public int BloodType { get; set; }
    }
}