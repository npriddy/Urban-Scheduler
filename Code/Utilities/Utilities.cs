#region

using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using Telerik.Web.UI;

#endregion

namespace UrbanSchedulerProject.Code.Utilities
{
    /// <summary>
    /// General Shared Utility class
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Stores list of all states and values
        /// </summary>
        public const string StatePathWithNoneXml = "~/App_Data/states_withnone.xml";

        /// <summary>
        ///     Gets the XML for path.
        /// </summary>
        /// <param name = "path">The path.</param>
        /// <returns></returns>
        /// <author>nate</author>
        /// <datetime>6/19/2011-1:27 PM</datetime>
        public static string GetXmlForPath(string path)
        {
            if (path.Trim() == String.Empty)
                throw new ArgumentException("path == String.Empty", "path");

            var sr = new StreamReader(HttpContext.Current.Server.MapPath(path));
            var xml = sr.ReadToEnd();
            sr.Close();
            return xml;
        }
        
        /// <summary>
        ///     Determines whether the specified input email is email.
        /// </summary>
        /// <param name = "inputEmail">The input email.</param>
        /// <returns>
        ///     <c>true</c> if the specified input email is email; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmail(string inputEmail)
        {
            var re = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$", RegexOptions.IgnoreCase);
            return re.IsMatch(inputEmail);
        }

        /// <summary>
        ///     NotNullAttribute 
        ///     Gets the query string safe. HI
        /// </summary>
        /// <param name = "key">The key.</param>
        /// <returns></returns>
        public static string GetQueryStringSafe(string key)
        {
            if (key == String.Empty)
                throw new ArgumentException("key == String.Empty", "key");

            var value = "";
            if (HttpContext.Current.Request.QueryString[key] != null)
                value = HttpContext.Current.Request.QueryString[key];
            return value;
        }

        /// <summary>
        ///     Gets the query string and try prases.
        ///     Returns 0 If Failed and a positive integer if anything else
        /// </summary>
        /// <param name = "key">The key.</param>
        /// <returns></returns>
        /// <author>nate</author>
        /// <datetime>6/22/2011-2:15 PM</datetime>
        public static int GetQueryStringInt(string key)
        {
            if (key == String.Empty)
                throw new ArgumentException("key == String.Empty", "key");

            if (key == null) return -1;
            var value = -1;

            if (HttpContext.Current.Request.QueryString[key] != null)
            {
                Int32.TryParse(HttpContext.Current.Request.QueryString[key], out value);
            }

            return value;
        }

        /// <summary>
        ///     Gets the query string GUID.
        /// </summary>
        /// <param name = "key">The key.</param>
        /// <returns></returns>
        public static Guid? GetQueryStringGuid(string key)
        {
            if (key == String.Empty)
                throw new ArgumentException("key == String.Empty", "key");
            try
            {
                if (HttpContext.Current.Request.QueryString[key] != null)
                {
                    var tmp = HttpContext.Current.Request.QueryString[key];
                    return new Guid(tmp);
                }
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
                // ReSharper restore EmptyGeneralCatchClause
            {
            }
            return null;
        }



        /// <summary>
        /// Strips the HTML text and tags.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string StripHtmlTextAndTags(string text)
        {
            if (text == String.Empty)
                throw new ArgumentException("text == String.Empty", "text");
            text = text.Replace("&nbsp;", " ");
            text = text.Replace('"', '\'');
            return Regex.Replace(text, @"<(.|\n)*?>", String.Empty);
        }
        /// <summary>
        ///     Determines whether [is site a2 B administration].
        /// </summary>
        /// <returns>
        ///     <c>true</c> if [is site a2 B administration]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSiteLocal()
        {
            var myString = ConfigurationManager.AppSettings["ServerLocation"];
            return myString == "local";
        }

        /// <summary>
        ///     Combines the date and time.
        /// </summary>
        /// <param name = "date">The date.</param>
        /// <param name = "time">The time.</param>
        /// <returns></returns>
        public static DateTime CombineDateAndTime(DateTime? date, TimeSpan? time)
        {
            if (date == null)
                throw new ArgumentNullException("date");
            if (time == null)
                throw new ArgumentNullException("time");
            return ((DateTime) date).Add((TimeSpan) time);
        }

        #region Nested type: GridFunctions

        /// <summary>
        /// Univeral Grid Functions
        /// </summary>
        public static class GridFunctions
        {
            #region Nested type: ReportExport

            /// <summary>
            /// Function to help with exporting 
            /// </summary>
            public static class ReportExport
            {
                /// <summary>
                ///     Formats the grid item.
                /// </summary>
                /// <param name = "item">The item.</param>
                public static void FormatGridItem(GridItem item)
                {
                    item.Style["color"] = "#000000";

                    if (item is GridDataItem)
                    {
                        item.Style["vertical-align"] = "middle";
                        item.Style["text-align"] = "center";
                    }

                    switch (item.ItemType) //Mimic RadGrid appearance for the exported PDF file
                    {
                        case GridItemType.Item:
                            item.Style["background-color"] = "#ebf4fb";
                            break;
                        case GridItemType.AlternatingItem:
                            item.Style["background-color"] = "#87CEFA";
                            break;
                        case GridItemType.Header:
                            item.Style["background-color"] = "#1E90FF";
                            break;
                        case GridItemType.CommandItem:
                            item.Style["background-color"] = "#1E90FF";
                            break;
                    }

                    if (item is GridCommandItem)
                    {
                        item.PrepareItemStyle(); //needed to span the image over the CommandItem cells
                    }
                }
            }

            #endregion
        }

        #endregion
    }
}