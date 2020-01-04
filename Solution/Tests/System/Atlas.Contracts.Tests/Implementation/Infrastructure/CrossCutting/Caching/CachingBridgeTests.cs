using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Caching;
using CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.CrossCutting.Caching.Stubs;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.CrossCutting.Caching
{
    [TestClass, ExcludeFromCodeCoverage]
    public class CachingBridgeTests : TestBase<CachingBridge>
    {
        private Mock<ICacheManager> _cacheManagerMock;
        private ICacheManager _cacheManager;


        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            _cacheManagerMock = new Mock<ICacheManager>();
            _cacheManager = _cacheManagerMock.Object;
            ServiceLocatorMock.Setup(x => x.GetInstance<ICacheManager>()).Returns(_cacheManager);
        }


        [TestMethod]
        public void Get_GivenExistingKey_ReturnsValue()
        {
            // Act
            _cacheManagerMock.SetupGet(c => c["A"]).Returns(12);
            object result = TestObject.Get("A");

            // Assert
            Assert.AreEqual(12, result);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Get_GivenNullKey_ThrowsException()
        {
            // Act
            TestObject.Get(null);
        }

        [TestMethod]
        public void Get_GivenUnexistingKey_ReturnsValue()
        {
            // Act
            _cacheManagerMock.SetupGet(c => c["B"]).Returns(12);
            object result = TestObject.Get("A");

            // Assert
            Assert.IsNull(result);
        }


        [TestMethod]
        public void Clear_Called_FlushesCache()
        {
            // Act
            TestObject.Clear();

            // Assert
            _cacheManagerMock.Verify(c => c.Flush(), Times.Once);
        }


        [TestMethod]
        public void Store_GivenKeyAndValue_StoresValueInCache()
        {
            // Arrange
            const string key = "A";
            object value = 12;
            var cacheManager = new CacheManagerStub();
            ServiceLocatorMock.Setup(x => x.GetInstance<ICacheManager>()).Returns(cacheManager);
            TestObject = new CachingBridge();

            // Act
            TestObject.Store(key, value);

            // Assert
            Assert.IsTrue(cacheManager.Added);
        }


        [TestMethod]
        public void TryGet_GivenExistingKey_ReturnsCorrectValue()
        {
            // Arrange
            _cacheManagerMock.SetupGet(c => c["A"]).Returns(12);

            // Act
            object value;
            TestObject.TryGet("A", out value);

            // Assert
            Assert.AreEqual(12, value);
        }

        [TestMethod]
        public void TryGet_GivenExistingKey_ReturnsTrue()
        {
            // Arrange
            const string key = "A";
            _cacheManagerMock.SetupGet(c => c[key]).Returns(12);
            _cacheManagerMock.Setup(c => c.Contains(key)).Returns(true);

            // Act
            object value;
            bool exists = TestObject.TryGet(key, out value);

            // Assert
            Assert.IsTrue(exists);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void TryGet_GivenNullKey_ThrowsException()
        {
            // Act
            object value;
            TestObject.TryGet(null, out value);
        }

        [TestMethod]
        public void TryGet_GivenUnexistingKey_ReturnsFalse()
        {
            // Arrange
            const string key = "B";
            _cacheManagerMock.SetupGet(c => c["A"]).Returns(12);
            _cacheManagerMock.Setup(c => c.Contains(key)).Returns(false);

            // Act
            object value;
            bool exists = TestObject.TryGet(key, out value);

            // Assert
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void TryGet_GivenUnexistingKey_ReturnsNull()
        {
            // Arrange
            _cacheManagerMock.SetupGet(c => c["A"]).Returns(12);

            // Act
            object value;
            TestObject.TryGet("B", out value);

            // Assert
            Assert.IsNull(value);
        }
    }
}