#region

using System;
using System.Collections.Generic;
using UrbanSchedulerProject.Code.Utilities.TypeUtilities;

#endregion

namespace UrbanSchedulerProject.App.UserControl
{
    /// <summary>
    /// Used to display feedback to users on pages.
    /// </summary>
    public partial class ucFeedback : System.Web.UI.UserControl
    {
        private List<string> _error = new List<string>();
        private List<string> _info = new List<string>();
        private List<string> _success = new List<string>();
        private List<string> _warning = new List<string>();

        /// <summary>
        /// Gets or sets the success.
        /// </summary>
        /// <value>
        /// The success.
        /// </value>
        private List<string> Success
        {
            get { return _success; }
            set { _success = value; }
        }

        /// <summary>
        /// Gets or sets the warning.
        /// </summary>
        /// <value>
        /// The warning.
        /// </value>
        private List<string> Warning
        {
            get { return _warning; }
            set { _warning = value; }
        }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        private new List<string> Error
        {
            get { return _error; }
            set { _error = value; }
        }

        /// <summary>
        /// Gets or sets the info.
        /// </summary>
        /// <value>
        /// The info.
        /// </value>
        private List<string> Info
        {
            get { return _info; }
            set { _info = value; }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        private void Refresh()
        {
            if (Error.Count > 0)
            {
                pnlFeedbackError.Visible = true;
                litFeedbackError.Visible = true;
                litFeedbackError.Text = String.Empty;
                for (var i = 0; i < Error.Count; i++)
                    litFeedbackError.Text += (Error[i] + (i != Error.Count - 1 ? "<br />" : ""));
            }
            if (Success.Count > 0)
            {
                pnlFeedbackSuccess.Visible = true;
                litFeedbackSuccess.Visible = true;
                litFeedbackSuccess.Text = String.Empty;
                for (var i = 0; i < Success.Count; i++)
                    litFeedbackSuccess.Text += (Success[i] + (i != Success.Count - 1 ? "<br />" : ""));
            }
            if (Warning.Count > 0)
            {
                pnlFeedbackWarning.Visible = true;
                litFeedbackWarning.Visible = true;
                litFeedbackWarning.Text = String.Empty;
                for (var i = 0; i < Warning.Count; i++)
                    litFeedbackWarning.Text += (Warning[i] + (i != Warning.Count - 1 ? "<br />" : ""));
            }

            if (Info.Count <= 0) return;

            pnlFeedbackInfo.Visible = true;
            litFeedbackInfo.Visible = true;
            litFeedbackInfo.Text = String.Empty;
            for (var i = 0; i < Info.Count; i++)
                litFeedbackInfo.Text += (Info[i] + (i != Info.Count - 1 ? "<br />" : ""));
        }



        /// <summary>
        /// Inserts the feed back.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        public void InsertFeedBack(string type, string message)
        {
            switch (type)
            {
                case FeedbackType.Error:
                    Error.Add(message);
                    break;
                case FeedbackType.Success:
                    Success.Add(message);
                    break;
                case FeedbackType.Warning:
                    Warning.Add(message);
                    break;
                case FeedbackType.Info:
                    Info.Add(message);
                    break;
            }
            Refresh();
        }
    }
}