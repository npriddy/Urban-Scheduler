#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace Urban.Data
{
    public partial class RoomCommentsManager
    {
        #region Query

        // A private class for lazy loading static compiled queries.
        private static partial class Query
        {
            // Add your compiled queries here. 
        }

        #endregion

        /// <summary>
        ///     Gets the by user id.
        /// </summary>
        /// <param name = "userId">The user id.</param>
        /// <returns></returns>
        public IEnumerable<RoomComments> GetByRoomUserId(int userId)
        {
            return (from d in Entity
                    where d.Room.UserID == userId
                    select d).ToList();
        }
    }
}