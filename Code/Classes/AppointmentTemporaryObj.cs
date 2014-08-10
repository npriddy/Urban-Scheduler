#region

using System;
using System.Collections.Generic;
using System.Linq;
using Urban.Data;
using UrbanSchedulerProject.Code.Classes;
using UrbanSchedulerProject.Code.Utilities.DB;

#endregion

namespace UrbanSchedulerProject.Code.Classes
{


    /// <summary>
    /// 
    /// </summary>
    public static class AppointmentTemporaryObjUtilities
    {
        /// <summary>
        /// Converts list of Room Availability objects to Appointment Temporary Objects
        /// </summary>
        /// <param name="avilList">The avil list.</param>
        /// <returns></returns>
        public static List<AppointmentTemporaryObj> ToAppointmentTempObject(this IEnumerable<RoomAvailability> avilList)
        {
            return avilList.Select(a => new AppointmentTemporaryObj
                                            {
                                                AllDay = a.AllDay,
                                                Days = a.Days, 
                                                EndDate = a.EndDate,
                                                EndTime = a.EndTime,
                                                RoomId = a.RoomID,
                                                StartDate = a.StartDate, 
                                                StartTime = a.StartTime
                                            }).ToList();
        }
    }

    /// <summary>
    ///     Used for storage of date objects in display of schedule controls
    /// </summary>
    [Serializable]
    public class AppointmentTemporaryObj
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref = "AppointmentTemporaryObj" /> class.
        /// </summary>
        public AppointmentTemporaryObj()
        {
            Id = Guid.NewGuid();
        }

        #region MetaData

        /// <summary>
        ///     Gets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the room ID.
        /// </summary>
        /// <value>
        ///     The room ID.
        /// </value>
        public int RoomId { get; set; }

        /// <summary>
        ///     Gets or sets the days.
        /// </summary>
        /// <value>
        ///     The days.
        /// </value>
        public string Days { get; set; }

        /// <summary>
        ///     Gets or sets the end date.
        /// </summary>
        /// <value>
        ///     The end date.
        /// </value>
        public DateTime? EndDate { get; set; }

        /// <summary>
        ///     Gets or sets the start date.
        /// </summary>
        /// <value>
        ///     The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///     Gets or sets the start time.
        /// </summary>
        /// <value>
        ///     The start time.
        /// </value>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        ///     Gets or sets the end time.
        /// </summary>
        /// <value>
        ///     The end time.
        /// </value>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        ///     Gets or sets all day.
        /// </summary>
        /// <value>
        ///     All day.
        /// </value>
        public bool? AllDay { get; set; }

        #endregion MetaData

        /// <summary>
        ///     Validates the list do any overlap in list.
        /// </summary>
        /// <param name = "apptList">The appt list.</param>
        /// <returns></returns>
        public static bool ValidateListDoAnyOverlapInList(IEnumerable<AppointmentTemporaryObj> apptList)
        {
            foreach (var d in apptList)
            {
                var list = new List<AppointmentTemporaryObj>(apptList);
                list.Remove(d);
                if (DoesDateOverLap(d, list))
                    return true;
            }
            return false;
        }

        /// <summary>
        ///     Overlapses the with.
        /// </summary>
        /// <param name = "a">A.</param>
        /// <param name = "b">The b.</param>
        /// <returns></returns>
        private static bool OverlapsWith(AppointmentTemporaryObj a, AppointmentTemporaryObj b)
        {
            if (b.EndDate == null || a.EndDate == null)
                return OverlapsWithNoEndDates(a, b);

            return !(((DateTime) b.EndDate).Add(b.EndTime) <= a.StartDate.Add(a.StartTime) || ((DateTime) a.EndDate).Add(a.EndTime) <= b.StartDate.Add(b.StartTime));
        }

        /// <summary>
        ///     Overlapses the with no end dates.
        /// </summary>
        /// <param name = "a">A.</param>
        /// <param name = "b">The b.</param>
        /// <returns></returns>
        private static bool OverlapsWithNoEndDates(AppointmentTemporaryObj a, AppointmentTemporaryObj b)
        {
            var aDays = RoomAvailabilityUtilities.ParseDayString(a.Days);
            var bDays = RoomAvailabilityUtilities.ParseDayString(b.Days);
            var overLapList = aDays.Where(bDays.Contains).ToList();

            if (overLapList.Count == 0)
                return false;

            //Compare start time vs end time
            return !(b.EndTime <= a.StartTime || a.EndTime <= b.StartTime);
        }

        /// <summary>
        ///     Doeses the date over lap.
        /// </summary>
        /// <param name = "newDate">The new date.</param>
        /// <param name = "currentList">The current list.</param>
        /// <returns></returns>
        public static bool DoesDateOverLap(AppointmentTemporaryObj newDate, IEnumerable<AppointmentTemporaryObj> currentList)
        {
            //End Dates Are known
            return newDate.EndDate != null ? ProcessDateOverLapWithEndDate(newDate, currentList) : ProcessNoEndDate(newDate, currentList);
        }

        /// <summary>
        ///     Processes the no end date.
        /// </summary>
        /// <param name = "newDate">The new date.</param>
        /// <param name = "currentList">The current list.</param>
        /// <returns></returns>
        private static bool ProcessNoEndDate(AppointmentTemporaryObj newDate, IEnumerable<AppointmentTemporaryObj> currentList)
        {
            //GetItemsWithNoEndDates
            var nonNullDateList = GetItemsWithEndDates(currentList);

            var dateToCheck = newDate;
            dateToCheck.EndDate = ReturnMaxDateInList(nonNullDateList);

            if (dateToCheck.EndDate != null && ProcessDateOverLapWithEndDate(dateToCheck, nonNullDateList))
            {
                return true;
            }

            var nullDateList = GetItemsWithNoEndDates(currentList);
            return nullDateList.Any(date => OverlapsWithNoEndDates(newDate, date));
        }

        /// <summary>
        ///     Processes the date over lap with end date.
        /// </summary>
        /// <param name = "newDate">The new date.</param>
        /// <param name = "currentList">The current list.</param>
        /// <returns></returns>
        private static bool ProcessDateOverLapWithEndDate(AppointmentTemporaryObj newDate, IEnumerable<AppointmentTemporaryObj> currentList)
        {
            var existinglist = new List<AppointmentTemporaryObj>();
            foreach (var date in currentList)
            {
                existinglist.AddRange(SplitOverMultpleDays(date));
            }

            var datetoCheck = new List<AppointmentTemporaryObj>();
            datetoCheck.AddRange(SplitOverMultpleDays(newDate));
            return datetoCheck.Any(date => GetOverLapList(existinglist, date));
        }

        /// <summary>
        ///     Gets the over lap list.
        /// </summary>
        /// <param name = "existinglist">The existinglist.</param>
        /// <param name = "date1">The date1.</param>
        /// <returns></returns>
        private static bool GetOverLapList(IEnumerable<AppointmentTemporaryObj> existinglist, AppointmentTemporaryObj date1)
        {
            return (from d in existinglist where OverlapsWith(d, date1) select d).Count() > 0 ? true : false;
        }

        /// <summary>
        ///     Returns the max date in list.
        /// </summary>
        /// <param name = "currentList">The current list.</param>
        /// <returns></returns>
        private static DateTime? ReturnMaxDateInList(ICollection<AppointmentTemporaryObj> currentList)
        {
            if (currentList.Count <= 0)
                return null;
            return currentList.Max(t => (DateTime) t.EndDate);
        }

        /// <summary>
        ///     Splits the over multple days.
        /// </summary>
        /// <param name = "appObj">The app obj.</param>
        /// <returns></returns>
        private static IEnumerable<AppointmentTemporaryObj> SplitOverMultpleDays(AppointmentTemporaryObj appObj)
        {
            var list = new List<AppointmentTemporaryObj>();

            //If Enddate is null or start date matches endate
            if (appObj.EndDate == null || appObj.StartDate == appObj.EndDate)
            {
                list.Add(appObj);
                return list;
            }
            var dayList = RoomAvailabilityUtilities.ParseDayString(appObj.Days);
            //Iterate over each date in range
            for (var date = appObj.StartDate; date.Date < appObj.EndDate; date = date.AddDays(1))
            {
                //If the current day doesn't exist in the current list of days then continue.
                if (dayList.Count > 0 && !dayList.Contains(date.DayOfWeek))
                    continue;

                //Create new Appoint Temp Obj
                list.Add(new AppointmentTemporaryObj
                             {
                                 AllDay = appObj.AllDay,
                                 Days = appObj.Days,
                                 EndDate = date,
                                 StartDate = date,
                                 StartTime = appObj.StartTime,
                                 EndTime = appObj.EndTime
                             });
            }

            return list;
        }

        /// <summary>
        ///     Gets the items with no end dates.
        /// </summary>
        /// <param name = "currentList">The current list.</param>
        /// <returns></returns>
        private static List<AppointmentTemporaryObj> GetItemsWithEndDates(IEnumerable<AppointmentTemporaryObj> currentList)
        {
            return (from d in currentList where d.EndDate != null select d).ToList();
        }

        /// <summary>
        ///     Gets the items with no end dates.
        /// </summary>
        /// <param name = "currentList">The current list.</param>
        /// <returns></returns>
        private static IEnumerable<AppointmentTemporaryObj> GetItemsWithNoEndDates(IEnumerable<AppointmentTemporaryObj> currentList)
        {
            return (from d in currentList where d.EndDate == null select d).ToList();
        }
    }
}