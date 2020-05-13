using System;

namespace MPT.Reporting.Core
{
    /// <summary>
    /// Sample base class for a listener object for the logger events.
    /// </summary>
    public class LoggerListener : ReporterWriter
    {
        // 'ncrunch: no coverage start
        protected LoggerListener()
        {
        }
        // 'ncrunch: no coverage end

        /// <summary>
        /// Subscribes the listener.
        /// </summary>
        /// <param name="subscriberEvent">The subscriber event.</param>
        public static void SubscribeListener(ILoggerEvent subscriberEvent)
        {
            subscriberEvent.LoggerEvent += reportLoggerEventToConsole;
        }

        /// <summary>
        /// Unsubscribes the listener.
        /// </summary>
        /// <param name="subscriberEvent">The subscriber event.</param>
        public static void UnsubscribeListener(ILoggerEvent subscriberEvent)
        {
            subscriberEvent.LoggerEvent -= reportLoggerEventToConsole;
        }

        /// <summary>
        /// Writes the log title and message to the console.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="LoggerEventArgs"/> instance containing the event data.</param>
        protected static void reportLoggerEventToConsole(object sender, LoggerEventArgs e)
        {
            if (e.Handled) return;
            if (!string.IsNullOrEmpty(e.Title))
            {
                Writer.WriteLine(getTitle(e));
            }
            Writer.WriteLine(getMessage(e));
        }

        /// <summary>
        /// Assembles a title from the logger object.
        /// </summary>
        /// <param name="e">The <see cref="LoggerEventArgs"/> instance containing the event data.</param>
        /// <returns>System.String.</returns>
        protected static string getTitle(LoggerEventArgs e)
        {
            return e.Title;
        }

        /// <summary>
        /// Assembles a message from the logger object.
        /// </summary>
        /// <param name="e">The <see cref="LoggerEventArgs"/> instance containing the event data.</param>
        /// <returns>System.String.</returns>
        protected static string getMessage(LoggerEventArgs e)
        {
            string message = "";

            if (e.Exception != null)
            {
                if (!string.IsNullOrEmpty(e.Exception.Message)) message += e.Exception.Message + Environment.NewLine;
                if (e.Exception.StackTrace != null) message += e.Exception.StackTrace + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(e.Message))
            {
                message += e.Message; 
            }
                
            
            foreach (string parameterName in e.Parameters.Keys)
            {
                message += Environment.NewLine + parameterName + " " + e.Parameters[parameterName];
            }

            return message;
        }
    }
}
