

using System;
using System.Collections.Specialized;
using System.Web.Security;
using System.Linq;

namespace Seguridad.Common.Security
{
    public class CustomRoleProvider : RoleProvider
    {
        #region Properties

        private int _cacheTimeoutInMinutes = 30;

        #endregion

        #region Overrides of RoleProvider

        /// <summary>
        /// Initialize values from web.config.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        public override void Initialize(string name, NameValueCollection config)
        {
            // Set Properties
            int val;
            if (!string.IsNullOrEmpty(config["cacheTimeoutInMinutes"]) && Int32.TryParse(config["cacheTimeoutInMinutes"], out val))
                _cacheTimeoutInMinutes = val;

            // Call base method
            base.Initialize(name, config);
        }

        /// <summary>
        /// Gets a value indicating whether the specified user is in the specified role for the configured applicationName.
        /// </summary>
        /// <returns>
        /// true if the specified user is in the specified role for the configured applicationName; otherwise, false.
        /// </returns>
        /// <param name="username">The user name to search for.</param><param name="roleName">The role to search in.</param>
        public override bool IsUserInRole(string username, string roleName)
        {
            var userRoles = GetRolesForUser(username);
            return userRoles.Contains(roleName);
        }

        /// <summary>
        /// Gets a list of the roles that a specified user is in for the configured applicationName.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the roles that the specified user is in for the configured applicationName.
        /// </returns>
        /// <param name="username">The user to return a list of roles for.</param>
        public override string[] GetRolesForUser(string username)
        {
            //Return if the user is not authenticated
            //if (!HttpContext.Current.User.Identity.IsAuthenticated)
            //    return null;

            ////Return if present in Cache
            //var cacheKey = string.Format("UserRoles_{0}", username);
            //if (HttpRuntime.Cache[cacheKey] != null)
            //    return (string[])HttpRuntime.Cache[cacheKey];

            ////Get the roles from DB
            //var userRoles = new string[] { };
            //using (var context = new MvcDemoEntities())
            //{
            //    var user = (from u in context.Users.Include(usr => usr.UserRole)
            //                where String.Compare(u.Username, username, StringComparison.OrdinalIgnoreCase) == 0
            //                select u).FirstOrDefault();

            //    if (user != null)
            //        userRoles = new[] { user.UserRole.UserRoleName };
            //}

            ////Store in cache
            //HttpRuntime.Cache.Insert(cacheKey, userRoles, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinutes), Cache.NoSlidingExpiration);

            //// Return
            //return userRoles.ToArray();

            throw new System.NotImplementedException();
        }

        #endregion


        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new System.NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new System.NotImplementedException();
        }


        public override string[] GetUsersInRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new System.NotImplementedException();
        }
    }
}
