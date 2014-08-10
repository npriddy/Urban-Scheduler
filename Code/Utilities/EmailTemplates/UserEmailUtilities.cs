#region

using System.Collections.Generic;
using UrbanSchedulerProject.Entities;

#endregion

namespace UrbanSchedulerProject.Code.Utilities.EmailTemplates
{
    /// <summary>
    /// User Email utilities for templates
    /// </summary>
    public static class UserEmailUtilities
    {
        /// <summary>
        ///     Sends new user account email with activation key
        /// </summary>
        /// <param name = "info">The info.</param>
        /// <datetime>6/11/2011-11:42 AM</datetime>
        /// <author>
        ///     nate
        /// </author>
        public static void NewUserAccount(User info)
        {
            var keys = new List<KeyValuePair<string, string>>
                           {
                               new KeyValuePair<string, string>("{!MainTitleHtml}", "Account Created"),
                               new KeyValuePair<string, string>("{!Subtitle01Html}", "Action Required"),
                               new KeyValuePair<string, string>("{!Subtitle01TextHtml}", "Congratulations on creating your account activation is still required. Please follow the links bellow."),
                               new KeyValuePair<string, string>("{!LinkHtml}", "http://UrbanScheduler.com?activationKey=" + info.ActivationGuid),
                               new KeyValuePair<string, string>("{!LinkTextHtml}", "Click Here To Activate Account"),
                               new KeyValuePair<string, string>("{!FormPerson}", "UrbanScheduler System")
                           };

            EmailTemplateUtilities.EmailFromFromMaster(keys, "UrbanScheduler Account Created", info, new List<int>());
        }
    }
}