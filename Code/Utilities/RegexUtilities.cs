#region

using System.Text.RegularExpressions;

#endregion

namespace UrbanSchedulerProject.Code.Utilities
{
    ///<summary>
    ///Used for regular expression functions
    ///</summary>
    public static class RegexUtilities
    {
        ///<summary>
        /// Used to validate emails
        ///</summary>
        ///<param name="strIn"></param>
        ///<returns></returns>
        public static bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                                 @"^(?("")(""[^""]+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }
    }
}