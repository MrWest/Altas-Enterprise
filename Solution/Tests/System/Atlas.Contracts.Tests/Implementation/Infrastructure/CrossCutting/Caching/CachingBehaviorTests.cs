using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Caching;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching;
using CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.CrossCutting.Caching.Stubs;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.CrossCutting.Caching
{
    [TestClass, ExcludeFromCodeCoverage]
    public class CachingBehaviorTests : TestBase<CachingBehavior>
    {
        private Mock<ICache> _cacheMock;
        private ICache _cache;

        private ICacheTargetStub _cacheTarget;

        private UnityContainer _container;
        
        
        [TestInitialize]
        public override void Initialize()
        {
            CreateServiceLocatorMock();
            CreateServiceLocator();

            _container = new UnityContainer();
            _container.AddNewExtension<Interception>();

            // Register the cache
            _cache = Mock.Of<ICache>();
            _cacheMock = Mock.Get(_cache);
            _container.RegisterInstance(_cache);
            ServiceLocatorMock.Setup(x => x.GetInstance<ICache>()).Returns(() => _container.Resolve<ICache>());

            // Register the target to cache
            InjectionMember[] behaviors =
            {
                new InterceptionBehavior<CachingBehavior>(),
                new Interceptor<InterfaceInterceptor>()
            };
            _container.RegisterType<ICacheTargetStub, CacheTargetStub>(behaviors);
            _cacheTarget = _container.Resolve<ICacheTargetStub>();

            CreateInstance();
        }


        [TestMethod]
        public void GetRequiredInterfaces_Value_ReturnsCorrectInterfacesTypes()
        {
            // Assert
            CollectionAssert.AreEqual(new[] { typeof(ICacheableObject) }, TestObject.GetRequiredInterfaces().ToArray());
        }


        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Invoke_IfTargetIsNotIdentifyable_ThrowsException()
        {
            // Arrange
            var invocation = Mock.Of<IMethodInvocation>();
            Mock<IMethodInvocation> invocationMock = Mock.Get(invocation);
            invocationMock.SetupGet(x => x.Target).Returns(new object());

            // Act
            TestObject.Invoke(invocation, null);
        }

        [TestMethod]
        public void Invoke_MethodNotRequiringCaching_CacheIsNotQueried()
        {
            // Arrange
            const string key = "A";
            _cacheTarget.Key = key;

            // Act
            _cacheTarget.NotCachedMethod("1", 0);

            // Assert
            object value;
            _cacheMock.Verify(c => c.TryGet(key, out value), Times.Never);
        }

        [TestMethod]
        public void Invoke_MethodNotRequiringCaching_CacheStoresNoValue()
        {
            // Arrange
            const string key = "A";
            _cacheTarget.Key = key;

            // Act
            _cacheTarget.NotCachedMethod("1", 0);

            // Assert
            object value = 1;
            _cacheMock.Verify(c => c.Store(key, value), Times.Never);
        }

        [TestMethod]
        public void Invoke_MethodRequiringCacheReseting_CacheGetsReseted()
        {
            // Act
            _cacheTarget.ResetingCacheMethod();

            // Assert
            _cacheMock.Verify(c => c.Clear(), Times.Once);
        }

        [TestMethod]
        public void Invoke_MethodNotRequiringCacheReseting_CacheGetsNeverReseted()
        {
            // Act
            _cacheTarget.NotResetingCacheMethod();

            // Assert
            _cacheMock.Verify(c => c.Clear(), Times.Never);
        }

        [TestMethod]
        public void Invoke_MethodRequiringCaching_InspectsForMethodResult()
        {
            // Arrange
            const string key = "A";
            _cacheTarget.Key = key;

            // Act
            _cacheTarget.CachedMethod("1", 0);

            // Assert
            object value;
            _cacheMock.Verify(c => c.TryGet(key, out value), Times.Once);
        }

        [TestMethod]
        public void Invoke_MethodRequiringCachingAndItsResultNotInCache_StoresValue()
        {
            // Arrange
            const string key1 = "A", key2 = "B";
            var secondCacheTarget = _container.Resolve<ICacheTargetStub>();
            secondCacheTarget.Key = key2;
            _cacheTarget.Key = key1;
            
            // Act
            int result1 = _cacheTarget.CachedMethod("1", 0);
            int result2 = secondCacheTarget.CachedMethod("2", 0);

            // Assert
            object value1 = result1;
            object value2 = result2;
            _cacheMock.Verify(c => c.Store(key1, value1), Times.Once);
            _cacheMock.Verify(c => c.Store(key2, value2), Times.Once);
        }

        [TestMethod]
        public void Invoke_MethodRequiringCachingAndItsResultInCache_DoesNotStoreValue()
        {
            // Arrange
            object value = 1;
            const string key = "A";
            _cacheTarget.Key = key;
            _cacheMock.Setup(c => c.TryGet(key, out value)).Returns(true);

            // Act
            int result = _cacheTarget.CachedMethod("1", 0);

            // Assert
            value = result;
            _cacheMock.Verify(c => c.Store(key, value), Times.Never);
        }

        [TestMethod, ExpectedException(typeof(DataMisalignedException))]
        public void Invoke_MethodRequiringCachingAndReturnsException_ThrowsTheException()
        {
            // Arrange
            object value;
            const string key = "A";
            _cacheTarget.Key = key;
            _cacheMock.Setup(c => c.TryGet(key, out value)).Returns(false);

            // Assert
            _cacheTarget.ExceptionMethod("1");
        }


        [TestMethod]
        public void WillExecute_Value_ReturnsAlwaysTrue()
        {
            // Assert
            Assert.IsTrue(TestObject.WillExecute);
        }
    }
}