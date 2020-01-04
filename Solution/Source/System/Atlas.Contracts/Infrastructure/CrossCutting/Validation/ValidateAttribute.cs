using System;

namespace CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Validation
{
    /// <summary>
    /// Decorates a method receiving as first argument an instance of domain entity, signaling that it must validate such domain entity
    /// before the method's logic executes. In case the validation returns results of an invalid state from the domain entity the method
    /// must not be executed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ValidateAttribute : Attribute
    {
    }
}
