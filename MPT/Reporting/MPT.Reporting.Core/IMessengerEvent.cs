using System;

namespace MPT.Reporting.Core
{
    /// <summary>
    /// Interface for objects that throw messenger events.
    /// </summary>
    /// <remarks></remarks>
    public interface IMessengerEvent
    {
        /// <summary>
        /// To be used for sending informative messages to the user.
        /// </summary>
        /// <remarks></remarks>
        event EventHandler<MessengerEventArgs> MessengerEvent;
    }
}
