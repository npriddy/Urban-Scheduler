#region

using System.Collections.Generic;
using System.IO;
using System.Web;
using UrbanSchedulerProject.Entities;

#endregion

namespace UrbanSchedulerProject.Code.Utilities.EmailTemplates
{
    /// <summary>
    /// Email Template Utilities to assit in email sending.
    /// </summary>
    public static class EmailTemplateUtilities
    {
        /// <summary>
        ///     Gets the HTML template from file.
        /// </summary>
        /// <returns></returns>
        private static string GetHtmlTemplate()
        {
            var emailTemplatePath = HttpContext.Current.Request.MapPath("\\" + "App_Data" + "\\EmailTemplate" + "\\EmailTemplate.html");
            var htmlTemplate = File.ReadAllText(emailTemplatePath);

            return htmlTemplate;
        }

        /// <summary>
        ///     Emails from from master.
        /// </summary>
        /// <param name = "variables">The variables lust of string to be replace ie Nate Priddy with {!from}.</param>
        /// <param name = "subject">The subject.</param>
        /// <param name = "to">To.</param>
        /// <param name = "cc">The cc.</param>
        /// <datetime>6/17/2011-10:16 AM</datetime>
        /// <author>
        ///     nate
        /// </author>
        public static void EmailFromFromMaster(IEnumerable<KeyValuePair<string, string>> variables, string subject, User to, IEnumerable<int> cc)
        {
            var html = GetHtmlTemplate();
            var text = "";
            EmailUtilities.Replacer(variables, ref html, ref text, ref subject);
            EmailUtilities.SendEmail(html, text, subject, to.FirstName + " " + to.LastName, to.Email, cc);
        }
    }
}