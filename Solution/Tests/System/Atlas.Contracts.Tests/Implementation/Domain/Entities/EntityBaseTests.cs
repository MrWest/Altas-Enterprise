using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Domain.Entities
{
    [TestClass, ExcludeFromCodeCoverage]
    public class EntityBaseTests
    {
        [TestMethod]
        public void FullName_ReturnsToStringMethodValue()
        {
            // Arrange
            var entityBase = Mock.Of<EntityBase>();

            // Act
            string fullName = entityBase.FullName;

            // Assert
            Assert.AreEqual(entityBase.ToString(), fullName);
        }
    }
}
