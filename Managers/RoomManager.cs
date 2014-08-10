#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Urban.Data
{
    public partial class RoomManager
    {
        #region Query

        // A private class for lazy loading static compiled queries.
        private static partial class Query
        {
            // Add your compiled queries here. 
        }

        #endregion

        public int GetCountForAll()
        {
            return Entity.Count();
        }

        ///// <summary>
        ///// Gets all for search.
        ///// </summary>
        ///// <param name="zip">The zip.</param>
        ///// <param name="city">The city.</param>
        ///// <param name="state">The state.</param>
        ///// <returns></returns>
        //public List<Room> GetAllForSearch(string zip, string city, string state)
        //{
        //    return (from d in Entity
        //            where (zip != String.Empty && d.Building.Zip.Trim() == zip.Trim()) ||
        //                  (city != String.Empty && d.Building.City.Trim() == city.Trim()) ||
        //                  (state != String.Empty && d.Building.State.Trim() == state.Trim())
        //            select d).ToList();
        //}


        /// <summary>
        /// Gets all for search.
        /// </summary>
        /// <param name="zip">The zip.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public List<Room> GetAllForSearch(string zip, string city, string state, DateTime? startDate, DateTime? endDate)
        {
            var rList =  (from d in Entity
                    where (zip == String.Empty || d.Building.Zip.Trim() == zip.Trim()) &&
                          (city == String.Empty || d.Building.City.Trim() == city.Trim()) &&
                          (state == String.Empty || d.Building.State.Trim() == state.Trim())
                    select d).ToList();

            if (startDate == null || endDate == null)
                return rList;

            var returnList = new List<Room>();
            UrbanDataContext db = new UrbanDataContext();
            foreach(var room in rList)
            {
                var apptList = UrbanSchedulerProject.Code.Utilities.AppointmentUtilities.GetAppointmentObjectsByDateRangeAndRoomId(ref db, (DateTime)startDate, (DateTime)endDate, room.Id);
                  if(apptList.Count() > 0)
                      returnList.Add(room);
            }
            return returnList;
        }

        /// <summary>
        ///     Gets the by user id.
        /// </summary>
        /// <param name = "userId">The user id.</param>
        /// <returns></returns>
        public List<Room> GetByUserId(int userId)
        {
            return (from d in Entity
                    where d.Building.UserID == userId
                    select d).ToList();
        }
    }
}