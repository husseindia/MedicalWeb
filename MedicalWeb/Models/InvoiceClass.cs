using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalWeb.Models
{
    public class InvoiceClass
    {
        public Guid id { get; set; }
        public string pharmName { get; set; }

        public string medicalName { get; set; }
        public int quantiti { get; set; }

        public float price { get; set; }
        public void Method()
        {
            throw new System.NotImplementedException();
        }    
    }
}