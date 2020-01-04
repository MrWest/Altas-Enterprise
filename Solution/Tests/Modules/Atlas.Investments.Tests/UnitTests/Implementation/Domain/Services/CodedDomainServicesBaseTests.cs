using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Services
{
    [TestClass, ExcludeFromCodeCoverage]
    public class CodedDomainServicesBaseTests : DomainServicesTestsBase<ICodedNomenclator, CodedNomenclatorDomainServicesBase<ICodedNomenclator>>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<ICodedNomenclator>()).Returns(() =>
            {
                var mock = new Mock<ICodedNomenclator>();
                mock.SetupAllProperties();

                return mock.Object;
            });
        }


        [TestMethod]
        public void Create_CreatesNewInstanceOfCodedNomenclator()
        {
            // Act
            ICodedNomenclator nomenclator = TestObject.Create();

            // Assert
            Assert.IsNotNull(nomenclator);
        }

        [TestMethod]
        public void Create_CreatesOACEWithANotEmptyCode()
        {
            // Act
            ICodedNomenclator nomenclator = TestObject.Create();

            // Assert
            Assert.IsTrue(nomenclator.Code.Any());
        }
    }
}