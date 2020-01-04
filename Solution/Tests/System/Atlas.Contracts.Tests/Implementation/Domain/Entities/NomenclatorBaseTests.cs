using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Domain.Entities
{
    [TestClass, ExcludeFromCodeCoverage]
    public class NomenclatorBaseTests
    {
        [TestMethod]
        public void ToString_ReturnsCorrestStringRepresentation()
        {
            // Arrange
            var nomenclator = new Mock<NomenclatorBase>() { CallBase = true }.Object;
            nomenclator.Name = "123";
            nomenclator.Description = "456";
            
            // Act
            string actualString = nomenclator.ToString();

            // Assert
            Assert.AreEqual("123", actualString);
        }
    }
}
