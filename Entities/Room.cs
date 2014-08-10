#region

using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using CodeSmith.Data.Audit;
using UrbanSchedulerProject.Entities;

#endregion

namespace Urban.Data
{
    public partial class Room
    {
        #region Metadata

        // For more information about how to use the metadata class visit:
        // http://www.plinqo.com/metadata.ashx
        [Audit]
        internal class Metadata
        {
            // WARNING: Only attributes inside of this class will be preserved.

            public int Id { get; set; }

            public int UserID { get; set; }

            public int? BuildingID { get; set; }

            public int RoomTypeID { get; set; }

            [Required]
            public string Number { get; set; }

            public int MaxOccupancy { get; set; }

            [Required]
            public string Title { get; set; }

            public string Description { get; set; }

            public Building Building { get; set; }

            public RoomType RoomType { get; set; }

            public User User { get; set; }

            public EntitySet<RoomAvailability> RoomAvailabilityList { get; set; }

            public EntitySet<RoomComments> RoomCommentsList { get; set; }

            public EntitySet<RoomImageLink> RoomImageLinkList { get; set; }

            public EntitySet<RoomReservation> RoomReservationList { get; set; }
        }

        #endregion

        // Place custom code here.
    }
}