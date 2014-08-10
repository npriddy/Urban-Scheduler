#region

using System.Collections.Generic;
using Urban.Data;
using UrbanSchedulerProject.Entities;

#endregion

namespace UrbanSchedulerProject.Code.Utilities.EmailTemplates
{
    /// <summary>
    /// Reservation template utilities
    /// </summary>
    public static class RoomReservationEmailUtilities
    {
        /// <summary>
        ///     Sends new user account email with activation key
        /// </summary>
        /// <param name = "room">The room.</param>
        /// <param name = "u">The u.</param>
        /// <param name = "roomReservationId">The room reservation id.</param>
        /// <param name = "comments">The comments.</param>
        /// <datetime>6/11/2011-11:42 AM</datetime>
        /// <author>
        ///     nate
        /// </author>
        public static void InitiateRoomReservationRequest(Room room, User u, int roomReservationId, string comments)
        {
            var keys = new List<KeyValuePair<string, string>>
                           {
                               new KeyValuePair<string, string>("{!MainTitleHtml}", "Room Reservation Requested"),
                               new KeyValuePair<string, string>("{!Subtitle01Html}", "Action Required"),
                               new KeyValuePair<string, string>("{!Subtitle01TextHtml}", "A user has requested reservation of room: " + room.Number + " in " + room.Building.Name + " with the following comments " + comments),
                               new KeyValuePair<string, string>("{!LinkHtml}", "http://UrbanScheduler.com?RoomReservationKey=" + roomReservationId),
                               new KeyValuePair<string, string>("{!LinkTextHtml}", "Click Here To View / Approve / or Deny"),
                               new KeyValuePair<string, string>("{!FormPerson}", "UrbanScheduler System")
                           };

            EmailTemplateUtilities.EmailFromFromMaster(keys, "UrbanScheduler Room Reservation Requested", u, new List<int>());
        }

        /// <summary>
        ///     Sends new user account email with activation key
        /// </summary>
        /// <param name = "room">The room.</param>
        /// <param name = "u">The u.</param>
        /// <param name = "roomReservationId">The room reservation id.</param>
        /// <param name = "comments">The comments.</param>
        /// <datetime>6/11/2011-11:42 AM</datetime>
        /// <author>
        ///     nate
        /// </author>
        public static void RoomReservationCommentsSentRoomPoster(Room room, User u, int roomReservationId, string comments)
        {
            var keys = new List<KeyValuePair<string, string>>
                           {
                               new KeyValuePair<string, string>("{!MainTitleHtml}", "Room Reservation Comments From Room Poster"),
                               new KeyValuePair<string, string>("{!Subtitle01Html}", "Comments Posted"),
                               new KeyValuePair<string, string>("{!Subtitle01TextHtml}", "Room poster for room: " + room.Number + " in " + room.Building.Name + " has posted the following comments " + comments),
                               new KeyValuePair<string, string>("{!LinkHtml}", "http://UrbanScheduler.com?RoomReservationKey=" + roomReservationId),
                               new KeyValuePair<string, string>("{!LinkTextHtml}", "Click Here To View"),
                               new KeyValuePair<string, string>("{!FormPerson}", "UrbanScheduler System")
                           };

            EmailTemplateUtilities.EmailFromFromMaster(keys, "UrbanScheduler Room Reservation Comments Posted From Room Poster", u, new List<int>());
        }

        /// <summary>
        ///     Sends new user account email with activation key
        /// </summary>
        /// <param name = "room">The room.</param>
        /// <param name = "u">The u.</param>
        /// <param name = "roomReservationId">The room reservation id.</param>
        /// <param name = "comments">The comments.</param>
        /// <datetime>6/11/2011-11:42 AM</datetime>
        /// <author>
        ///     nate
        /// </author>
        public static void RoomReservationCommentsSentRoomRequestor(Room room, User u, int roomReservationId, string comments)
        {
            var keys = new List<KeyValuePair<string, string>>
                           {
                               new KeyValuePair<string, string>("{!MainTitleHtml}", "Room Reservation Comments From Requestor"),
                               new KeyValuePair<string, string>("{!Subtitle01Html}", "Comments Posted"),
                               new KeyValuePair<string, string>("{!Subtitle01TextHtml}", "Room requestor for room: " + room.Number + " in " + room.Building.Name + " has posted the following comments " + comments),
                               new KeyValuePair<string, string>("{!LinkHtml}", "http://UrbanScheduler.com?RoomReservationKey=" + roomReservationId),
                               new KeyValuePair<string, string>("{!LinkTextHtml}", "Click Here To View"),
                               new KeyValuePair<string, string>("{!FormPerson}", "UrbanScheduler System")
                           };

            EmailTemplateUtilities.EmailFromFromMaster(keys, "UrbanScheduler Room Reservation Comments Posted", u, new List<int>());
        }

        /// <summary>
        ///     Sends new user account email with activation key
        /// </summary>
        /// <param name = "room">The room.</param>
        /// <param name = "u">The u.</param>
        /// <param name = "roomReservationId">The room reservation id.</param>
        /// <param name = "comments">The comments.</param>
        /// <datetime>6/11/2011-11:42 AM</datetime>
        /// <author>
        ///     nate
        /// </author>
        public static void RoomReservationApproved(Room room, User u, int roomReservationId, string comments)
        {
            var keys = new List<KeyValuePair<string, string>>
                           {
                               new KeyValuePair<string, string>("{!MainTitleHtml}", "Room Reservation Approved"),
                               new KeyValuePair<string, string>("{!Subtitle01Html}", " "),
                               new KeyValuePair<string, string>("{!Subtitle01TextHtml}", string.Format("Room requestor has approved request for room: {0} in {1} {2}" + comments, room.Number, room.Building.Name, comments.Trim() == string.Empty ? " " : " has posted the following comments " + comments)),
                               new KeyValuePair<string, string>("{!LinkHtml}", "http://UrbanScheduler.com?RoomReservationKey=" + roomReservationId),
                               new KeyValuePair<string, string>("{!LinkTextHtml}", "Click Here To View"),
                               new KeyValuePair<string, string>("{!FormPerson}", "UrbanScheduler System")
                           };

            EmailTemplateUtilities.EmailFromFromMaster(keys, "UrbanScheduler Room Reservation Request Approved", u, new List<int>());
        }

        /// <summary>
        ///     Sends new user account email with activation key
        /// </summary>
        /// <param name = "room">The room.</param>
        /// <param name = "u">The u.</param>
        /// <param name = "roomReservationId">The room reservation id.</param>
        /// <param name = "comments">The comments.</param>
        /// <datetime>6/11/2011-11:42 AM</datetime>
        /// <author>
        ///     nate
        /// </author>
        public static void RoomReservationDenied(Room room, User u, int roomReservationId, string comments)
        {
            var keys = new List<KeyValuePair<string, string>>
                           {
                               new KeyValuePair<string, string>("{!MainTitleHtml}", "Room Reservation Denied"),
                               new KeyValuePair<string, string>("{!Subtitle01Html}", " "),
                               new KeyValuePair<string, string>("{!Subtitle01TextHtml}", string.Format("Room requestor has denied request for room: {0} in {1} {2}" + comments, room.Number, room.Building.Name, comments.Trim() == string.Empty ? " " : " has posted the following comments " + comments)),
                               new KeyValuePair<string, string>("{!LinkHtml}", "http://UrbanScheduler.com?RoomReservationKey=" + roomReservationId),
                               new KeyValuePair<string, string>("{!LinkTextHtml}", "Click Here To View"),
                               new KeyValuePair<string, string>("{!FormPerson}", "UrbanScheduler System")
                           };

            EmailTemplateUtilities.EmailFromFromMaster(keys, "UrbanScheduler Room Request Denied", u, new List<int>());
        }
    }
}