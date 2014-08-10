#region

using System;
using Telerik.Web.UI;
using Urban.Data;
using UrbanSchedulerProject.Code.BasePage;
using UrbanSchedulerProject.Code.Utilities;
using UrbanSchedulerProject.Code.Utilities.DB;
using UrbanSchedulerProject.Code.Utilities.EmailTemplates;
using UrbanSchedulerProject.Code.Utilities.TypeUtilities;
using UrbanSchedulerProject.Entities;

#endregion

namespace UrbanSchedulerProject.App.Popup
{
    /// <summary>
    /// Used for user creation
    /// </summary>
    public partial class pUserCreation : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }


        /// <summary>
        ///     Determines whether [is input valid].
        /// </summary>
        /// <returns>
        ///     <c>true</c> if [is input valid]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsInputValid()
        {
            var isValid = true;
            if (_txtEmail.Text.Trim() == String.Empty)
            {
                WriteFeedBackMaster(FeedbackType.Warning, "Email Required");
                isValid = false;
            }

            if (!RegexUtilities.IsValidEmail(_txtEmail.Text.Trim()))
            {
                WriteFeedBackMaster(FeedbackType.Warning, "Email Invalid");
                isValid = false;
            }

            if (_txtPassword.Text.Trim() == String.Empty || _txtPasswordVerification.Text.Trim() == String.Empty)
            {
                WriteFeedBackMaster(FeedbackType.Warning, "Password Required");
                isValid = false;
            }
            if (_txtPassword.Text.Trim() != _txtPasswordVerification.Text.Trim())
            {
                WriteFeedBackMaster(FeedbackType.Warning, "Passwords do not match");
                isValid = false;
            }
            if (_rCaptcha.IsValid == false)
            {
                WriteFeedBackMaster(FeedbackType.Warning, "Captcha does not match");
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        ///     Handles the Click event of the _btnCreateAccount control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _btnCreateAccount_Click(object sender, EventArgs e)
        {
            if (IsInputValid() == false)
                return;

            var db = new UrbanDataContext();
            var user = db.Manager.User.GetUserByEmail(_txtEmail.Text.Trim());
            if (user != null)
            {
                WriteFeedBackMaster(FeedbackType.Warning, "Email address already in use.");
                return;
            }

            //Create new user
            var u = new User
                        {
                            ActivationGuid = Guid.NewGuid(),
                            DateCreated = DateTime.Now,
                            FirstName = _txtFirstName.Text.Trim(),
                            LastName = _txtLastName.Text.Trim(),
                            Email = _txtEmail.Text.Trim()
                        };

            //Email new user and verify success
            if (UserUtilities.CreateUser(u, _txtPassword.Text.Trim()))
            {
                UserEmailUtilities.NewUserAccount(u);
                WriteFeedBackMaster(FeedbackType.Success, "Account Created Succesfully");
                var ajaxManager = RadAjaxManager.GetCurrent(Page);
                ajaxManager.ResponseScripts.Add("Close();");
            }
            else
            {
                WriteFeedBackMaster(FeedbackType.Error, "Account Creation Error");
            }
        }
    }
}