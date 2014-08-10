#region

using System;
using System.Collections.Generic;
using System.Linq;
using Urban.Data;
using UrbanSchedulerProject.Code.Classes;
using UrbanSchedulerProject.Code.Utilities.DB;

#endregion

namespace UrbanSchedulerProject.Code.Utilities
{
    ///<summary>
    /// Appointment Utilities top help with date ranges.
    ///</summary>
    public static class AppointmentUtilities
    {
        /// <summary>
        ///     Gets all reserved objects by date range and user id.
        /// </summary>
        /// <param name = "db">The db.</param>
        /// <param name = "startDate">The start date.</param>
        /// <param name = "endDate">The end date.</param>
        /// <param name = "userId">The user id.</param>
        /// <returns></returns>
        public static IEnumerable<AppointmentObj> GetAllReservedObjectsByDateRangeAndUserId(ref UrbanDataContext db, DateTime startDate, DateTime endDate, int userId)
        {
            var rList = RoomReservationDatesUtilities.GetAppointmentObjByStartDateEndDateUserId(ref db, startDate, endDate, userId);
            return rList.Select(r => new AppointmentObj
                                         {
                                             Busy = true,
                                             Days = r.Days,
                                             End = r.End,
                                             Start = r.Start,
                                             Subject = r.Subject
                                         }).ToList();
        }

        /// <summary>
        /// Gets all reserved objects by date range and user id.
        /// </summary>
        /// <param name="db">The db.</param>
        /// <param name="reservationId">The reservation id.</param>
        /// <returns></returns>
        public static IEnumerable<AppointmentObj> GetAllReservedObjectsByReservationId(ref UrbanDataContext db, int reservationId)
        {
            var rList = RoomReservationDatesUtilities.GetAppointmentObjByUserId(ref db, reservationId);
            return rList.Select(r => new AppointmentObj
            {
                Busy = true,
                Days = r.Days,
                End = r.End,
                Start = r.Start,
                Subject = r.Subject
            }).ToList();
        }

        /// <summary>
        ///     Gets the appointment objects by date range and room id.
        /// </summary>
        /// <param name = "db">The db.</param>
        /// <param name = "startDate">The start date.</param>
        /// <param name = "endDate">The end date.</param>
        /// <param name = "roomId">The room id.</param>
        /// <returns></returns>
        public static IEnumerable<AppointmentObj> GetAppointmentObjectsByDateRangeAndRoomId(ref UrbanDataContext db, DateTime startDate, DateTime endDate, int roomId)
        {
            var apptList = new List<AppointmentObj>();

            //Retrieve Reserved Dates For room and date range
            var rList = RoomReservationDatesUtilities.GetAppointmentObjByStartDateEndDateRoomId(ref db, startDate, endDate, roomId);
            //Retrieve Dates with recurring for start and end date with roomId
            var aList = RoomAvailabilityUtilities.GetAppointmentObjsWithRecurring(ref db, startDate, endDate, roomId);

            //Deal with date duplicates
            var processAvailList = ProcessAvailabilityDateOverLap(rList, aList);

            //Add Dates to list
            apptList.AddRange(rList);
            apptList.AddRange(processAvailList);

            //Order List by start date
            return apptList.OrderBy(t => t.Start).ToList();
        }


        /// <summary>
        ///     Processes the availability date over lap.
        /// </summary>
        /// <param name = "reservationList">The reservation list.</param>
        /// <param name = "availabilityList">The availability list.</param>
        /// <returns></returns>
        private static IEnumerable<AppointmentObj> ProcessAvailabilityDateOverLap(IEnumerable<AppointmentObj> reservationList, IEnumerable<AppointmentObj> availabilityList)
        {
            var apptList = new List<AppointmentObj>();
            foreach (var avail in availabilityList)
            {
                var avilForQuery = avail;
                //Check if current availability date overlaps with any reserved dates.
                var overLapList = (from d in reservationList where AppointmentObj.OverlapsWith(d, avilForQuery) select d).ToList();
                //If no overlap then simply add to list
                if (overLapList.Count == 0)
                    apptList.Add(avail);
                else
                {
                    //Overlap found handle breaking availability across reserved dates.
                    var splitList = SplitAvaiableDateFromOverlappingReservations(avail, overLapList);
                    apptList.AddRange(splitList);
                }
            }
            return apptList;
        }


        /// <summary>
        ///     Splits the avaiable date from overlapping reservations.
        /// </summary>
        /// <param name = "avail">The avail.</param>
        /// <param name = "overlappingDates">The overlapping dates.</param>
        /// <returns></returns>
        private static IEnumerable<AppointmentObj> SplitAvaiableDateFromOverlappingReservations(
            AppointmentObj avail, IEnumerable<AppointmentObj> overlappingDates)
        {
            var aList = new List<AppointmentObj>();
            //Stores current date placeholder
            DateTime? currentStart = null;
            //Stores how many hours are valid
            var currentHours = 1;
            //Iterates over times incrementing by 1 hour checking for overlap
            for (var date = avail.Start; date.Date < avail.End; date = date.AddHours(1))
            {
                //if date is >= the end date then break;
                if (date >= avail.End)
                    break;

                var dateForQuery = date;
                //Check current hour range 12-13 and detect any overlaps with reserved dates
                if (overlappingDates.Any(t => AppointmentObj.OverlapsWith(t, 
                    new AppointmentObj("Available", dateForQuery, dateForQuery.AddHours(1), "", "", "", avail.UserId, false, avail.DbId))))
                {
                    //Found Overlap
                    //Current date is set but now out of range
                    //Need to create appointment object);
                    if (currentStart != null)
                    {
                        aList.Add(new AppointmentObj("Available", ((DateTime) currentStart), 
                            ((DateTime) currentStart).AddHours(currentHours), "", "", "", avail.UserId, false, avail.DbId));
                        currentStart = null; //Reset
                        currentHours = 1; //Reset
                    }
                }
                else
                {
                    if (currentStart == null)
                        //New valid date range found
                        currentStart = date;
                    else
                        //Still current date range extend
                        currentHours++;
                }
            }
            //Finished looping valid date still stored
            if (currentStart != null)
            {
                aList.Add(new AppointmentObj("Available", ((DateTime) currentStart), 
                    ((DateTime) currentStart).AddHours(currentHours), "", "", "", avail.UserId, false, avail.DbId));
            }
            return aList;
        }
    }
}