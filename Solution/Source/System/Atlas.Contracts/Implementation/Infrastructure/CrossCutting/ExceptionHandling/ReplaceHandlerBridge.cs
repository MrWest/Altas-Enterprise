using System;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.ExceptionHandling
{
    /// <summary>
    /// Bridges the implementation of Enterprise Library Exception Block with the local contract of the Replace Exception Handler.
    /// </summary>
    public class ReplaceHandlerBridge : ReplaceHandler, IReplaceHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceHandlerBridge"/> class.
        /// </summary>
        public ReplaceHandlerBridge()
            : base(Resources.UnexpectedException, typeof(Exception))
        {
        }
    }
}
