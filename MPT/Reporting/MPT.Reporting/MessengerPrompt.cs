using System;
using MPT.Reporting.Core;

namespace MPT.Reporting
{
    /// <summary>
    /// Class that is subscribed/unsubscribed to the event.
    /// </summary>
    /// <seealso ref="https://stackoverflow.com/questions/289002/how-to-raise-custom-event-from-a-static-class"/>
    public static class MessengerPrompt
    {
        public static event EventHandler<MessengerEventArgs> MessengerEvent = delegate { };

        public static eMessageActions Prompt(
            MessageDetails messageDetails, 
            string message, 
            string title, 
            params object[] arg)
        {
            MessengerEventArgs e = new MessengerEventArgs(messageDetails, message, title, arg);
            MessengerEvent(null, e);
            return e.Action;
        }
    }
}