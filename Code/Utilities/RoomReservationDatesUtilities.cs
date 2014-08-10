#region

using System;
using System.Collections.Generic;
using System.Linq;
using Urban.Data;
using UrbanSchedulerProject.Code.Classes;

#endregion

namespace UrbanSchedulerProject.Code.Utilities
{
    /// <summary>
    /// Helps calculate reservation dates over a range.
    /// </summary>
    public static class RoomReservationDatesUtilities
    {
        /// <summary>
        ///     Gets the appointment obj by start date end date room id.
        /// </summary>
        /// <param name = "db">The db.</param>
        /// <param name = "startDate">The start date.</param>
        /// <param name = "endDate">The end date.</param>
        /// <param name = "roomId">The room id.</param>
        /// <returns></returns>
        public static IEnumerable<AppointmentObj> GetAppointmentObjByStartDateEndDateRoomId(ref UrbanDataContext db, DateTime startDate, DateTime endDate, int roomId)
        {
            var rList = (from d in db.RoomReservationDates
                         where d.RoomReservation.RoomID == roomId && d.StartDate >= startDate && d.EndDate <= endDate
                         select new AppointmentObj("Reserved", d.StartDate, d.EndDate, "", "", "", d.RoomReservation.ReserverUserID, true, d.Id)).ToList();

            if (rList.Any(t => t.Start >= t.End || t.End <= t.Start))
            {
                throw new Exception("Date Ranges Are Not Valid");
            }

            return rList;
        }

        /// <summary>
        /// Gets the appointment obj by start date end date user Id
        /// </summary>
        /// <param name="db">The db.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static IEnumerable<AppointmentObj> GetAppointmentObjByStartDateEndDateUserId(ref UrbanDataContext db, DateTime startDate, DateTime endDate, int userId)
        {
            var rList = (from d in db.RoomReservationDates
                         where d.RoomReservation.ReserverUserID == userId &&
                               d.StartDate >= startDate && d.EndDate <= endDate 
                         select new AppointmentObj("Reserved", d.StartDate, d.EndDate, "", "", "", d.RoomReservation.ReserverUserID, true, d.Id)).ToList();

            if (rList.Any(t => t.Start >= t.End || t.End <= t.Start))
            {
                throw new Exception("Date Ranges Are Not Valid");
            }

            return rList;
        }

        /// <summary>
        /// Gets the appointment obj by start date end date user Id
        /// </summary>
        /// <param name="db">The db.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="roomId">The room id.</param>
        /// <returns></returns>
        public static IEnumerable<AppointmentObj> GetAppointmentObjByUserId(ref UrbanDataContext db, int reservationId)
        {
            var rList = (from d in db.RoomReservationDates
                         where d.RoomReservation.Id == reservationId

                         select new AppointmentObj("Reserved", d.StartDate, d.EndDate, "", "", "", d.RoomReservation.ReserverUserID, true, d.Id)).ToList();

            if (rList.Any(t => t.Start >= t.End || t.End <= t.Start))
            {
                throw new Exception("Date Ranges Are Not Valid");
            }

            return rList;
        }
    }
}