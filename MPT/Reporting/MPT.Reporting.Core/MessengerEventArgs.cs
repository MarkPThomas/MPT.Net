using System;

namespace MPT.Reporting.Core
{
    /// <summary>
    /// Event argument that relays a message from the libraries to an external assembly.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class MessengerEventArgs : EventArgs
    {
        private readonly MessageData _messageData = new MessageData();
        /// <summary>
        /// Title of the message associated with the event.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string Title => _messageData.Title;

        /// <summary>
        /// Message associated with the event.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string Message => _messageData.Message;

        /// <summary>
        /// Footer of the message associated with the event.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string Footer => _messageData.Footer;

        /// <summary>
        /// List content of the message associated with the event.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string PromptList => _messageData.PromptList;


        private readonly MessageDetails _messageDetails = new MessageDetails();
        /// <summary>
        /// If a console or message box prompt is used to display the message data, the choice can be recorded here.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public eMessageActions Action
        {
            get => _messageDetails.Action;
            set => _messageDetails.Action = value;
        }

        /// <summary>
        /// Decision combination types to display with the message.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public eMessageActionSets ActionSet => _messageDetails.ActionSet;

        /// <summary>
        /// Type of message.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public eMessageType MessageType => _messageDetails.MessageType;

        /// <summary>
        /// Default action to take.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public eMessageActions ActionDefault => _messageDetails.ActionDefault;


        /// <summary>
        /// True: The event has already been handled.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Handled { get; set; }



        /// <inheritdoc />
        /// <summary>
        /// Initializer.
        /// </summary>
        /// <param name="message">Message associated with the event.</param>
        /// <param name="arg">List of variables to insert into the message.</param>
        /// <remarks></remarks>
        public MessengerEventArgs(string message, params object[] arg)
        {
            _messageData = new MessageData(message: string.Format(message, arg));
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializer.
        /// </summary>
        /// <param name="title">Title associated with the message.</param>
        /// <param name="message">Message associated with the event.</param>
        /// <param name="arg">List of variables to insert into the message.</param>
        /// <remarks></remarks>
        public MessengerEventArgs(
            string title, 
            string message, 
            params object[] arg)
        {
            _messageData = new MessageData(
                                title: title, 
                                message: string.Format(message, arg));
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializer.
        /// </summary>
        /// <param name="messageDetails">Object that contains the message details as enumerations for message types and actions.</param>
        /// <param name="message">Message associated with the event.</param>
        /// <param name="arg">List of variables to insert into the message.</param>
        /// <remarks></remarks>
        public MessengerEventArgs(
            MessageDetails messageDetails, 
            string message, 
            params object[] arg)
        {
            _messageDetails = messageDetails;
            _messageData = new MessageData(message: string.Format(message, arg));
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializer.
        /// </summary>
        /// <param name="messageData">Object that contains the message and related data.</param>
        /// <param name="messageDetails">Object that contains the message details as enumerations for message types and actions.</param>
        /// <param name="arg">List of variables to insert into the message.</param>
        /// <remarks></remarks>
        public MessengerEventArgs(
            MessageDetails messageDetails, 
            MessageData messageData, 
            params object[] arg)
        {
            _messageDetails = messageDetails;
            _messageData = new MessageData(
                                title: messageData.Title, 
                                message: string.Format(messageData.Message, arg), 
                                footer: messageData.Footer, 
                                promptList: messageData.PromptList);
        }

        public MessengerEventArgs(MessengerEventArgs args)
        {
            if (args == null)
            {
                return;
            }
            _messageDetails = args._messageDetails;
            _messageData = args._messageData;
        }
    }
}
