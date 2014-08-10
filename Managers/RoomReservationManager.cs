#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace Urban.Data
{
    public partial class RoomReservationManager
    {
        #region Query

        // A private class for lazy loading static compiled queries.
        private static partial class Query
        {
            // Add your compiled queries here. 

           
        }

        #endregion

        public int GetAllCount()
        {
            return Entity.Count();
        }

        /// <summary>
        ///     Gets all for search.
        /// </summary>
        /// <param name = "userId">The user id.</param>
        /// <param name = "open">if set to <c>true</c> [open].</param>
        /// <returns></returns>
        public List<RoomReservation> GetAllForSearch(int userId, bool open)
        {
            return (from d in Entity
                    where (d.Room.UserID == userId || d.ReserverUserID == userId) && ((open && d.Approved == null) || (open == false && d.Approved != null))
                    select d).ToList();
        }
    }
}