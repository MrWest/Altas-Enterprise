using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Caching;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Data;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.ExceptionHandling;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Validation;

namespace CompanyName.Atlas.Contracts.Infrastructure.CrossCutting
{
    /// <summary>
    /// Provides the sets of policies that are meant for key components in the system's architecture.
    /// </summary>
    public static class Policies
    {
        /// <summary>
        /// These are the policies that must be employed for the Item Manager Application Services.
        /// </summary>
        public static InjectionMember[] ItemManagerApplicationServicesPolicies
        {
            get
            {
                return new InjectionMember[]
                {
                    new Interceptor<InterfaceInterceptor>(),
                    new InterceptionBehavior<HandleSystemExceptionBehavior>(),
                    new InterceptionBehavior<ValidationBehavior>(),
                    new InterceptionBehavior<CachingBehavior>(),
                    new InterceptionBehavior<UnitOfWorkBehavior>(),
                };
            }
        }
    }
}
