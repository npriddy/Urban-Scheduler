#region

using System;
using Telerik.Web.UI;

#endregion

namespace UrbanSchedulerProject.Code.Classes
{
    /// <summary>
    /// Used for scheduler control display container for AvailabilityDates and Reservation Dates
    /// </summary>
    [Serializable]
    public class AppointmentObj
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="AppointmentObj"/> class from being created.
        /// </summary>
        public AppointmentObj()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref = "AppointmentObj" /> class.
        /// </summary>
        /// <param name = "subject">The subject.</param>
        /// <param name = "start">The start.</param>
        /// <param name = "end">The end.</param>
        /// <param name = "recurrenceRule">The recurrence rule.</param>
        /// <param name = "recurrenceParentId">The recurrence parent ID.</param>
        /// <param name = "reminder">The reminder.</param>
        /// <param name = "userId">The user ID.</param>
        /// <param name = "busy">if set to <c>true</c> [busy].</param>
        /// <param name = "dbId">The db id.</param>
        public AppointmentObj(string subject, DateTime start, DateTime end, string recurrenceRule,
                              string recurrenceParentId, string reminder, int? userId, bool busy, int dbId) : this()
        {
            Id = Guid.NewGuid().ToString();
            Subject = subject;
            Start = start;
            End = end;
            RecurrenceRule = recurrenceRule;
            RecurrenceParentId = recurrenceParentId;
            Reminder = reminder;
            UserId = userId;
            Busy = busy;
            DbId = dbId;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref = "AppointmentObj" /> class.
        /// </summary>
        /// <param name = "source">The source.</param>
        public AppointmentObj(Appointment source) : this()
        {
            CopyInfo(source);
        }


        /// <summary>
        /// Gets the id.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets or sets the db id.
        /// </summary>
        /// <value>
        /// The db id.
        /// </value>
        public int DbId { get; set; }

        ///<summary>
        ///</summary>
        public string Subject { get; set; }

        ///<summary>
        ///</summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public DateTime End { get; set; }

        /// <summary>
        /// Gets or sets the recurrence rule.
        /// </summary>
        /// <value>
        /// The recurrence rule.
        /// </value>
        public string RecurrenceRule { get; set; }

        /// <summary>
        /// Gets or sets the recurrence parent id.
        /// </summary>
        /// <value>
        /// The recurrence parent id.
        /// </value>
        public string RecurrenceParentId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the reminder.
        /// </summary>
        /// <value>
        /// The reminder.
        /// </value>
        public string Reminder { get; set; }

        /// <summary>
        /// Gets or sets the busy.
        /// </summary>
        /// <value>
        /// The busy.
        /// </value>
        public bool? Busy { get; set; }

        /// <summary>
        /// Gets or sets the days.
        /// </summary>
        /// <value>
        /// The days.
        /// </value>
        public string Days { get; set; }

        /// <summary>
        ///     Overlapses the with.
        /// </summary>
        /// <param name = "a">A.</param>
        /// <param name = "b">The b.</param>
        /// <returns></returns>
        public static bool OverlapsWith(AppointmentObj a, AppointmentObj b)
        {
            return !(b.End <= a.Start || a.End <= b.Start);
        }

        /// <summary>
        /// Overlapses the with.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public bool OverlapsWith(AppointmentObj b)
        {
            return !(b.End <= Start || End <= b.Start);
        }

        /// <summary>
        /// Alloweds to add.
        /// </summary>
        /// <param name="toCompare">To compare.</param>
        /// <returns></returns>
        public bool AllowedToAdd(AppointmentObj toCompare)
        {
            if (Start <= toCompare.End && End >= toCompare.Start)
                return true;
            else return false;
        }

        /// <summary>
        ///     Copies the info.
        /// </summary>
        /// <param name = "source">The source.</param>
        public void CopyInfo(Appointment source)
        {
            Subject = source.Subject;
            Start = source.Start;
            End = source.End;

            var busy = source.Resources.GetResourceByType("Busy");
            if (busy != null)
                Busy = (bool) busy.Key;
            else Busy = null;
        }
    }
}