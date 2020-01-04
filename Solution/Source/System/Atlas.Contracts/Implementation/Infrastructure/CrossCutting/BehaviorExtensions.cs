using System;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting
{
    /// <summary>
    /// Exposes some extensions for the <see cref="IInterceptionBehavior"/> implementors.
    /// </summary>
    public static class BehaviorExtensions
    {
        /// <summary>
        /// Extracts the method being the target of the given interception behavior.
        /// </summary>
        /// <param name="behavior">The <see cref="IInterceptionBehavior"/> behavior that is trying to obtain the target method.</param>
        /// <param name="input">The <see cref="IMethodInvocation"/> used as source to obtain the target method.</param>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is null.</exception>
        /// <exception cref="InvalidOperationException">
        /// There could not be able to obtain the single method matching the criteria of the one that is expected to obtain. There
        /// were none found or more than one to return.
        /// </exception>
        /// <returns>An instance of <see cref="MethodBase"/> being the target of <paramref name="behavior"/>.</returns>
        public static MethodBase FindMethod(this IInterceptionBehavior behavior, IMethodInvocation input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            Type type = input.Target.GetType();
            MethodBase other = input.MethodBase;
            ParameterInfo[] otherParameters = other.GetParameters();
            const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;

            var rslt = type.GetMethods(flags).Where(
                m => m.Name == other.Name &&
                     otherParameters.Length == m.GetParameters().Count() &&
                     m.GetParameters().All(p1 => otherParameters.Any(p2 =>
                         p1.Position == p2.Position &&
                         ((p1.ParameterType.IsGenericType && p2.ParameterType.IsGenericType) ||
                           p1.ParameterType == p2.ParameterType)))).ToList();
         
            return type.GetMethods(flags).Single(
                m => m.Name == other.Name &&
                     otherParameters.Length == m.GetParameters().Count() &&
                     m.GetParameters().All(p1 => otherParameters.Any(p2 =>
                         p1.Position == p2.Position &&
                         ((p1.ParameterType.IsGenericType && p2.ParameterType.IsGenericType) ||
                           p1.ParameterType == p2.ParameterType))));
        }
    }
}
