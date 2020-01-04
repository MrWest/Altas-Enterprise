using System;
using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Presentation.ViewModels
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InvestmentComponentViewModelTests :
        CrudViewModelTestsBase<InvestmentComponentViewModel, IInvestmentComponent, IInvestmentComponentPresenter, IInvestmentComponentManagerApplicationServices>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IInvestmentComponentPresenter>()).Returns(() =>
            {
                var mock = new Mock<IInvestmentComponentPresenter>();
                mock.SetupAllProperties();

                return mock.Object;
            });

            ServiceLocatorMock.Setup(x => x.GetInstance<IBudgetPresenter>()).Returns(() =>
            {
                var mock = new Mock<IBudgetPresenter>();
                mock.SetupAllProperties();

                return mock.Object;
            });

            ServiceLocatorMock.Setup(x => x.GetInstance<IEquipmentComponentPresenter>()).Returns(() =>
            {
                var mock = new Mock<IEquipmentComponentPresenter>();
                mock.SetupAllProperties();

                return mock.Object;
            });
            ServiceLocatorMock.Setup(x => x.GetInstance<IConstructionComponentPresenter>()).Returns(() =>
            {
                var mock = new Mock<IConstructionComponentPresenter>();
                mock.SetupAllProperties();

                return mock.Object;
            });
            ServiceLocatorMock.Setup(x => x.GetInstance<IOtherExpensesComponentPresenter>()).Returns(() =>
            {
                var mock = new Mock<IOtherExpensesComponentPresenter>();
                mock.SetupAllProperties();

                return mock.Object;
            });
            ServiceLocatorMock.Setup(x => x.GetInstance<IWorkCapitalComponentPresenter>()).Returns(() =>
            {
                var mock = new Mock<IWorkCapitalComponentPresenter>();
                mock.SetupAllProperties();

                return mock.Object;
            });
        }

        protected override void CreateApplicationServicesMock()
        {
            base.CreateApplicationServicesMock();
            ApplicationServicesMock.SetupProperty(x => x.InvestmentElement);
        }


        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Parent_IfReadBeforeAssigned_Throws()
        {
            Console.WriteLine(TestObject.InvestmentElement);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Parent_GivenNull_Throws()
        {
            TestObject.InvestmentElement = null;
        }

        [TestMethod]
        public void Parent_GivenCorrectInvestmentElementPresenter_SetsIt()
        {
            // Arrange
            var investmentElementPresenter = Mock.Of<IInvestmentElementPresenter>();

            // Act
            TestObject.InvestmentElement = investmentElementPresenter;

            // Assert
            Assert.AreSame(investmentElementPresenter, TestObject.InvestmentElement);
        }


        [TestMethod]
        public void CreateServices_ReturnsInstanceOfCorrectApplicationServicesWithParentCorrectlySet()
        {
            // Arrange
            var investmentElement = Mock.Of<IInvestmentElement>();
            var investmentElementPresenter = Mock.Of<IInvestmentElementPresenter>(x => x.Object == investmentElement);
            TestObject.InvestmentElement = investmentElementPresenter;

            // Act
            var services = (IInvestmentComponentManagerApplicationServices)TestObjectInternals.Invoke("CreateServices");

            // Assert
            Assert.AreSame(investmentElement, services.InvestmentElement);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreatePresenterFor_GivenNull_Throws()
        {
            TestObjectInternals.Invoke("CreatePresenterFor", (IInvestmentComponent)null);
        }

        [TestMethod]
        public void CreatePresenterFor_GivenInvestmentComponent_InitializesItsParent()
        {
            // Arrange
            TestObject.InvestmentElement = Mock.Of<IInvestmentElementPresenter>();
            var investmentComponent = Mock.Of<IInvestmentComponent>(x => x.Budget == EntityTestsHelpers.CreateBudget());

            // Act
            var componentPresenter = (IInvestmentComponentPresenter)TestObjectInternals.Invoke("CreatePresenterFor", investmentComponent);

            // Assert
            Assert.AreSame(TestObject.InvestmentElement, componentPresenter.Parent);
        }
    }
}