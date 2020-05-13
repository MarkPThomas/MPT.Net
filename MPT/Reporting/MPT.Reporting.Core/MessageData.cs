namespace MPT.Reporting.Core
{
    /// <summary>
    /// Class that contains the message and related data.
    /// </summary>
    public class MessageData
    {
        /// <summary>
        /// Title of the message.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string Title { get; }

        /// <summary>
        /// Message.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string Message { get; }

        /// <summary>
        /// Footer to the message.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string Footer { get; }

        /// <summary>
        /// List content of the message.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string PromptList { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageData"/> class.
        /// </summary>
        /// <param name="title">The title of the message.</param>
        /// <param name="message">The message.</param>
        /// <param name="footer">The footer to the message.</param>
        /// <param name="promptList">The list content of the message.</param>
        public MessageData(
            string title = "", 
            string message = "", 
            string footer = "", 
            string promptList = "")
        {
            Title = title;
            Message = message;
            Footer = footer;
            PromptList = promptList;
        }
    }
}
