#region

using System;
using Urban.Data;

#endregion

namespace UrbanSchedulerProject.App.UserControl
{
    /// <summary>
    /// Edit Dates
    /// </summary>
    public partial class ucEditDates : System.Web.UI.UserControl
    {
        /// <summary>
        /// Gets or sets the data item.
        /// </summary>
        /// <value>
        /// The data item.
        /// </value>
        public object DataItem { get; set; }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the DataBinding event of the EmployeeDetails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void EmployeeDetails_DataBinding(object sender, EventArgs e)
        {
            _lblId.Text = ((RoomAvailability) DataItem).Id.ToString();
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DataBinding += this.EmployeeDetails_DataBinding;
        }

        #endregion
    }
}