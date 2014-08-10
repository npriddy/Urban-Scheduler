#region

using System;
using System.Web.Security;
using Telerik.Web.UI;
using UrbanSchedulerProject.Code.BasePage;
using UrbanSchedulerProject.Code.Classes;
using UrbanSchedulerProject.Code.Utilities;
using UrbanSchedulerProject.Code.Utilities.DB;
using UrbanSchedulerProject.Code.Utilities.TypeUtilities;

#endregion

namespace UrbanSchedulerProject.App.UserControl
{
    /// <summary>
    /// Handles logging user control
    /// </summary>
    public partial class ucLogin : BaseUserControl
    {
        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            SetLoginControls();
        }

        /// <summary>
        ///     Sets the login controls.
        /// </summary>
        private void SetLoginControls()
        {
            if (CurrentUserUtilities.GetCuIdSafely() > 0)
            {
                _lblEmail.Text = Cu.Email;
                _lblEmail.Visible = true;
                _txtEmail.Visible = false;
                _txtPassword.Visible = false;
                _btnLogin.Visible = false;
                _btnLogout.Visible = true;
                _btnCreateAccount.Visible = false;
                _pnlLoggedIn.Visible = true;
                _pnlUserLoggedIn.Visible = false;
            }
            else
            {
                _lblEmail.Visible = false;
                _txtEmail.Visible = true;
                _txtPassword.Visible = true;
                _btnLogin.Visible = true;
                _btnLogout.Visible = false;
                _btnCreateAccount.Visible = true;
                _pnlLoggedIn.Visible = false;
                _pnlUserLoggedIn.Visible = true;
            }
        }

        /// <summary>
        ///     Handles the Click event of the _btnLogin control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _btnLogin_Click(object sender, EventArgs e)
        {
            if (_txtEmail.Text.Trim() == String.Empty || _txtPassword.Text.Trim() == String.Empty)
                WriteFeedBackMaster(FeedbackType.Warning, "Please enter a username and password.");


            if (!UserUtilities.Login(_txtEmail.Text.Trim(), _txtPassword.Text.Trim()))
                return;

            FormsAuthentication.SignOut();
            Session.RemoveAll();
            //add Session stuff here!!
            var userObj = new CurrentUserObj(_txtEmail.Text.Trim());

            Session["currentUser"] = userObj;
            Session.Timeout = 60;
            FormsAuthentication.SetAuthCookie(_txtEmail.Text.Trim(), true);

            SetLoginControls();

            RadAjaxManager.GetCurrent(Page).Redirect("~/App/Pages/MyAccount.aspx");
        }

        /// <summary>
        ///     Handles the Click event of the _btnLogout control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            SetLoginControls();
            _txtEmail.Text = String.Empty;
            WriteFeedBackMaster(FeedbackType.Success, "User logged out.");
            RadAjaxManager.GetCurrent(Page).Redirect("~/Default.aspx");
        }
    }
}