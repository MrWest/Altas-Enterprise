using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Presentation.ViewModels
{
    [TestClass]
    public class WageScaleViewModelTests : CrudViewModelTestsBase<WageScaleViewModel, IWageScale, IWageScalePresenter, IWageScaleManagerApplicationServices>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        [TestMethod]
        public void WageScale_ReturnsOwnItemsList()
        {
            // Arrange
            IWageScalePresenter[] wageScales = { Mock.Of<IWageScalePresenter>(), Mock.Of<IWageScalePresenter>() };
            TestMock.Setup(x => x.Items).Returns(wageScales);

            // Act
            IEnumerable<IWageScalePresenter> actualWageScale = ((IWageScaleProvider)TestObject).WageScales;

            // Assert
            CollectionAssert.AreEquivalent(wageScales, actualWageScale.ToArray());
        }

        [TestMethod]
        public void WageScale_IsStaticMember_ReturnsOwnItemsList()
        {
            // Arrange
            IWageScalePresenter[] wageScales = { Mock.Of<IWageScalePresenter>(), Mock.Of<IWageScalePresenter>() };
            TestMock.Setup(x => x.Items).Returns(wageScales);

            ServiceLocatorMock.Setup(x => x.GetInstance<IWageScaleProvider>()).Returns(TestObject);

            // Act
            IEnumerable<IWageScalePresenter> actualWageScale = WageScaleViewModel.WageScales;

            // Assert
            CollectionAssert.AreEquivalent(wageScales, actualWageScale.ToArray());
        }
    }
}