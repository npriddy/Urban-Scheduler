using System;
using System.Security.Cryptography;
using System.Web.Security;
using Urban.Data;

namespace UrbanSchedulerProject.Code.Utilities
{
    public static class UserUtilities
    {
        #region CreateUser

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static bool CreateUser(User info, string password)
        {
            var db = new UrbanDataContext();

            //User Item
            var u = new User
                        {
                            ActivationGuid = info.ActivationGuid,
                            City = info.City,
                            DateCreated = DateTime.Now,
                            Email = info.Email,
                            FirstName = info.FirstName,
                            LastName = info.LastName,
                            IsAdmin = false,
                            PhoneNumber = info.PhoneNumber,
                            PrimaryAddress = info.PrimaryAddress,
                            Zip = info.Zip
                        };

            string passSalt = CreateSalt();
            string passwordHash = CreatePasswordHash(password, passSalt);

            u.Password = passwordHash;
            u.PasswordSalt = passSalt;
            db.User.InsertOnSubmit(u);
            db.SubmitChanges();

            //Send Email With firm information user ID, and the new password.
            //User_EmailTemplate.NewUserAccount(u.Id, (int)c.FirmID, password);

            return true;
        }

        /// <summary>
        /// Checks the activate user.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static int CheckActivateUser(Guid key)
        {
            var db = new UrbanDataContext();
            User user = db.Manager.User.GetByActivationGuid(key);
            if (user == null)
                return -1;

            if (user.IsUserAuthenticated != null && (bool) user.IsUserAuthenticated)
                return 0;

            user.IsUserAuthenticated = true;
            db.SubmitChanges();
            return 1;
        }

        #endregion CreateUser

        #region Login

        /// <summary>
        ///   Logins the specified user name.
        /// </summary>
        /// <param name = "userName">Name of the user.</param>
        /// <param name = "password">The password.</param>
        /// <returns></returns>
        public static bool Login(string userName, string password)
        {
            var db = new UrbanDataContext();
            User c = db.Manager.User.GetUserByEmail(userName);
            if (c != null && c.IsUserAuthenticated != null && (bool) c.IsUserAuthenticated)
            {
                if (DoesPassMatch(password, c.PasswordSalt, c.Password))
                    return true;
                return false;
            }
            return false;
        }

        #endregion Login

        /// <summary>
        ///   Creates the pass.
        /// </summary>
        /// <param name = "pwd">The PWD.</param>
        /// <returns></returns>
        private static string CreatePass(string pwd)
        {
            string salt = CreateSalt();
            return CreatePasswordHash(pwd, salt);
        }

        /// <summary>
        ///   Creates the salt.
        /// </summary>
        /// <returns></returns>
        private static string CreateSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[32];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        ///   Creates the password hash.
        /// </summary>
        /// <param name = "pwd">The PWD.</param>
        /// <param name = "salt">The salt.</param>
        /// <returns></returns>
        private static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");
            return hashedPwd;
        }

        /// <summary>
        ///   Doeses the pass match.
        /// </summary>
        /// <param name = "pwd">The PWD.</param>
        /// <param name = "salt">The salt.</param>
        /// <param name = "storedPwdHash">The stored PWD hash.</param>
        /// <returns></returns>
        private static bool DoesPassMatch(string pwd, string salt, string storedPwdHash)
        {
            string hashPass = CreatePasswordHash(pwd, salt);
            if (hashPass == storedPwdHash)
                return true;
            else return false;
        }
    }
}