using System;
using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Presentation.ViewModels.Budget
{
    [TestClass, ExcludeFromCodeCoverage]
    public class BudgetPresenterTests : PresenterTestsBase<IBudget, BudgetPresenter, IItemManagerApplicationServices<IBudget>>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void InvestmentElement_IfReadBeforeSet_Throws()
        {
            Console.WriteLine(TestObject.InvestmentElement);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void InvestmentElement_GivenNullInvestmentElement_Throws()
        {
            TestObject.InvestmentElement = null;
        }

        [TestMethod]
        public void InvestmentElement_GivenInvestmentElement_GetsSuchInvestmentElement()
        {
            // Arrange
            var investmentElement = Mock.Of<IInvestmentElementPresenter>();

            // Act
            TestObject.InvestmentElement = investmentElement;

            // Assert
            Assert.AreSame(TestObject.InvestmentElement, investmentElement);
        }


        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void EquipmentComponent_IfReadBeforeSet_Throws()
        {
            Console.WriteLine(TestObject.EquipmentComponent);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void EquipmentComponent_GivenNullEquipmentComponent_Throws()
        {
            TestObject.EquipmentComponent = null;
        }

        [TestMethod]
        public void EquipmentComponent_GivenEquipmentComponent_GetsSuchEquipmentComponent()
        {
            // Arrange
            var equipmentComponent = Mock.Of<IEquipmentComponentPresenter>();

            // Act
            TestObject.EquipmentComponent = equipmentComponent;

            // Assert
            Assert.AreSame(TestObject.EquipmentComponent, equipmentComponent);
        }


        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void ConstructionComponent_IfReadBeforeSet_Throws()
        {
            Console.WriteLine(TestObject.ConstructionComponent);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructionComponent_GivenNullConstructionComponent_Throws()
        {
            TestObject.ConstructionComponent = null;
        }

        [TestMethod]
        public void ConstructionComponent_GivenConstructionComponent_GetsSuchConstructionComponent()
        {
            // Arrange
            var constructionComponent = Mock.Of<IConstructionComponentPresenter>();

            // Act
            TestObject.ConstructionComponent = constructionComponent;

            // Assert
            Assert.AreSame(TestObject.ConstructionComponent, constructionComponent);
        }


        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void OtherExpensesComponent_IfReadBeforeSet_Throws()
        {
            Console.WriteLine(TestObject.OtherExpensesComponent);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void OtherExpensesComponent_GivenNullOtherExpensesComponent_Throws()
        {
            TestObject.OtherExpensesComponent = null;
        }

        [TestMethod]
        public void OtherExpensesComponent_GivenOtherExpensesComponent_GetsSuchOtherExpensesComponent()
        {
            // Arrange
            var otherExpensesComponent = Mock.Of<IOtherExpensesComponentPresenter>();

            // Act
            TestObject.OtherExpensesComponent = otherExpensesComponent;

            // Assert
            Assert.AreSame(TestObject.OtherExpensesComponent, otherExpensesComponent);
        }


        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void WorkCapitalComponent_IfReadBeforeSet_Throws()
        {
            Console.WriteLine(TestObject.WorkCapitalComponent);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WorkCapitalComponent_GivenNullWorkCapitalComponent_Throws()
        {
            TestObject.WorkCapitalComponent = null;
        }

        [TestMethod]
        public void WorkCapitalComponent_GivenWorkCapitalComponent_GetsSuchWorkCapitalComponent()
        {
            // Arrange
            var workCapitalComponent = Mock.Of<IWorkCapitalComponentPresenter>();

            // Act
            TestObject.WorkCapitalComponent = workCapitalComponent;

            // Assert
            Assert.AreSame(TestObject.WorkCapitalComponent, workCapitalComponent);
        }
    }
}
