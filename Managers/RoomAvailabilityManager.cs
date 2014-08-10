#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Urban.Data
{
    public partial class RoomAvailabilityManager
    {
        #region Query

        // A private class for lazy loading static compiled queries.
        private static partial class Query
        {
            // Add your compiled queries here. 
        }

        #endregion

        /// <summary>
        ///     Gets the by room and date range.
        /// </summary>
        /// <param name = "startDate">The start date.</param>
        /// <param name = "endDate">The end date.</param>
        /// <param name = "roomId">The room id.</param>
        /// <returns></returns>
        public List<RoomAvailability> GetByRoomAndDateRange(DateTime startDate, DateTime endDate, int roomId)
        {
            var nonNullList = (from d in Entity
                    where d.RoomID == roomId && d.EndDate != null && d.StartDate >= startDate && d.EndDate <= endDate
                    select d).ToList();

            var nullIst = (from d in Entity
                           where d.RoomID == roomId && d.EndDate == null && d.StartDate <= startDate
                           select d).ToList();

            var longDatesList = (from d in Entity where d.RoomID == roomId && d.EndDate != null && d.StartDate <= startDate && endDate >= endDate select d).ToList();

            var list = new List<RoomAvailability>();
            list.AddRange(nonNullList);
            list.AddRange(nullIst);
            list.AddRange(longDatesList);
            list = list.Distinct().ToList();
            return list;
        }
    }
}