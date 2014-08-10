#region

using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI;
using Urban.Data;

#endregion

namespace UrbanSchedulerProject.Code.Utilities.DB
{
    /// <summary>
    /// </summary>
    public static class RoomTypeUtilties
    {
        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<RadComboBoxItem> GetAll(bool insertEmpty)
        {
            var db = new UrbanDataContext();
            var list = (from d in db.RoomType orderby d.Name select new RadComboBoxItem {Text = d.Name, Value = d.Id.ToString()}).ToList();
            if (insertEmpty) list.Insert(0, new RadComboBoxItem("(None)", "NULL"));
            return list;
        }
    }
}