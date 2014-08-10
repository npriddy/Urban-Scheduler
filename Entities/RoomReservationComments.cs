#region

using System;
using CodeSmith.Data.Audit;
using UrbanSchedulerProject.Entities;

#endregion

namespace Urban.Data
{
    public partial class RoomReservationComments
    {
        #region Metadata

        // For more information about how to use the metadata class visit:
        // http://www.plinqo.com/metadata.ashx
        [Audit]
        internal class Metadata
        {
            // WARNING: Only attributes inside of this class will be preserved.

            public int Id { get; set; }

            public int RoomReservationID { get; set; }

            public int UserID { get; set; }

            public DateTime DateSent { get; set; }

            public string Comments { get; set; }

            public RoomReservation RoomReservation { get; set; }

            public User User { get; set; }
        }

        #endregion

        // Place custom code here.
    }
}