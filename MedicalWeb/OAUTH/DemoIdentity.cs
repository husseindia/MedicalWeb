using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using OAuth2.Mvc;

namespace MedicalWeb.OAuth
{
    
    public class DemoIdentity : OAuthIdentityBase
    {
        public DemoIdentity(IOAuthProvider provider, string token)
            : base(provider)
        {
            Token = token;
            Realm = "Demo";
        }

        protected override void Load()
        {
            var token = DemoService.Tokens.FirstOrDefault(t => t.AccessToken == Token && !t.IsAccessExpired);
            if (token == null)
                return;

            IsAuthenticated = true;
            Name = token.Name;
        }

    }
}