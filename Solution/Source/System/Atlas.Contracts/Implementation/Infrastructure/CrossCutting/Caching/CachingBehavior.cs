using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity.InterceptionExtension;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Caching
{
    /// <summary>
    ///     Represents the base class of the behaviors participating in the caching.
    /// </summary>
    public class CachingBehavior : IInterceptionBehavior
    {
        /// <summary>
        ///     Gets whether this behavior gets executed. Alwasy returns true.
        /// </summary>
        public bool WillExecute
        {
            get { return true; }
        }

        /// <summary>
        ///     Performs the custom caching logic. No to be called directly,
        ///     that's why no information is provided about the parameters.
        /// </summary>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Type targetType = input.Target.GetType();
            if (!targetType.Implements<ICacheableObject>())
                throw new InvalidOperationException(Resources.CacheablesMustImplementContract.EasyFormat(targetType.FullName));

            MethodBase method = this.FindMethod(input);
            object[] attributes = method.GetCustomAttributes(true);

            return ExecuteCachingAction(input, getNext, attributes);
        }

        /// <summary>
        ///     Returns the interfaces to be implemented by the behavior.
        /// </summary>
        /// <returns>None.</returns>
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return new[] { typeof(ICacheableObject) };
        }

        private IMethodReturn ExecuteCachingAction(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext, IEnumerable<object> attributes)
        {
            if (MustCacheResult(attributes)) return CacheResult(input, getNext);

            return MustResetCache(attributes) ? ResetCache(input, getNext) : getNext()(input, getNext);
        }

        private static bool MustCacheResult(IEnumerable<object> attributes)
        {
            return HasAttribute<CachesResultAttribute>(attributes);
        }

        private static bool MustResetCache(IEnumerable<object> attributes)
        {
            return HasAttribute<ResetsCacheAttribute>(attributes);
        }

        private static bool HasAttribute<TAttribute>(IEnumerable<object> attributes)
        {
            return attributes.OfType<TAttribute>().Any();
        }

        private static string GenerateKey(IMethodInvocation input)
        {
            object[] arguments = input.Arguments.Cast<object>().ToArray();

            var cacheable = (ICacheableObject)input.Target;
            string key = cacheable.GetKeyFor(input.MethodBase, arguments);

            return key;
        }

        private static ICache GetCache()
        {
            return ServiceLocator.Current.GetInstance<ICache>();
        }

        #region Behavior functions

        private IMethodReturn CacheResult(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            ICache cache = GetCache();

            string key = GenerateKey(input);
            object value;

            if (!cache.TryGet(key, out value))
            {
                IMethodReturn result = getNext()(input, getNext);

                if (result.Exception != null)
                    return input.CreateExceptionMethodReturn(result.Exception);

                value = result.ReturnValue;
                cache.Store(key, value);
            }

            return input.CreateMethodReturn(value);
        }

        private static IMethodReturn ResetCache(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            IMethodReturn result = getNext()(input, getNext);

            if (result.Exception == null)
                GetCache().Clear();

            return result;
        }

        #endregion
    }
}