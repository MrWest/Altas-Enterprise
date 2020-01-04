using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.CrossCutting.Caching.Stubs
{
    [ExcludeFromCodeCoverage]
    public class CacheTargetStub : ICacheTargetStub
    {
        public string Key { get; set; }

        public int MethodCalled { get; set; }


        [CachesResult]
        public int CachedMethod(string objectId, int argument)
        {
            MethodCalled++;
            return argument + 1;
        }

        public int NotCachedMethod(string objectId, int argument)
        {
            return argument + 1;
        }

        [CachesResult]
        public int ExceptionMethod(string objectId)
        {
            throw new DataMisalignedException();
        }

        [ResetsCache]
        public int ResetingCacheMethod()
        {
            return 30;
        }

        public int NotResetingCacheMethod()
        {
            return 30;
        }

        public string GetKeyFor(MethodBase method, params object[] arguments)
        {
            return Key;
        }
    }
}