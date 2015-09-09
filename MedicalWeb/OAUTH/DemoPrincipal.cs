using System.Security.Principal;
using OAuth2.Mvc;

namespace MedicalWeb.OAuth
{
    public class DemoPrincipal : OAuthPrincipalBase
    {
        public DemoPrincipal(IOAuthProvider provider, IIdentity identity)
            : base(provider, identity)
        { }

        protected override void Load()
        {
            // Everyone is an admin in the demo!
            Roles = new[] {"Admin"};
        }
    }
}