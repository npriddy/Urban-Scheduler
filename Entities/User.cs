#region

using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using CodeSmith.Data.Audit;
using Urban.Data;

#endregion

namespace UrbanSchedulerProject.Entities
{
    public partial class User
    {
        #region Metadata

        // For more information about how to use the metadata class visit:
        // http://www.plinqo.com/metadata.ashx
        [Audit]
        internal class Metadata
        {
            // WARNING: Only attributes inside of this class will be preserved.

            public int Id { get; set; }

            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            public bool? IsUserAuthenticated { get; set; }

            public bool IsAdmin { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }

            public string PrimaryAddress { get; set; }

            [DataType(DataType.MultilineText)]
            public string SecondaryAddress { get; set; }

            public string City { get; set; }

            public string Zip { get; set; }

            public string UserName { get; set; }

            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            public string PasswordSalt { get; set; }

            public DateTime? DateCreated { get; set; }

            public Guid? ActivationGuid { get; set; }

            public Room Room { get; set; }

            public EntitySet<Building> BuildingList { get; set; }

            public EntitySet<RoomComments> RoomCommentsList { get; set; }

            public EntitySet<RoomReservation> ReserverRoomReservationList { get; set; }

            public EntitySet<RoomReservationComments> RoomReservationCommentsList { get; set; }
        }

        #endregion

        // Place custom code here.
    }
}