#region

using System;
using System.Data.Linq;
using CodeSmith.Data.Audit;
using UrbanSchedulerProject.Entities;

#endregion

namespace Urban.Data
{
    public partial class RoomReservation
    {
        #region Metadata

        // For more information about how to use the metadata class visit:
        // http://www.plinqo.com/metadata.ashx
        [Audit]
        internal class Metadata
        {
            // WARNING: Only attributes inside of this class will be preserved.

            public int Id { get; set; }

            public int RoomID { get; set; }

            public int ReserverUserID { get; set; }

            public bool? Approved { get; set; }

            public DateTime? RequestedDate { get; set; }

            public Room Room { get; set; }

            public User ReserverUser { get; set; }

            public EntitySet<RoomReservationComments> RoomReservationCommentsList { get; set; }

            public EntitySet<RoomReservationDates> RoomReservationDatesList { get; set; }
        }

        #endregion

        // Place custom code here.
    }
}