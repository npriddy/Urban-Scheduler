#region

using System;
using System.Linq;
using System.Text;
using Urban.Data;

#endregion

namespace UrbanSchedulerProject.Code.Utilities.DB
{
    /// <summary>
    /// </summary>
    public static class RoomCommentsUtilities
    {
        /// <summary>
        ///     Gets the rating for room.
        /// </summary>
        /// <param name = "roomId">The room id.</param>
        /// <returns></returns>
        public static int GetRatingForRoomAndComments(int roomId)
        {
            var db = new UrbanDataContext();
            var commentList = db.Manager.RoomComments.GetByRoomID(roomId).ToList();

            var sum = commentList.Sum(t => t.Score);
            var num = commentList.Count();

            if (num <= 0)
                return 0;

            try
            {
                return (int) sum/num;
            }
            catch (DivideByZeroException)
            {
                return 0;
            }
        }

        /// <summary>
        ///     Builds the comments for room.
        /// </summary>
        /// <param name = "roomId">The room id.</param>
        /// <returns></returns>
        public static string BuildCommentsForRoom(int roomId)
        {
            var sb = new StringBuilder();
            var db = new UrbanDataContext();
            var commentList = db.Manager.RoomComments.GetByRoomID(roomId).OrderByDescending(t => t.DatePosted);
            var count = 0;
            foreach (var c in commentList)
            {
                if (count > 0)
                    sb.Append("<br />");
                sb.Append(String.Format("<strong>Rating: {0} / 5 </strong><br />", c.Score));
                if(c.DatePosted != null)
                    sb.AppendLine(String.Format("<strong>Date Posted: {0} </strong><br />", ((DateTime)c.DatePosted).ToShortDateString()));
                sb.AppendLine(String.Format("<strong>Comments:</strong> {0}<br />", c.Comments));
                count++;
            }


            return sb.ToString();
        }
    }
}