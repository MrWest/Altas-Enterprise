using System;
using System.Runtime.Serialization;

namespace CompanyName.Atlas.Contracts.Exceptions
{
    /// <summary>
    /// Thrown when an unexpected error occured when there were performing an operation in the system.
    /// </summary>
    public class UnexpectedErrorException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the exception <see cref="UnexpectedErrorException"/>.
        /// </summary>
        public UnexpectedErrorException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the exception <see cref="UnexpectedErrorException"/> with a message.
        /// </summary>
        /// <param name="message">The message explaining the cause of the exception.</param>
        public UnexpectedErrorException(string message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the exception <see cref="UnexpectedErrorException"/> with a message.
        /// </summary>
        /// <param name="message">The message explaining the cause of the exception.</param>
        /// <param name="innerException">The actual exception being the cause of the one being initialized.</param>
        public UnexpectedErrorException(string message, Exception innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the exception <see cref="UnexpectedErrorException"/> with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected UnexpectedErrorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
