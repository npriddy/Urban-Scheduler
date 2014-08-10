#region

using System.Web.Script.Serialization;

#endregion

namespace UrbanSchedulerProject.Code.Utilities
{
    //based on http://stackoverflow.com/questions/1056121/how-to-create-json-string-in-c-sharp
    ///<summary>
    /// Converts objects to json objects
    ///</summary>
    public static class JsonHelper
    {
        ///<summary>
        ///</summary>
        ///<param name = "obj"></param>
        ///<returns></returns>
        public static string ToJson(this object obj)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }
    }
}