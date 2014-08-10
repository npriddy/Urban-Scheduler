#region

using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using CodeSmith.Data.Audit;

#endregion

namespace Urban.Data
{
    public partial class Files
    {
        #region Metadata

        // For more information about how to use the metadata class visit:
        // http://www.plinqo.com/metadata.ashx
        [Audit]
        internal class Metadata
        {
            // WARNING: Only attributes inside of this class will be preserved.

            public int Id { get; set; }

            [DataType(DataType.MultilineText)]
            public string FilesName { get; set; }

            public string Extension { get; set; }

            public string ServerFileName { get; set; }

            public int? FileSize { get; set; }

            public string FileType { get; set; }

            public string FileSubType { get; set; }

            public EntitySet<RoomImageLink> FileRoomImageLinkList { get; set; }
        }

        #endregion

        // Place custom code here.
    }
}