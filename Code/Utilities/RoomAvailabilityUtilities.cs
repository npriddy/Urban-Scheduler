using System;
using System.Collections.Generic;
using System.Linq;
using Urban.Data;
using UrbanSchedulerProject.Code.Classes;

namespace UrbanSchedulerProject.Code.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public static class RoomAvailabilityUtilities
    {
        /// <summary>
        /// End date needs to have time = 23 and start time = 0 or 1
        /// </summary>
        /// <param name="db">The db.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="roomId">The room id.</param>
        /// <returns></returns>
        public static IEnumerable<AppointmentObj> GetAppointmentObjsWithRecurring(ref UrbanDataContext db, DateTime startDate, DateTime endDate, int roomId)
        {
            var apptList = new List<AppointmentObj>();
            var aList = db.Manager.RoomAvailability.GetByRoomAndDateRange(startDate, endDate, roomId);

            if (aList.Any(t => t.EndDate != null && (t.StartDate.Add(t.StartTime) >= ((DateTime) t.EndDate).Add(t.EndTime) || ((DateTime) t.EndDate).Add(t.EndTime) <= t.StartDate.Add(t.StartTime))))
            {
                throw new Exception("Date Ranges Are Not Valid");
            }

            foreach (var date in aList)
            {
                if (date.Days == null) // Non Recurring Dates
                {
                    apptList.Add(new AppointmentObj("Available", DateUtil.CombineDateAndTime(date.StartDate, date.StartTime), DateUtil.CombineDateAndTime(date.EndDate, date.EndTime), "", "", "", date.Room.UserID, false, date.Id));
                }
                else // Recurring Dates
                {
                    var reccuringApptDates = CalculateRecurringFromAvailble(date, endDate);
                    apptList.AddRange(reccuringApptDates);
                }
            }
            return apptList;
        }

        /// <summary>
        /// Parses the day string.
        /// </summary>
        /// <param name="days">The days.</param>
        /// <returns></returns>
        private static List<DayOfWeek> ParseDayString(string days)
        {
            // sun, mon, tue, wed, thur, fri, sat
            var dayList = new List<DayOfWeek>();
            var array = days.Split(',');
            foreach (var d in array)
            {
                switch (d.ToLower())
                {
                    case "sun":
                        {
                            dayList.Add(DayOfWeek.Sunday);
                            break;
                        }
                    case "mon":
                        {
                            dayList.Add(DayOfWeek.Monday);
                            break;
                        }
                    case "tue":
                        {
                            dayList.Add(DayOfWeek.Tuesday);
                            break;
                        }
                    case "wed":
                        {
                            dayList.Add(DayOfWeek.Wednesday);
                            break;
                        }
                    case "thur":
                        {
                            dayList.Add(DayOfWeek.Thursday);
                            break;
                        }
                    case "fri":
                        {
                            dayList.Add(DayOfWeek.Friday);
                            break;
                        }
                    case "sat":
                        {
                            dayList.Add(DayOfWeek.Saturday);
                            break;
                        }
                }
            }
            return dayList;
        }

        /// <summary>
        /// Calculates the recurring from availble.
        /// </summary>
        /// <param name="avail">The avail.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        private static IEnumerable<AppointmentObj> CalculateRecurringFromAvailble(RoomAvailability avail, DateTime endDate)
        {
            var dayList = ParseDayString(avail.Days);

            var apptList = new List<AppointmentObj>();
            //recurring dates
            var days = 0;
            for (var date = avail.StartDate.Add(avail.StartTime); date.Date < endDate; date = date.AddDays(1))
            {
                if (dayList.Contains(date.DayOfWeek)) //Handle day of the week recurring
                    apptList.Add(new AppointmentObj("Available", avail.StartDate.Add(avail.StartTime).AddDays(days), avail.StartDate.Add(avail.EndTime).AddDays(days), "", "", "", avail.Room.UserID, false, avail.Id));

                days++;
            }

            return apptList;
        }
    }
}