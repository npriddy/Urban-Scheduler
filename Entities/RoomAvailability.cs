﻿#region

using System;
using CodeSmith.Data.Audit;

#endregion

namespace Urban.Data
{
    public partial class RoomAvailability
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

            public string Days { get; set; }

            public DateTime StartDate { get; set; }

            public DateTime? EndDate { get; set; }

            public TimeSpan StartTime { get; set; }

            public TimeSpan EndTime { get; set; }

            public bool? AllDay { get; set; }

            public Room Room { get; set; }
        }

        #endregion

        // Place custom code here.
    }
}