using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Implementation.Application;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Application
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InvestmentComponentManagerApplicationServicesTests :
        ItemManagerApplicationServicesTestsBase<InvestmentComponentManagerApplicationServicesTests.InvestmentComponentManagerApplicationServicesStub, IInvestmentComponent, IInvestmentComponentRepository, IInvestmentComponentDomainServices, IUnitOfWork>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IInvestmentComponentRepository>()).Returns(() =>
            {
                var mock = new Mock<IInvestmentComponentRepository>();
                mock.SetupAllProperties();

                return mock.Object;
            });

            ServiceLocatorMock.Setup(x => x.GetInstance<IInvestmentComponentDomainServices>()).Returns(() =>
            {
                var mock = new Mock<IInvestmentComponentDomainServices>();
                mock.SetupAllProperties();

                return mock.Object;
            });
        }


        [TestMethod]
        public void Repository_ReturnsCorrectInstanceWithTheParentInvestmentElementSet()
        {
            // Arrange
            var investmentElement = Mock.Of<IInvestmentElement>();
            TestObject.InvestmentElement = investmentElement;

            // Act
            IInvestmentComponentRepository repository = TestObject.Repository;

            // Assert
            Assert.AreSame(investmentElement, repository.InvestmentElement);
        }


        [TestMethod]
        public void GetKeyFor_ReturnsCorrectKey()
        {
            // Arrange
            Type servicesType = typeof(InvestmentComponentManagerApplicationServices);
            MethodInfo deleteMethod = servicesType.GetMethod("Delete");

            TestObject.InvestmentElement = Mock.Of<IInvestmentElement>(x => (int)x.Id == 1);

            // Act
            string key = TestObject.GetKeyFor(deleteMethod, Mock.Of<IInvestmentComponent>());

            // Assert
            Assert.IsTrue(key.StartsWith("1->"));
        }


        public class InvestmentComponentManagerApplicationServicesStub : InvestmentComponentManagerApplicationServices
        {
            public new IInvestmentComponentRepository Repository
            {
                get { return base.Repository; }
            }

            public new IInvestmentComponentDomainServices DomainServices
            {
                get { return base.DomainServices; }
            }
        }
    }
}
