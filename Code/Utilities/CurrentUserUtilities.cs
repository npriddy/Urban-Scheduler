#region

using System.Web;
using UrbanSchedulerProject.Code.Classes;

#endregion

namespace UrbanSchedulerProject.Code.Utilities
{
    /// <summary>
    /// Assist with current user functions
    /// </summary>
    public static class CurrentUserUtilities
    {
        /// <summary>
        ///     Gets the cu.
        /// </summary>
        /// <author>nate</author>
        /// <datetime>6/17/2011-10:05 AM</datetime>
        private static CurrentUserObj Cu
        {
            get
            {
                var session = HttpContext.Current.Session;
                if (session["currentUser"] != null)
                {
                    return (CurrentUserObj) session["currentUser"];
                }
                if (session.Count > 0 && session[0] is CurrentUserObj)
                {
                    return (CurrentUserObj) HttpContext.Current.Session[0];
                }
                if (RepopulateSession(HttpContext.Current.User.Identity.Name))
                {
                    return (CurrentUserObj) session["currentUser"];
                }
                return null;
            }
        }

        /// <summary>
        ///     Gets the current user.
        /// </summary>
        /// <returns></returns>
        /// <author>nate</author>
        /// <datetime>6/17/2011-10:06 AM</datetime>
        public static CurrentUserObj GetCurrentUser()
        {
            return Cu;
        }

        /// <summary>
        ///     Repopulates the session.
        /// </summary>
        /// <param name = "userName">Name of the user.</param>
        /// <returns></returns>
        /// <author>nate</author>
        /// <datetime>6/17/2011-10:06 AM</datetime>
        private static bool RepopulateSession(string userName)
        {
            if (userName != "" && Utilities.IsEmail(userName))
            {
                HttpContext.Current.Session.RemoveAll();
                var currentUser = new CurrentUserObj(userName);
                HttpContext.Current.Session["currentUser"] = currentUser;
                return true;
            }
            return false;
        }


        /// <summary>
        ///     Gets the cu id safely.
        /// </summary>
        /// <returns></returns>
        public static int GetCuIdSafely()
        {
            try
            {
                return Cu != null ? Cu.Id : 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}