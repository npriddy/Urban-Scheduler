using System;

namespace UrbanSchedulerProject.Code.Classes
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ReserveRoomTempObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReserveRoomTempObject"/> class.
        /// </summary>
        public ReserveRoomTempObject()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }
        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public TimeSpan Start { get; set; }
        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public TimeSpan End { get; set; }


        /// <summary>
        /// Toes the temp appointment obj.
        /// </summary>
        /// <returns></returns>
        public AppointmentObj ToAppointmentObj()
        {
            return new AppointmentObj
                       {

                           Days = String.Empty,
                           Busy = true,
                           End = Date.Add(End),
                           Start = Date.Add(Start)                          
                       };
        }
    }
}