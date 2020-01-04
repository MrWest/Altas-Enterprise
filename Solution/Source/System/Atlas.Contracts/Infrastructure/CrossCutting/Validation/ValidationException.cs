using System;
using System.Runtime.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Validation
{
    /// <summary>
    /// Defines an exception thrown when a validation has unsatisfactory results.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ValidationException"/>.
        /// </summary>
        public ValidationException()
        {
            Errors = new string[0];
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ValidationException"/> with a message.
        /// </summary>
        /// <param name="message">The message explaining the cause of the exception.</param>
        public ValidationException(string message)
            : base(message)
        {
            Errors = new string[0];
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ValidationException"/> with a message and the actual exception causing the problem.
        /// </summary>
        /// <param name="message">The message explaining the cause of the exception.</param>
        /// <param name="innerException">The exception actually causing the problem by which the current exception is thrown.</param>
        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
            Errors = new string[0];
        }
 
        /// <summary>
        /// Initializes a new instance of the System.Exception class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.
        /// </param>
        /// <exception cref="ArgumentNullException">The info parameter is null.</exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException">
        /// The class name is null or System.Exception.HResult is zero (0).
        /// </exception>
        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Errors = new string[0];
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ValidationException"/> with an array of errors descriptions.
        /// </summary>
        /// <param name="errors">The error descriptions array contaning the explanations of the problems.</param>
        /// <exception cref="ArgumentNullException"><paramref name="errors"/> is null.</exception>
        public ValidationException(params string[] errors)
        {
            if (errors == null)
                throw new ArgumentNullException("errors");

            Errors = errors;
        }


        /// <summary>
        /// Gets the errors descriptions causing this validation exception.
        /// </summary>
        public string[] Errors { get; private set; }
    }
}
