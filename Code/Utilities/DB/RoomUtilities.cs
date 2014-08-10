#region

using System;
using System.Collections.Generic;
using System.Linq;
using Urban.Data;
using UrbanSchedulerProject.Code.Classes;

#endregion

namespace UrbanSchedulerProject.Code.Utilities.DB
{
    /// <summary>
    /// </summary>
    public static class RoomUtilities
    {
        #region RelatedCityInformation

        /// <summary>
        ///     Return Distinct City Objects
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<CityObject> GetDistinctCities()
        {
            var db = new UrbanDataContext();
            var cList = ((from d in db.Room select new { d.Building.City }).Distinct().AsEnumerable().Select(p => new CityObject { Text = p.City, Value = p.City })).Distinct().ToList();

            cList.Insert(0, new CityObject
                                {
                                    Text = "(None)",
                                    Value = "NULL"
                                });
            return cList;
        }

        /// <summary>
        ///     Return Distinct City Objects
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<CityObject> GetDistinctCitiesByState(string state)
        {
            var db = new UrbanDataContext();
            var cList = ((from d in db.Room where d.Building.State == state  select new { d.Building.City }).Distinct().AsEnumerable().Select(p => new CityObject { Text = p.City, Value = p.City })).Distinct().ToList();

            cList.Insert(0, new CityObject
            {
                Text = "(None)",
                Value = "NULL"
            });
            return cList;
        }


        /// <summary>
        ///     Container for city drop down on room search
        /// </summary>
        public class CityObject
        {
            ///<summary>
            ///</summary>
            public string Text { get; set; }

            ///<summary>
            ///</summary>
            public string Value { get; set; }
        }

        #endregion RelatedCityInformation

        /// <summary>
        ///     Validates the room with number doesnt exist.
        /// </summary>
        /// <param name = "db">The db.</param>
        /// <param name = "buildingId">The building id.</param>
        /// <param name = "roomNumber">The room number.</param>
        /// <returns></returns>
        public static bool DoesRoomExistWithNumber(ref UrbanDataContext db, int buildingId, string roomNumber)
        {
            return db.Room.Any(t => t.BuildingID == buildingId && t.Number == roomNumber);
        }


        /// <summary>
        ///     Processes the room creation.
        /// </summary>
        /// <param name = "db">The db.</param>
        /// <param name = "buildingId">The building id.</param>
        /// <param name = "userId">The user id.</param>
        /// <param name = "apptList">The appt list.</param>
        /// <param name = "params">The @params.</param>
        /// <returns></returns>
        public static int ProcessRoomCreation(ref UrbanDataContext db, int buildingId, int userId, IEnumerable<AppointmentTemporaryObj> apptList, ProcessRoomCreationParams @params)
        {
            //Validate Room building combination
            if (DoesRoomExistWithNumber(ref db, buildingId, @params.Number))
            {
                return -1;
            }

            if (AppointmentTemporaryObj.ValidateListDoAnyOverlapInList(apptList))
                throw new Exception("Dates Over Lap");

            var room = new Room
                           {
                               Number = @params.Number,
                               Title = @params.Name,
                               MaxOccupancy = @params.MaxOccupancy,
                               RoomTypeID = @params.Type,
                               Description = @params.Description,
                               BuildingID = buildingId,
                               UserID = userId
                           };

            db.Room.InsertOnSubmit(room);
            db.SubmitChanges();

            foreach (var availabilityDate in apptList.Select(tempAppt => new RoomAvailability
                                                                             {
                                                                                 AllDay = false, 
                                                                                 Days = tempAppt.Days == String.Empty ? null : tempAppt.Days , 
                                                                                 EndDate = tempAppt.EndDate, 
                                                                                 EndTime = tempAppt.EndTime, 
                                                                                 RoomID = room.Id, 
                                                                                 StartDate = tempAppt.StartDate, 
                                                                                 StartTime = tempAppt.StartTime
                                                                             }))
            {
                db.RoomAvailability.InsertOnSubmit(availabilityDate);
            }


            db.SubmitChanges();
            return room.Id;
        }

        #region Nested type: ProcessRoomCreationParams

        /// <summary>
        /// </summary>
        public class ProcessRoomCreationParams
        {
            /// <summary>
            ///     Gets or sets the name.
            /// </summary>
            /// <value>
            ///     The name.
            /// </value>
            public string Name { get; set; }

            /// <summary>
            ///     Gets or sets the number.
            /// </summary>
            /// <value>
            ///     The number.
            /// </value>
            public string Number { get; set; }

            /// <summary>
            ///     Gets or sets the max occupancy.
            /// </summary>
            /// <value>
            ///     The max occupancy.
            /// </value>
            public int MaxOccupancy { get; set; }

            /// <summary>
            ///     Gets or sets the type.
            /// </summary>
            /// <value>
            ///     The type.
            /// </value>
            public int Type { get; set; }

            /// <summary>
            ///     Gets or sets the description.
            /// </summary>
            /// <value>
            ///     The description.
            /// </value>
            public string Description { get; set; }
        }

        #endregion
    }
}