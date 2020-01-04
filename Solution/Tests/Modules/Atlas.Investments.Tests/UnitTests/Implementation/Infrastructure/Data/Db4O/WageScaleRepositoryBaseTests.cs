using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Infrastructure.Data.Db4O
{
    [TestClass, ExcludeFromCodeCoverage]
    public class WageScaleRepositoryBaseTests : MockedTestBase<WageScaleRepository>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void CreateMock()
        {
            TestMock = new Mock<WageScaleRepository>(Mock.Of<IDb4ODatabaseContext>()) { CallBase = true };
        }


        [TestMethod]
        public void RelevantProperties_ReturnsCorrectProperties()
        {
            // Act
            var actualProperties = GetProperty<string[]>(TestObject, "RelevantProperties");

            // Assert
            string[] expectedProperties =
            {
                ExtractPropertyName<IWageScale, object>(x => x.Id),
                ExtractPropertyName<IWageScale, string>(x => x.Name),
                ExtractPropertyName<IWageScale, decimal>(x => x.Retribution)
            };
            CollectionAssert.AreEquivalent(expectedProperties, actualProperties);
        }
    }
}
