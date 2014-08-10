#region

using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Urban.Data;

#endregion

namespace UrbanSchedulerProject.Code.Utilities
{
    /// <summary>
    /// Emails are physically handled at this point.
    /// </summary>
    public static class EmailUtilities
    {
        private const string GlobalServer = "smtp.gmail.com";
        private const string GlobalLiveServer = "localhost";

        #region Send

        /// <summary>
        ///     Sends email
        ///     Attaches Files by Id in the DocList
        /// </summary>
        /// <param name = "html">The HTML.</param>
        /// <param name = "text">The text.</param>
        /// <param name = "subject">The subject.</param>
        /// <param name = "toName">To name.</param>
        /// <param name = "toEmail">To email.</param>
        /// <param name = "cc">The cc.</param>
        public static void SendEmail(string html, string text, string subject, string toName, string toEmail, IEnumerable<int> cc)
        {
            if (string.IsNullOrEmpty(toEmail)) return;
            var db = new UrbanDataContext();
            const string email = "donotreply@UrbanScheduler.com";
            const string emailSystem = "UrbanScheduler";

            var smtp = new SmtpClient(Utilities.IsSiteLocal() ? GlobalServer : GlobalLiveServer);
            var message = new MailMessage(new MailAddress(email, emailSystem), new MailAddress(toEmail, toName))
                              {Subject = subject};

            foreach (var userCc in cc.Select(i => db.Manager.User.GetByKey(i)).Where(userCc => !string.IsNullOrEmpty(userCc.Email)))
            {
                message.CC.Add(new MailAddress(userCc.Email, userCc.FirstName + " " + userCc.LastName));
            }

            //Create Views
            var htmlView = AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html);
            var textView = AlternateView.CreateAlternateViewFromString(text, null, "text/plain");

            message.AlternateViews.Add(textView);
            message.AlternateViews.Add(htmlView);

            if (Utilities.IsSiteLocal())
            {
                var userName = ConfigurationManager.AppSettings["gmailAddress"];
                var password = ConfigurationManager.AppSettings["gmailPass"];
                var cred = new NetworkCredential(userName, password);
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = cred;
                smtp.Port = 587;
            }

            db.SubmitChanges();
            smtp.Timeout = 300000;
            smtp.Send(message);
        }

        #endregion Send

        #region Misc

        /// <summary>
        ///     Replacers the specified variables.
        /// </summary>
        /// <param name = "variables">The variables.</param>
        /// <param name = "html">The HTML.</param>
        /// <param name = "text">The text.</param>
        /// <param name = "subject">The subject.</param>
        public static void Replacer(IEnumerable<KeyValuePair<string, string>> variables, ref string html, ref string text, ref string subject)
        {
            //Cycles through html, text, subject variables and fills in actual assigned text-values
            foreach (var kvp in variables)
            {
                if (html.Contains(kvp.Key))
                    html = html.Replace(kvp.Key, kvp.Value);

                if (text.Contains(kvp.Key))
                    text = text.Replace(kvp.Key, kvp.Value);

                if (subject.Contains(kvp.Key))
                    subject = subject.Replace(kvp.Key, kvp.Value);
            }
        }

        #endregion Misc
    }
}