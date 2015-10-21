using System;
using System.Security.Principal;
namespace Seguridad.Common.Security
{
     [Serializable]
    public class CustomPrincipal : IPrincipal
    {
        public CustomPrincipal(CustomIdentity identity)
        {
             Identity = identity;
        }
        public IIdentity Identity { get; private set; }
        public CustomIdentity CustomIdentity { get { return (CustomIdentity)Identity; } set { Identity = value; } }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}
