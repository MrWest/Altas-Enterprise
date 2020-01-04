using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Domain.Entities
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InvestmentElementTests : TestBase<InvestmentElement>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        [TestMethod]
        public void Constructor_InitializesElementsProperty()
        {
            // Assert
            Assert.IsNotNull(TestObject.Elements);
        }
    }
}
