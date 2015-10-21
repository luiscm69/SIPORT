
using System;
using System.Security.Principal;
using System.Web.Security;
namespace Seguridad.Common.Security
{
    [Serializable]
    public class CustomIdentity : IIdentity
    {
        #region Properties

        public IIdentity Identity { get; set; }

        public string NombreUsuario { get; set; }

        public string EmailUsuario { get; set; }

        public int UserRoleId { get; set; }

        public string UserRoleName { get; set; }
        
        #endregion

        #region Implementation of IIdentity

        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return Identity.IsAuthenticated; }
        }

        public string Name
        {
            get { return Identity.Name; }
        }
        
        #endregion


        #region Constructor

        public CustomIdentity(IIdentity identity)
        {
            Identity = identity;

            var customMembershipUser = (CustomMembershipUser)Membership.GetUser(identity.Name);
            if (customMembershipUser != null)
            {
                NombreUsuario = customMembershipUser.NombreUsuario;
                EmailUsuario = customMembershipUser.Email;
                //UserRoleId = customMembershipUser.UserRoleId;
                //UserRoleName = customMembershipUser.UserRoleName;
            }
        }

        #endregion 


        
    }
}
