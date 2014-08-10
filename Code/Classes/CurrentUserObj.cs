#region

using System;
using Urban.Data;
using UrbanSchedulerProject.Code.Utilities;

#endregion

namespace UrbanSchedulerProject.Code.Classes
{
    /// <summary>
    ///     Container for authenticated user in session.
    /// </summary>
    public class CurrentUserObj
    {
        #region Members

        ///<summary>
        /// Currently Logged in users email
        ///</summary>
        public readonly string Email;

        /// <summary>
        /// Id of user 
        /// </summary>
        public readonly int Id;

        #endregion Members

        /// <summary>
        ///     Initializes a new instance of the <see cref = "CurrentUserObj" /> class.
        ///     Will save in  session and authenticate
        /// </summary>
        /// <param name = "email">The email.</param>
        public CurrentUserObj(string email)
        {
            if (email == String.Empty)
                throw new ArgumentException("username == String.Empty", "email");

            TraceUtilities.WriteTrace(true);
            var db = new UrbanDataContext();
            var person = db.Manager.User.GetUserByEmail(email);
            //Loads information for first and last name from contacts if they exist.)
            if (person == null)
                throw new NullReferenceException("Contact associated with the user is null for username:" + email + Environment.StackTrace);

            Id = person.Id;
            Email = person.Email;

            TraceUtilities.WriteTrace(false);
        }
    }
}