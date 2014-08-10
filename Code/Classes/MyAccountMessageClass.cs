#region

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace UrbanSchedulerProject.Code.Classes
{
    /// <summary>
    /// Temporary Object for custom message display
    /// </summary>
    public class MyAccountMessageClass
    {
        /// <summary>
        ///     Prevents a default instance of the <see cref = "MyAccountMessageClass" /> class from being created.
        /// </summary>
        public MyAccountMessageClass(string message, DateTime datePosted, string roomNumber, string buildingTitle, string subject)
        {
            room = new Room {Number = roomNumber};
            building = new Building {Title = buildingTitle};
            Message = message;
            DatePosted = datePosted;
            Subject = subject;
        }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        /// <value>
        ///     The message.
        /// </value>
        [Required]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the date posted.
        /// </summary>
        /// <value>
        /// The date posted.
        /// </value>
        [Required]
        public DateTime DatePosted { get; set; }

        /// <summary>
        /// Gets or sets the room.
        /// </summary>
        /// <value>
        /// The room.
        /// </value>
        public Room room { get; set; }

        /// <summary>
        /// Gets or sets the building.
        /// </summary>
        /// <value>
        /// The building.
        /// </value>
        public Building building { get; set; }

        /// <summary>
        ///     Gets or sets the subject.
        /// </summary>
        /// <value>
        ///     The subject.
        /// </value>
        public string Subject { get; set; }

        #region Nested type: Building

        /// <summary>
        /// </summary>
        public class Building
        {
            /// <summary>
            /// Gets or sets the title.
            /// </summary>
            /// <value>
            /// The title.
            /// </value>
            public string Title { get; set; }
        }

        #endregion

        #region Nested type: Room

        /// <summary>
        /// 
        /// </summary>
        public class Room
        {
            /// <summary>
            ///     Gets or sets the number.
            /// </summary>
            /// <value>
            ///     The number.
            /// </value>
            public string Number { get; set; }
        }

        #endregion
    }
}