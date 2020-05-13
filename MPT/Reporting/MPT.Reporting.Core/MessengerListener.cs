namespace MPT.Reporting.Core
{
    /// <summary>
    /// Sample base class for a listener object for the messenger events.
    /// </summary>
    public class MessengerListener : ReporterWriter
    {
        // ncrunch: no coverage start
        protected MessengerListener()
        {
        }
        // ncrunch: no coverage end

        /// <summary>
        /// Subscribes the listener.
        /// </summary>
        /// <param name="subscriberEvent">The subscriber event.</param>
        public static void SubscribeListener(IMessengerEvent subscriberEvent)
        {
            subscriberEvent.MessengerEvent += reportMessageToConsole;
        }

        /// <summary>
        /// Unsubscribes the listener.
        /// </summary>
        /// <param name="subscriberEvent">The subscriber event.</param>
        public static void UnsubscribeListener(IMessengerEvent subscriberEvent)
        {
            subscriberEvent.MessengerEvent -= reportMessageToConsole;
        }

        /// <summary>
        /// Writes the messenger title and message to the console.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MessengerEventArgs"/> instance containing the event data.</param>
        protected static void reportMessageToConsole(object sender, MessengerEventArgs e)
        {
            if (e.Handled) return;
            Writer.WriteLine(getTitle(e));
            Writer.WriteLine(getMessage(e));
        }

        /// <summary>
        /// Assembles a title from the messenger object.
        /// </summary>
        /// <param name="e">The <see cref="MessengerEventArgs"/> instance containing the event data.</param>
        /// <returns>System.String.</returns>
        protected static string getTitle(MessengerEventArgs e)
        {
            return e.Title;
        }

        /// <summary>
        /// Assembles a message from the messenger object.
        /// </summary>
        /// <param name="e">The <see cref="MessengerEventArgs"/> instance containing the event data.</param>
        /// <returns>System.String.</returns>
        protected static string getMessage(MessengerEventArgs e)
        {
            return e.Message;
        }
    }
}
