#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urban.Data;
using UrbanSchedulerProject.Code.Classes;
using UrbanSchedulerProject.Code.Utilities.EmailTemplates;

#endregion

namespace UrbanSchedulerProject.Code.Utilities.DB
{
    /// <summary>
    /// </summary>
    public static class RoomReservationUtilities
    {
        /// <summary>
        ///     Creates the room reservation.
        /// </summary>
        /// <param name = "userId">The user id.</param>
        /// <param name = "roomId">The room id.</param>
        /// <param name = "comments">The comments.</param>
        /// <returns></returns>
        public static int CreateRoomReservation(int userId, int roomId, string comments, List<ReserveRoomTempObject> timeList)
        {
            if (timeList.Count <= 0)
                return -1;

            var db = new UrbanDataContext();

            var reservation = new RoomReservation
                                  {
                                      Approved = null,
                                      ReserverUserID = userId,
                                      RoomID = roomId,
                                      RequestedDate = DateTime.Now
                                  };
            db.RoomReservation.InsertOnSubmit(reservation);
            db.SubmitChanges();
            //Insert Dates

            foreach (var rTime in timeList.Select(r => new RoomReservationDates
                                                           {
                                                               AllDay = false, 
                                                               EndDate = r.Date.Add(r.End),
                                                               StartDate = r.Date.Add(r.Start),
                                                               RoomReservationID = reservation.Id
                                                           }))
            {
                db.RoomReservationDates.InsertOnSubmit(rTime);
            }

            if (comments.Trim() != string.Empty)
            {
                var revComments = new RoomReservationComments
                                      {
                                          Comments = comments.Trim(),
                                          DateSent = DateTime.Now,
                                          RoomReservationID = reservation.Id,
                                          UserID = userId
                                      };
                db.RoomReservationComments.InsertOnSubmit(revComments);
            }
            db.SubmitChanges();
            var room = db.Manager.Room.GetByKey(roomId);
            var user = db.Manager.User.GetByKey(room.UserID);
            RoomReservationEmailUtilities.InitiateRoomReservationRequest(room, user, reservation.Id, comments);
            return 1;
        }

        /// <summary>
        ///     Converts the comments to string.
        /// </summary>
        /// <param name = "rev">The rev.</param>
        /// <returns></returns>
        public static string ConvertCommentsToString(ref RoomReservation rev)
        {
            var sb = new StringBuilder();
            foreach (var r in rev.RoomReservationCommentsList.OrderByDescending(t => t.DateSent))
            {
                sb.Append(String.Format("<strong>Person: </strong> {0} <br />", r.UserID == r.RoomReservation.Room.UserID ? "Room Poster" : "Room Requester"));
                sb.Append(String.Format("<strong>Date Posted: </strong> {0} <br />", r.DateSent));
                sb.Append(String.Format("<strong>Comments: </strong> {0} <br /> <br />", r.Comments));
            }
            return sb.ToString();
        }
    }
}