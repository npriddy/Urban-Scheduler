#region

using System.Data.Linq;
using CodeSmith.Data.Audit;

#endregion

namespace Urban.Data
{
    public partial class RoomType
    {
        #region Metadata

        // For more information about how to use the metadata class visit:
        // http://www.plinqo.com/metadata.ashx
        [Audit]
        internal class Metadata
        {
            // WARNING: Only attributes inside of this class will be preserved.

            public int Id { get; set; }

            public string Name { get; set; }

            public EntitySet<Room> RoomList { get; set; }
        }

        #endregion

        // Place custom code here.
    }
}