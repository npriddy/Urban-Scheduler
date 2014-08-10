#region

using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using CodeSmith.Data.Audit;
using UrbanSchedulerProject.Entities;

#endregion

namespace Urban.Data
{
    public partial class Building
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

            public string Name { get; set; }

            [Required]
            public string PrimaryAddress { get; set; }

            public string SecondaryAddress { get; set; }

            [Required]
            public string City { get; set; }

            [Required]
            public string State { get; set; }

            public string Longitude { get; set; }

            public string Latitude { get; set; }

            [Required]
            public string Zip { get; set; }

            public bool HasMoreThanOneRoom { get; set; }

            public User User { get; set; }

            public EntitySet<Room> RoomList { get; set; }
        }

        #endregion

        // Place custom code here.
    }
}