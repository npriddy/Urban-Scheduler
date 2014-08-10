#region

using System;
using System.Diagnostics;
using System.Web;

#endregion

namespace UrbanSchedulerProject.Code.Utilities
{
    /// <summary>
    /// </summary>
    public static class TraceUtilities
    {
        /// <summary>
        ///     Writes the trace.
        /// </summary>
        /// <param name = "enteringFunction">if set to <c>true</c> [entering function].</param>
        public static void WriteTrace(bool enteringFunction)
        {
            var callingFunctionName = "Undetermined method";
            var callingParentFunctionName = "";
            var action = enteringFunction ? "Entering" : "Exiting";
            var test = "";
            try
            {
                //Determine the name of the calling function.
                var stackTrace = new StackTrace();
                callingFunctionName = stackTrace.GetFrame(1).GetMethod().Name;
                callingParentFunctionName = " Parent: " + stackTrace.GetFrame(2).GetMethod();
                test = stackTrace.ToString().Substring(0, stackTrace.ToString().IndexOf("at System.Web"));
            }
            catch (Exception ex)
            {
                Trace.TraceWarning(ex.Message);
            }
            HttpContext.Current.Trace.Write(action, callingFunctionName + callingParentFunctionName + test);
        }
    }
}