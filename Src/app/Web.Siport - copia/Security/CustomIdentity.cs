namespace Web.Siport.Security
{
    using System.Security.Principal;
    using System.Web.Security;

    public class CustomIdentity : IIdentity
    {
        #region Implementation of IIdentity
        public string Name { get { return Identity.Name; } }

        public string AuthenticationType { get { return Identity.AuthenticationType; } }

        public bool IsAuthenticated { get { return Identity.IsAuthenticated; } }

        #endregion

        public IIdentity Identity { get; set; }

        public string NombreUsuario { get; set; }

        public string EmailUsuario { get; set; }

        public int UserRoleId { get; set; }

        public string UserRoleName { get; set; }

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