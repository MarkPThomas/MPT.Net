using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MPT.Reporting.Core
{
    /// <summary>
    /// Event argument that relays a message, error, or thrown exception from the libraries to an external assembly.
    /// </summary>
    /// <seealso cref="MessengerEventArgs" />
    public class LoggerEventArgs : MessengerEventArgs
    {
        /// <summary>
        /// Exception object thrown.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public Exception Exception { get; }

        /// <summary>
        /// Parameter name-value pairs associated with the calling method or operation within the method.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public Dictionary<string, string> Parameters { get; } = new Dictionary<string, string>();

        /// <inheritdoc />
        /// <summary>
        /// Initializer for a message-type of log.
        /// </summary>
        /// <param name="messenger">Event argument that relays a message.</param>
        /// <param name="arg">List of variables to insert into the message.</param>
        public LoggerEventArgs(
            MessengerEventArgs messenger, 
            params object[] arg) : base(messenger)
        {
            storeParameters(arg);
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerEventArgs"/> class for an exception/error type of log.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="messenger">Event argument that relays a message.</param>
        /// <param name="arg">Parameter name-value pairs associated with the calling method or operation within the method.</param>
        public LoggerEventArgs(
            Exception exception,
            MessengerEventArgs messenger,
            params object[] arg) : base(messenger)
        {
            storeParameters(arg);
            Exception = exception;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerEventArgs"/> class for an exception/error type of log.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="arg">Parameter name-value pairs associated with the calling method or operation within the method.</param>
        public LoggerEventArgs(
            Exception exception,
            params object[] arg) : base(new MessengerEventArgs(string.Empty))
        {
            storeParameters(arg);
            Exception = exception;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MPT.Reporting.Core.LoggerEventArgs" /> class for an exception/error type of log..
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="messenger">Event argument that relays a message.</param>
        public LoggerEventArgs(Exception exception,
            MessengerEventArgs messenger = null) : base(messenger)
        {
            Exception = exception;
        }

        
        /// <summary>
        /// Parses the arguments into parameter name/value pairs, accounting for the initial format of a local parameter object vs. property.
        /// </summary>
        /// <param name="arg"></param>
        /// <remarks></remarks>
        private void storeParameters(params object[] arg)
        {
            if (!arg.Any()) return;
            for (var i = 0; i <= arg.Count() - 1; i += 2)
            {
                if (i + 1 > arg.Count() - 1) continue;
                string parameterName = arg[i].ToString();
                storeParameter(parameterName, arg[i + 1]);
            }
        }

        /// <summary>
        /// Stores the name/value of the parameter, considering whether or not the value is a list of values.
        /// </summary>
        /// <param name="parameterName">Name of the parameter to store.</param>
        /// <param name="parameterValue">Value object corresponding to the parameter name.</param>
        /// <remarks></remarks>
        private void storeParameter(string parameterName, object parameterValue)
        {
            // Determine if list of values or single value
            if (!(parameterValue is string) && 
                 parameterValue is IEnumerable currentArg)
            {
                int keyNumber = 0;
                foreach (object currentArgItem in currentArg)
                {
                    Parameters.Add(parameterName + "(" + keyNumber + ")", currentArgItem.ToString());
                    keyNumber += 1;
                }
            }
            else
            {
                Parameters.Add(parameterName, parameterValue.ToString());
            }
        }
    }
}
