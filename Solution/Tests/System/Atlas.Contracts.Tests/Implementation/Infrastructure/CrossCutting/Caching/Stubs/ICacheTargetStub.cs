using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.CrossCutting.Caching.Stubs
{
    public interface ICacheTargetStub : ICacheableObject
    {
        string Key { get; set; }


        int MethodCalled { get; set; }


        int CachedMethod(string objectId, int argument);

        int NotCachedMethod(string objectId, int argument);

        int ExceptionMethod(string objectId);

        int ResetingCacheMethod();

        int NotResetingCacheMethod();
    }
}