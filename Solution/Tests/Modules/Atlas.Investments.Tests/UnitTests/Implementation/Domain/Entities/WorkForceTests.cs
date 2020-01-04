using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Entities
{
    [TestClass, ExcludeFromCodeCoverage]
    public class WorkForceTests
    {
        [TestMethod]
        public void ToString_ReturnsName()
        {
            // Arrange
            var workForce = new WorkForce { Name = "A" };

            // Act
            string toString = workForce.ToString();

            // Assert
            Assert.AreEqual(workForce.Name, toString);
        }
    }
}