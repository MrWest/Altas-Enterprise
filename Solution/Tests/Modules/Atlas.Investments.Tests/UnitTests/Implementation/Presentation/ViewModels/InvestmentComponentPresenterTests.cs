using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Presentation.ViewModels
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InvestmentComponentPresenterTests :
        PresenterTestsBase<IInvestmentComponent, InvestmentComponentPresenter, IInvestmentComponentManagerApplicationServices>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ApplicationServicesMock.SetupProperty(x => x.InvestmentElement);
        }


        [TestMethod]
        public void Depth_IsCurrentPresenterChildOfRootInvestmentPresenter_Returns1()
        {
            // Arrange
            TestObject.Parent = Mock.Of<IInvestmentPresenter>();

            // Act
            int depth = TestObject.Depth;

            // Assert
            Assert.AreEqual(1, depth);
        }

        [TestMethod]
        public void Depth_IsForthLevelChildOfRootInvestmentPresenter_Returns3()
        {
            // Arrange
            TestObject.Parent = Mock.Of<ITreeNode>(x => x.Value == Mock.Of<IInvestmentElementPresenter>(y => y.Depth == 2));

            // Act
            int depth = TestObject.Depth;

            // Assert
            Assert.AreEqual(3, depth);
        }


        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Parent_IfReadBeforeAssigned_Throws()
        {
            Console.WriteLine(TestObject.Parent);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Parent_GivenNullValue_Throws()
        {
            TestObject.Parent = null;
        }

        [TestMethod]
        public void Parent_GivenValidInvestmentElementPresenter_SetsIt()
        {
            // Arrange
            var investmentElementPresenter = new Mock<IInvestmentElementPresenter>();

            // Act
            TestObject.Parent = investmentElementPresenter.Object;

            // Assert
            Assert.AreSame(investmentElementPresenter.Object, TestObject.Parent);
        }


        [TestMethod]
        public void CreateServices_ReturnsInstanceOfCorrectApplicationServicesWithParentCorrectlySet()
        {
            // Arrange
            var investmentElement = Mock.Of<IInvestmentElement>(x => x.Budget == EntityTestsHelpers.CreateBudget());
            TestObject.Parent = Mock.Of<ITreeNode>(x => x.Value == investmentElement);

            // Act
            var services = (IInvestmentComponentManagerApplicationServices)TestObjectInternals.Invoke("CreateServices");

            // Assert
            Assert.AreSame(investmentElement, services.InvestmentElement);
        }
    }
}
