namespace MPT.Reporting.Core
{
    /// <summary>
    /// Class that contains the message details as enumerations for message types and actions.
    /// </summary>
    public class MessageDetails
    {
        /// <summary>
        ///  If a console or message box prompt is used to display the message data, the choice can be recorded here.
        ///  </summary>
        ///  <value></value>
        ///  <returns></returns>
        ///  <remarks></remarks>
        public eMessageActions Action { get; set; } = eMessageActions.None;

        /// <summary>
        ///  Decision combination types to display with the message.
        ///  </summary>
        ///  <value></value>
        ///  <returns></returns>
        ///  <remarks></remarks>
        public eMessageActionSets ActionSet { get; }

        /// <summary>
        ///  Type of message.
        ///  </summary>
        ///  <value></value>
        ///  <returns></returns>
        ///  <remarks></remarks>
        public eMessageType MessageType { get; }

        /// <summary>
        ///  Default action to take.
        ///  </summary>
        ///  <value></value>
        ///  <returns></returns>
        ///  <remarks></remarks>
        public eMessageActions ActionDefault { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageDetails"/> class.
        /// </summary>
        /// <param name="messageActionSet">Decision combination types to display with the message.</param>
        /// <param name="messageType">Type of message.</param>
        /// <param name="messageDefault">Default action to take.</param>
        public MessageDetails(
            eMessageActionSets messageActionSet = eMessageActionSets.OkOnly, 
            eMessageType messageType = eMessageType.None, 
            eMessageActions messageDefault = eMessageActions.None)
        {
            MessageType = messageType;
            ActionSet = messageActionSet;
            ActionDefault = messageDefault;
        }
    }
}
