using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalWeb.Models
{
    public class Customer
    {
        public Guid id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Nameofpharmaci { get; set; }
        public string city { get; set; }
        public string Email { get; set; }
        public int MobileNumber { get; set; }

    }
}