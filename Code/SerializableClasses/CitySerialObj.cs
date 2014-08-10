#region

using System;

#endregion

namespace UrbanSchedulerProject.Code.SerializableClasses
{
    /// <summary>
    /// Used for displaying city objects on google maps
    /// </summary>
    [Serializable]
    public class CitySerialObj
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingSerialObj"/> class.
        /// </summary>
        /// <param name="city">The city.</param>
        public CitySerialObj(string city)
        {
            Address = city;
            Content = "";
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        public string Address { get; private set; }
        /// <summary>
        /// Gets the content.
        /// </summary>
        public string Content { get; private set; }
    }
}