using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.Practices.Unity.InterceptionExtension;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Validation;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Validation
{
    /// <summary>
    /// Represents an interception behavior that is about to intercept the calls to methods decorated by <see cref="ValidationAttribute"/>,
    /// validating the domain entity the methods receives and only going on with the method's logic if the validation turns out to be
    /// successful.
    /// </summary>
    public class ValidationBehavior : IInterceptionBehavior
    {
        /// <summary>
        /// Returns true.
        /// </summary>
        public bool WillExecute
        {
            get { return true; }
        }


        /// <summary>
        /// Returns an empty types array.
        /// </summary>
        /// <returns>An empty types array.</returns>
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        /// <summary>
        /// Executes validation of on domain entity passes as first argument of the target method. In case the validation is right, then the
        /// target method is called.
        /// </summary>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="getNext">Delegate to execute to get the next delegate in the behavior chain.</param>
        /// <returns>Return value from the target.</returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            IEntity entity;
            IValidationServices validationServices;

            // Determine whether there are all the elements to continue
            if (CanValidate(input, out entity, out validationServices))
            {
                // Validate the entity if everything is there to perform the validation
                IEnumerable<string> errors = validationServices.Validate(entity);

                // If errors, throw a validation exception
                if (errors.Any())
                    return input.CreateExceptionMethodReturn(new ValidationException(errors.ToArray()));
            }
            
            // If no errors, then continue with the next call in the chain
            return getNext()(input, getNext);;
        }

        private bool CanValidate(IMethodInvocation input, out IEntity entity, out IValidationServices validationServices)
        {
            MethodBase method = this.FindMethod(input);
            entity = null;
            validationServices = null;

            // Not requiring validation, skip it altogether
            if (method.GetCustomAttribute<ValidateAttribute>() == null)
                return false;

            // Grab the first method's argument, if there are not arguments or the is not at least a domain entity, again skip the validation
            entity = input.Inputs.OfType<IEntity>().FirstOrDefault();
            if (entity == null)
                return false;

            // Get the validation services, if the target is not a one, skip validation
            validationServices = input.Target as IValidationServices;
            if (validationServices == null)
                return false;

            return true;
        }
    }
}
