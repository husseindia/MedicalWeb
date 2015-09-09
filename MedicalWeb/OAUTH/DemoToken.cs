using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalWeb.OAuth
{
    public class DemoToken
    {
        public DemoToken(string name)
        {
            AccessToken = Guid.NewGuid().ToString("N");
            Expire = DateTime.Now.AddMinutes(60);
            Name = name;
            RefreshToken = Guid.NewGuid().ToString("N");
        }

        public DemoToken(string name, Guid userId)
        {
            AccessToken = Guid.NewGuid().ToString("N");
            Expire = DateTime.Now.AddMinutes(60);
            Name = name;
            UserId = userId;
            RefreshToken = Guid.NewGuid().ToString("N");
        }

        public string Name { get; set; }
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expire { get; set; }
            
        public int ExpireSeconds
        {
            get { return (int) Expire.Subtract(DateTime.Now).TotalSeconds; }
        }

        public bool IsAccessExpired
        {
            get { return DateTime.Now > Expire; }
        }

        public bool IsRefreshExpired
        {
            get { return DateTime.Now > Expire.AddMinutes(5); }
        }
    }
}
