using System;

namespace MPT.Reporting.Core
{
    /// <summary>
    /// Class that handles reporting starting and ending a method.
    /// TODO: Unsure if this class is still neeed.
    /// </summary>
    public static class RaiseToggleAction
    {
        public static event EventHandler<MessengerEventArgs> MessengerEvent = delegate { };
        
        /// <summary>
        /// Raises a messenger event indicating the start of a function.
        /// </summary>
        public static void StartAction(string method)
        {
            MessengerEvent(null, new MessengerEventArgs(method + " started."));
        }

        /// <summary>
        /// Raises a messenger event indicating the end of a function
        /// </summary>
        public static void EndAction(string method)
        {
            MessengerEvent(null, new MessengerEventArgs(method + " completed."));
        }
    }
}
