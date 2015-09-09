using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalWeb.Models
{
    public class Medical
    {
        public Guid id { get; set; }
        public Guid ParentId { get; set; }
        public string Type { get; set; }
        public string name { get; set; }
        public int quantiti { get; set; }
        public string MediaThwmbnail { get; set; }
        public string Description { get; set; }
        public int price { get; set; }

        public void Method()
        {
            throw new System.NotImplementedException();
        }

    }
}