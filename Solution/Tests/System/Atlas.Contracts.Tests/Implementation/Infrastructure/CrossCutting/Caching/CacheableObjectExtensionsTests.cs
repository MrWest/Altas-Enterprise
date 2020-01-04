using System;
using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.CrossCutting.Caching
{
    [TestClass, ExcludeFromCodeCoverage]
    public class CacheableObjectExtensionsTests
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetKeyFor_GivenNullCacheableObject_ThrowsException()
        {
            // Act
            CacheableObjectExtensions.GetKeyFor(null, string.Empty, Mock.Of<IEntity>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetKeyFor_GivenNullBaseKey_ThrowsException()
        {
            // Arrange
            var cacheableObject = Mock.Of<ICacheableObject>();

            // Act
            cacheableObject.GetKeyFor((string)null, Mock.Of<IEntity>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetKeyFor_GivenNullEntity_ThrowsException()
        {
            // Arrange
            var cacheableObject = Mock.Of<ICacheableObject>();

            // Act
            cacheableObject.GetKeyFor<IEntity>("A", null);
        }

        [TestMethod]
        public void GetKeyFor_GivenRightArguments_ReturnsCorrectString()
        {
            // Arrange
            var cacheableObjectMock = new Mock<ICacheableObject>();
            ICacheableObject cacheableObject = cacheableObjectMock.Object;

            var entity = Mock.Of<IEntity>(x => (string)x.Id == "Id");

            // Act
            string key = cacheableObject.GetKeyFor("M12", entity);

            // Assert
            Assert.AreEqual("Id->M12", key);
        }
    }
}
