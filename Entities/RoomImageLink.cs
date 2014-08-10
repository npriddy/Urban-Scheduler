#region

using CodeSmith.Data.Audit;

#endregion

namespace Urban.Data
{
    public partial class RoomImageLink
    {
        #region Metadata

        // For more information about how to use the metadata class visit:
        // http://www.plinqo.com/metadata.ashx
        [Audit]
        internal class Metadata
        {
            // WARNING: Only attributes inside of this class will be preserved.

            public int Id { get; set; }

            public int FileID { get; set; }

            public int RoomID { get; set; }

            public int RoomImageType { get; set; }

            public string Title { get; set; }

            public string ImageDescription { get; set; }

            public Files FileFiles { get; set; }

            public Room Room { get; set; }

            public RoomImageType RoomImageType1 { get; set; }
        }

        #endregion

        // Place custom code here.
    }
}