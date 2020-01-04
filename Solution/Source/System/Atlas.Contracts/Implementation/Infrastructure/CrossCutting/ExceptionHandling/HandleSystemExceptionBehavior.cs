using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity.InterceptionExtension;
using CompanyName.Atlas.Contracts.Exceptions;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Validation;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.ExceptionHandling
{
    /// <summary>
    /// This behavior ensures the handling of unexpected situations of the target method it intercepts.
    /// </summary>
    public class HandleSystemExceptionBehavior : IInterceptionBehavior
    {
        /// <summary>
        /// Determines whether the current behavior will execute. Always returns true.
        /// </summary>
        public bool WillExecute
        {
            get { return true; }
        }

        private ExceptionPolicyEntry[] ExceptionPolicy
        {
            get
            {
                return new[]
                {
                    // If there is a ValidationException, throw it as is
                    new ExceptionPolicyEntry(typeof(ValidationException), PostHandlingAction.NotifyRethrow, new IExceptionHandler[0]),

                    // If there is another exception, then log it, mail it and sanitize it
                    new ExceptionPolicyEntry(typeof(Exception), PostHandlingAction.ThrowNewException,
                        new IExceptionHandler[]
                        {
                            ServiceLocator.Current.GetInstance<ILogExceptionHandler>(),
                            ServiceLocator.Current.GetInstance<IEmailExceptionHandler>(),
                            ServiceLocator.Current.GetInstance<IReplaceHandler>()
                        })
                };
            }
        }


        /// <summary>
        /// Gets the types of the required interfaces it needs to implement.
        /// </summary>
        /// <returns>Returns an empty types array.</returns>
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        /// <summary>
        /// Invoked when the intercepted method is about to be executed. Tries to execute such method, and in case of an exception it
        /// logs the original exception, mails the exception details to the system's support team, and replaces it in a sanitized one
        /// so that there is no no security leaks, by letting no internal information flow up to the caller layer.
        /// </summary>
        /// <param name="input">
        /// The details of the intercepted method's invocation (parameters, argument, the target object over the method is called,
        /// etc). See the <see cref="IMethodInvocation"/> interface for more details.
        /// </param>
        /// <param name="getNext">
        /// A delegate which when executed returns the method to invoked in order to continue the flow to the intercepted method or the
        /// next behavior in the interception chain.
        /// </param>
        /// <returns>A <see cref="IMethodReturn"/> instance representing the details of the intercepted methods result.</returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            // Configure the exception handling block and prepare it for the intercepted method's execution
            ExceptionPolicyEntry[] policy = ExceptionPolicy;
            const string policyName = "Default";
            var policyManager = new ExceptionPolicyImpl(policyName, policy);
            var manager = new ExceptionManagerImpl(new Dictionary<string, ExceptionPolicyImpl> { { policyName, policyManager } });

            // Execute the method
            IMethodReturn methodResult = getNext()(input, getNext);

            // If there was an exception
            Exception originalException = methodResult.Exception;
            if (originalException != null)
            {
                /* Pass it to the Exception Handling Block, and if it recommends rethrowing an exception, throw the recommended by it should
                 * there is a one, or if none, then throw the original one catched from the intercepted method invocation */
                Exception newException;
                if (manager.HandleException(methodResult.Exception, policyName, out newException))
                    return input.CreateExceptionMethodReturn(newException ?? originalException);
            }

            // Otherwise return the result of the intercepted method
            return methodResult;
        }
    }
}
