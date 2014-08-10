#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Web.UI;
using Urban.Data;

#endregion

namespace UrbanSchedulerProject.Code.Utilities.DB
{
    /// <summary>
    /// </summary>
    public static class BuildingUtilities
    {
        /// <summary>
        ///     Doeses the building already exist.
        /// </summary>
        /// <param name = "db">The db.</param>
        /// <param name = "primaryAddress">The primary address.</param>
        /// <param name = "secondaryAddress">The secondary address.</param>
        /// <param name = "city">The city.</param>
        /// <param name = "zip">The zip.</param>
        /// <param name = "state">The state.</param>
        /// <param name = "userId">The user id.</param>
        /// <returns></returns>
        public static Building DoesBuildingAlreadyExist(ref UrbanDataContext db, string primaryAddress, string secondaryAddress, string city, string zip, string state, int userId)
        {
            return db.Building.SingleOrDefault(t => t.UserID == userId && t.PrimaryAddress == primaryAddress && t.SecondaryAddress == secondaryAddress && t.City == city && t.Zip == zip && t.State == state);
        }

        /// <summary>
        /// Doeses the building already exist.
        /// </summary>
        /// <param name="db">The db.</param>
        /// <param name="name">The name.</param>
        /// <param name="primaryAddress">The primary address.</param>
        /// <param name="secondaryAddress">The secondary address.</param>
        /// <param name="city">The city.</param>
        /// <param name="zip">The zip.</param>
        /// <param name="state">The state.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static Building DoesBuildingAlreadyExist(ref UrbanDataContext db, string name, string primaryAddress, string secondaryAddress, string city, string zip, string state, int userId)
        {
            return db.Building.SingleOrDefault(t => t.Name == name && t.UserID == userId && t.PrimaryAddress == primaryAddress && t.SecondaryAddress == secondaryAddress && t.City == city && t.Zip == zip && t.State == state);
        }

        /// <summary>
        ///     Doeses the building already exist.
        /// </summary>
        /// <param name = "db">The db.</param>
        /// <param name = "primaryAddress">The primary address.</param>
        /// <param name = "secondaryAddress">The secondary address.</param>
        /// <param name = "city">The city.</param>
        /// <param name = "zip">The zip.</param>
        /// <param name = "state">The state.</param>
        /// <param name = "userId">The user id.</param>
        /// <returns></returns>
        public static bool DoesBuildingAlreadyExistNotForUser(ref UrbanDataContext db, string primaryAddress, string secondaryAddress, string city, string zip, string state, int userId)
        {
            return db.Building.Any(t => t.UserID != userId && t.PrimaryAddress == primaryAddress && t.SecondaryAddress == secondaryAddress && t.City == city && t.Zip == zip && t.State == state);
        }

        /// <summary>
        ///     Gets the by user id.
        /// </summary>
        /// <param name = "userId">The user id.</param>
        /// <param name = "insertEmpty">if set to <c>true</c> [insert empty].</param>
        /// <returns></returns>
        public static IEnumerable<RadComboBoxItem> GetByUserId(int userId, bool insertEmpty)
        {
            var db = new UrbanDataContext();
            var list = (from d in db.Building
                        where d.UserID == userId
                        orderby d.Name
                        select new RadComboBoxItem
                                   {
                                       Text = d.Name ?? d.PrimaryAddress,
                                       Value = d.Id.ToString()
                                   }).ToList();
            if (insertEmpty) list.Insert(0, new RadComboBoxItem("(None)", "NULL"));
            return list;
        }

        /// <summary>
        ///     Processes the building creation.
        /// </summary>
        /// <param name = "db">The db.</param>
        /// <param name = "userId">The user id.</param>
        /// <param name = "params">The @params.</param>
        /// <returns></returns>
        public static int ProcessBuildingCreation(ref UrbanDataContext db, int userId, ProcessBuildingCreationParams @params)
        {
            //Check if already exists then return buidling Id
            var existingBuilding = DoesBuildingAlreadyExist(ref db, @params.PrimaryAddress, @params.SecondaryAddress, @params.City, @params.Zip, @params.State, userId);
            if (existingBuilding != null)
                return existingBuilding.Id;

            //Check if valid if not return -1 
            if (DoesBuildingAlreadyExistNotForUser(ref db, @params.PrimaryAddress, @params.SecondaryAddress, @params.City, @params.Zip, @params.State, userId))
                return -1;

            //create new building then return new building id
            var building = new Building
                               {
                                   PrimaryAddress = @params.PrimaryAddress,
                                   SecondaryAddress = @params.SecondaryAddress,
                                   City = @params.City,
                                   Zip = @params.Zip,
                                   State = @params.State,
                                   Name = @params.Name,
                                   UserID = userId
                               };
            db.Building.InsertOnSubmit(building);
            db.SubmitChanges();
            return building.Id;
        }

        #region Nested type: ProcessBuildingCreationParams

        /// <summary>
        /// </summary>
        public class ProcessBuildingCreationParams
        {
            /// <summary>
            ///     Gets or sets the name.
            /// </summary>
            /// <value>
            ///     The name.
            /// </value>
            public string Name { get; set; }

            /// <summary>
            ///     Gets or sets the primary address.
            /// </summary>
            /// <value>
            ///     The primary address.
            /// </value>
            public string PrimaryAddress { get; set; }

            /// <summary>
            ///     Gets or sets the secondary address.
            /// </summary>
            /// <value>
            ///     The secondary address.
            /// </value>
            public string SecondaryAddress { get; set; }

            /// <summary>
            ///     Gets or sets the zip.
            /// </summary>
            /// <value>
            ///     The zip.
            /// </value>
            public string Zip { get; set; }

            /// <summary>
            ///     Gets or sets the city.
            /// </summary>
            /// <value>
            ///     The city.
            /// </value>
            public string City { get; set; }

            /// <summary>
            ///     Gets or sets the state.
            /// </summary>
            /// <value>
            ///     The state.
            /// </value>
            public string State { get; set; }
        }

        #endregion
    }
}