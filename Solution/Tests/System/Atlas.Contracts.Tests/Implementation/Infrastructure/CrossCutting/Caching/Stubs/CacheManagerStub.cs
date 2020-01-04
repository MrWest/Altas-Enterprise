using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.CrossCutting.Caching.Stubs
{
    [ExcludeFromCodeCoverage]
    public class CacheManagerStub : ICacheManager
    {
        public bool Added { get; private set; }


        public void Add(string key, object value)
        {
            Added = true;
        }

        public void Add(string key, object value, CacheItemPriority scavengingPriority, ICacheItemRefreshAction refreshAction,
            params ICacheItemExpiration[] expirations)
        {
            Added = true;
        }

        public bool Contains(string key)
        {
            return false;
        }

        public void Flush()
        {
        }

        public object GetData(string key)
        {
            return null;
        }

        public void Remove(string key)
        {
        }

        public int Count { get; private set; }

        public object this[string key]
        {
            get { return null; }
        }
    }
}