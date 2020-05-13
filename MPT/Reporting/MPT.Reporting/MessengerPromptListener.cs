using System.Windows;
using MPT.Reporting.Core;

namespace MPT.Reporting
{
    /// <summary>
    /// Listener to display messenger report on a prompt.
    /// </summary>
    /// <seealso cref="MPT.Reporting.Core.MessengerListener" />
    public class MessengerPromptListener : MessengerListener
    {

        // 'ncrunch: no coverage start
        private MessengerPromptListener()
        {
        }
        // 'ncrunch: no coverage end

        /// <summary>
        /// Subscribes the listener to the provided object and displays a standard message box.
        /// </summary>
        /// <param name="subscriberEvent"></param>
        /// <remarks></remarks>
        public static void SubscribeListenerToMessageBox(IMessengerEvent subscriberEvent)
        {
            subscriberEvent.MessengerEvent += reportMessengerEventToMessengerEventBox;
        }

        /// <summary>
        /// Unsubscribes the listener from the provided object.
        /// </summary>
        /// <param name="subscriberEvent"></param>
        /// <remarks></remarks>
        public static void UnsubscribeListenerToMessageBox(IMessengerEvent subscriberEvent)
        {
            subscriberEvent.MessengerEvent -= reportMessengerEventToMessengerEventBox;
        }

        /// <summary>
        /// Subscribes shared classes to the listener and displays a standard message box.
        /// </summary>
        /// <remarks></remarks>
        public static void SubscribeLibraryListenerToMessageBox()
        {
            MessengerPrompt.MessengerEvent += reportMessengerEventToMessengerEventBox;
        }

        /// <summary>
        /// Writes the messenger title and message to the console.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MessengerEventArgs"/> instance containing the event data.</param>
        protected static void reportMessengerEventToMessengerEventBox(object sender, MessengerEventArgs e)
        {
            if (e.Handled) return;
            MessageBoxResult result = MessageBox.Show(
                                        getMessage(e), 
                                        getTitle(e), 
                                        toMessageBox(e.ActionSet),
                                        toMessageBox(e.MessageType));
            switch (result)
            {
                case MessageBoxResult.Cancel:
                {
                    e.Action = eMessageActions.Cancel;
                    break;
                }

                case MessageBoxResult.No:
                {
                    e.Action = eMessageActions.No;
                    break;
                }

                case MessageBoxResult.None:
                {
                    e.Action = eMessageActions.None;
                    break;
                }

                case MessageBoxResult.OK:
                {
                    e.Action = eMessageActions.OK;
                    break;
                }

                case MessageBoxResult.Yes:
                {
                    e.Action = eMessageActions.Yes;
                    break;
                }

                default:
                {
                    e.Action = eMessageActions.None;
                    break;
                }
            }
        }

        /// <summary>
        /// Converts the message enum to the corresponding MessageBox enum.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>MessageBoxButton.</returns>
        protected static MessageBoxButton toMessageBox(eMessageActionSets value)
        {
            switch (value)
            {
                case eMessageActionSets.AbortRetryIgnore:
                {
                    return MessageBoxButton.YesNoCancel;
                }

                case eMessageActionSets.None:
                {
                    return MessageBoxButton.OK;
                }

                case eMessageActionSets.OkCancel:
                {
                    return MessageBoxButton.OKCancel;
                }

                case eMessageActionSets.OkOnly:
                {
                    return MessageBoxButton.OK;
                }

                case eMessageActionSets.RetryCancel:
                {
                    return MessageBoxButton.OKCancel;
                }

                case eMessageActionSets.YesNo:
                {
                    return MessageBoxButton.YesNo;
                }

                case eMessageActionSets.YesNoCancel:
                {
                    return MessageBoxButton.YesNoCancel;
                }

                default:
                {
                    return MessageBoxButton.OK;
                }
            }
        }

        /// <summary>
        /// Converts the message enum to the corresponding MessageBox enum.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>MessageBoxImage.</returns>
        protected static MessageBoxImage toMessageBox(eMessageType value)
        {
            switch (value)
            {
                case eMessageType.Asterisk:
                {
                    return MessageBoxImage.Asterisk;
                }

                case eMessageType.Error:
                {
                    return MessageBoxImage.Error;
                }

                case eMessageType.Exclamation:
                {
                    return MessageBoxImage.Exclamation;
                }

                case eMessageType.Hand:
                {
                    return MessageBoxImage.Hand;
                }

                case eMessageType.Information:
                {
                    return MessageBoxImage.Information;
                }

                case eMessageType.None:
                {
                    return MessageBoxImage.None;
                }

                case eMessageType.Question:
                {
                    return MessageBoxImage.Question;
                }

                case eMessageType.Stop:
                {
                    return MessageBoxImage.Stop;
                }

                case eMessageType.Warning:
                {
                    return MessageBoxImage.Warning;
                }

                default:
                {
                    return MessageBoxImage.None;
                }
            }
        }
    }
}