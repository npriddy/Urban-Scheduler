#region

using System;
using System.Data.Linq;
using System.Linq;
using UrbanSchedulerProject.Entities;

#endregion

namespace Urban.Data
{
    public partial class UserManager
    {
        #region Query

        /// <summary>
        /// Gets the user by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public User GetUserByEmail(string email)
        {
            return Context.LoadOptions == null ? Query.GetUserByEmail.Invoke(Context, email) : Entity.SingleOrDefault(p => p.Email == email);
        }

        // A private class for lazy loading static compiled queries.

        /// <summary>
        /// Gets the by activation GUID.
        /// </summary>
        /// <param name="activation">The activation.</param>
        /// <returns></returns>
        public User GetByActivationGuid(Guid activation)
        {
            return Entity.SingleOrDefault(p => p.ActivationGuid == activation);
        }

        private static partial class Query
        {
            // Add your compiled queries here. 
            internal static readonly Func<UrbanDataContext, string, User> GetUserByEmail =
                CompiledQuery.Compile(
                    (UrbanDataContext db, string email) => db.User.SingleOrDefault(p => p.Email == email));
        }

        #endregion
    }
}