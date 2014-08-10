#region

using System;
using System.Collections.Generic;
using System.Text;
using Urban.Data;

#endregion

namespace UrbanSchedulerProject.Code.SerializableClasses
{
    /// <summary>
    /// Used for storing building information for use with google maps api
    /// </summary>
    [Serializable]
    public class BuildingSerialObj
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingSerialObj"/> class.
        /// </summary>
        /// <param name="room">The room.</param>
        public BuildingSerialObj(Room room)
        {
            BuildAddress(room);
            BuildContent(room);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingSerialObj"/> class.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <param name="rList">The r list.</param>
        public BuildingSerialObj(Building b, IEnumerable<Room> rList)
        {
            BuildAddress(b);
            foreach (var r in rList)
            {
                BuildContent(r);
            }
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }
        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }


        /// <summary>
        /// Builds the address.
        /// </summary>
        /// <param name="room">The room.</param>
        private void BuildAddress(Room room)
        {
            var sb = new StringBuilder();
            sb.Append(room.Building.PrimaryAddress + " ");
            if (room.Building.SecondaryAddress != null)
                sb.Append(room.Building.SecondaryAddress + " ");
            sb.Append(room.Building.City + " ");
            sb.Append(room.Building.Zip + " ");
            sb.Append(room.Building.State + " ");
            Address = sb.ToString();
        }


        /// <summary>
        /// Builds the address.
        /// </summary>
        /// <param name="b">The b.</param>
        private void BuildAddress(Building b)
        {
            var sb = new StringBuilder();
            sb.Append(b.PrimaryAddress + " ");
            if (b.SecondaryAddress != null)
                sb.Append(b.SecondaryAddress + " ");
            sb.Append(b.City + " ");
            sb.Append(b.Zip + " ");
            sb.Append(b.State + " ");
            Address = sb.ToString();
        }

        /// <summary>
        /// Builds the content.
        /// </summary>
        /// <param name="room">The room.</param>
        private void BuildContent(Room room)
        {
            var sb = new StringBuilder();
            var javascript = String.Format("javascript:rebindParentAndRedirect('{0}')", room.Id);
            sb.Append(String.Format("<strong>Title</strong>: <a href={0}> {1} </a> <br />", javascript, room.Building.Name));
            sb.Append(String.Format("<strong>Description</strong>: {0} <br />", Utilities.Utilities.StripHtmlTextAndTags(room.Description)));
            foreach (var image in room.RoomImageLinkList)
            {
                sb.Append(String.Format("<br /><strong>Image :</strong> <img  height='100px' src='{0}'></img>", "../../Files/" + image.FileFiles.ServerFileName));
            }
            Content += sb.ToString();
        }
    }
}