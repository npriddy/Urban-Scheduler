#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Urban.Data;
using UrbanSchedulerProject.Code.Classes;

#endregion

namespace UrbanSchedulerProject.Code.Utilities.DB
{
    /// <summary>
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
        public static List<AppointmentObj> GetAppointmentObjsWithRecurring(ref UrbanDataContext db, DateTime startDate, 
            DateTime endDate, int roomId)
        {
            var apptList = new List<AppointmentObj>();
            var aList = db.Manager.RoomAvailability.GetByRoomAndDateRange(startDate, endDate, roomId);

            if (aList.Any(t => t.EndDate != null && (t.StartDate.Add(t.StartTime) >= ((DateTime) t.EndDate).Add(t.EndTime) || 
                ((DateTime) t.EndDate).Add(t.EndTime) <= t.StartDate.Add(t.StartTime))))
            {
                throw new Exception("Date Ranges Are Not Valid");
            }

            foreach (var date in aList)
            {
                if (date.Days == null) // Non Recurring Dates
                {
                    if (date.EndDate == null) continue;
                   
                    if (date.EndDate > date.StartDate.Date)
                    {
                        var reccuringApptDates = CalculateRecurringFromAvailble(date, (DateTime)date.EndDate).Distinct();
                        apptList.AddRange(reccuringApptDates);
                    }
                    else apptList.Add(new AppointmentObj("Available", Utilities.CombineDateAndTime(date.StartDate, date.StartTime),
                        Utilities.CombineDateAndTime(date.EndDate, date.EndTime), "", "", "", date.Room.UserID, false, date.Id));
                }
                else // Recurring Dates
                {
                    var endingDate = endDate; //If end date is null use end date requested
                    if (date.EndDate != null && date.EndDate <= endDate)
                        endingDate = (DateTime) date.EndDate;

                    var reccuringApptDates = CalculateRecurringFromAvailble(date, endingDate).Distinct();
                    apptList.AddRange(reccuringApptDates);
                }
            }
            return apptList;
        }

        /// <summary>
        ///     Parses the day string.
        /// </summary>
        /// <param name = "days">The days.</param>
        /// <returns></returns>
        public static List<DayOfWeek> ParseDayString(string days)
        {
            // sun, mon, tue, wed, thur, fri, sat
            var dayList = new List<DayOfWeek>();
            if(!days.Contains(","))
            {
                return new List<DayOfWeek> {DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday};
            }

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
        ///     Calculates the recurring from availble.
        /// </summary>
        /// <param name = "avail">The avail.</param>
        /// <param name = "endDate">The end date.</param>
        /// <returns></returns>
        private static IEnumerable<AppointmentObj> CalculateRecurringFromAvailble(RoomAvailability avail, DateTime endDate)
        {
            var dayList = ParseDayString(avail.Days ?? String.Empty);

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