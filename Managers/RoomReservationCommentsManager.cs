#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace Urban.Data
{
    public partial class RoomReservationCommentsManager
    {
        #region Query

        // A private class for lazy loading static compiled queries.
        private static partial class Query
        {
            // Add your compiled queries here. 
        }

        #endregion

        /// <summary>
        /// Gets all for search.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public IEnumerable<RoomReservationComments> GetAllForSearch(int userId)
        {
            return (from d in Entity
                    where d.UserID == userId || d.RoomReservation.Room.UserID == userId
                    select d).ToList();
        }
    }
}