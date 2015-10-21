

namespace Web.Siport.Security
{
    using System;
    using System.Collections.Specialized;
    using System.Web.Security;

    public class CustomMembershipProvider : MembershipProvider
    {
        #region Properties

        private int _cacheTimeoutInMinutes = 30;

        #endregion

        public override void Initialize(string name, NameValueCollection config)
        {
            // Set Properties
            int val;
            if (!string.IsNullOrEmpty(config["cacheTimeoutInMinutes"]) && Int32.TryParse(config["cacheTimeoutInMinutes"], out val))
                _cacheTimeoutInMinutes = val;

            // Call base method
            base.Initialize(name, config);
        }

        public override MembershipUser CreateUser(
            string username,
            string password,
            string email,
            string passwordQuestion,
            string passwordAnswer,
            bool isApproved,
            object providerUserKey,
            out MembershipCreateStatus status)
        {
            throw new System.NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(
            string username,
            string password,
            string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new System.NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new System.NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new System.NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new System.NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public override bool EnablePasswordReset
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
    }
}