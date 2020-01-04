using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Presentation.ViewModels
{
    [TestClass, ExcludeFromCodeCoverage]
    public class WorkForcePresenterTests : PresenterTestsBase<IWorkForce, WorkForcePresenter, IWorkForceManagerApplicationServices>
    {
        private Mock<IWageScaleProvider> _wageScaleProvider;


        [TestInitialize]
        public override void Initialize()
        {
            _wageScaleProvider = new Mock<IWageScaleProvider>();

            base.Initialize();
        }

        protected override void CreateInstance()
        {
            ServiceLocatorMock.Setup(x => x.GetInstance<IWageScaleProvider>()).Returns(_wageScaleProvider.Object);
            base.CreateInstance();
        }


        [TestMethod]
        public void Name_GivenSameValue_DoesNotNotifyPropertyChanges()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.Name, () => "A");
        }

        [TestMethod]
        public void Name_GivenDifferentValue_NotifiesPropertyChanges()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.Name, () => "A", () => "B");
        }

        [TestMethod]
        public void Name_GivenDifferentValue_GetsSetValue()
        {
            TestGetsSetValue(x => x.Name, () => "A", false);
        }


        [TestMethod]
        public void Code_GivenSameValue_DoesNotNotifyPropertyChanges()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.Code, () => "A");
        }

        [TestMethod]
        public void Code_GivenDifferentValue_NotifiesPropertyChanges()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.Code, () => "A", () => "B");
        }

        [TestMethod]
        public void Code_GivenDifferentValue_GetsSetValue()
        {
            TestGetsSetValue(x => x.Code, () => "A", false);
        }


        [TestMethod]
        public void WageScales_ReturnsWageScalesPresentersList()
        {
            // Arrange
            IWageScalePresenter[] wageScales = { Mock.Of<IWageScalePresenter>(), Mock.Of<IWageScalePresenter>() };
            _wageScaleProvider.Setup(x => x.WageScales).Returns(wageScales);

            // Act
            IEnumerable<IWageScalePresenter> actualWageScale = TestObject.WageScales;

            // Assert
            CollectionAssert.AreEquivalent(wageScales, actualWageScale.ToArray());
        }


        [TestMethod]
        public void Retribution_GetsSelectedWageScaleRetribution()
        {
            // Arrange
            var wageScale = Mock.Of<IWageScalePresenter>(x => x.Retribution == 6.0m);
            TestObject.WageScale = wageScale;

            // Act
            decimal retribution = TestObject.Retribution;

            // Assert
            Assert.AreEqual(6.0m, retribution);
        }


        [TestMethod]
        public void WageScale_GivenSameValue_DoesNotNotifyPropertyChanges()
        {
            // Arrange
            var wageScale = Mock.Of<IWageScale>();
            var wageScalePresenter = Mock.Of<IWageScalePresenter>(x => x.Object == wageScale);

            // Act, Assert
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.WageScale, () => wageScalePresenter);
        }

        [TestMethod]
        public void WageScale_GivenDifferentValue_NotifiesPropertyChanges()
        {
            // Arrange
            var wageScale1 = Mock.Of<IWageScale>();
            var wageScalePresenter1 = Mock.Of<IWageScalePresenter>(x => x.Object == wageScale1);
            var wageScale2 = Mock.Of<IWageScale>();
            var wageScalePresenter2 = Mock.Of<IWageScalePresenter>(x => x.Object == wageScale2);

            // Act, Assert
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.WageScale, () => wageScalePresenter1, () => wageScalePresenter2);
        }

        [TestMethod]
        public void WageScale_GivenDifferentValue_NotifiesRetributionPropertyChanges()
        {
            // Arrange
            var wageScale = Mock.Of<IWageScale>();
            var wageScalePresenter = Mock.Of<IWageScalePresenter>(x => x.Object == wageScale);

            // Act
            TestGetsSetValue(x => x.WageScale, () => wageScalePresenter, false);

            // Assert
            AssertNotified(x => x.Retribution);
        }

        [TestMethod]
        public void WageScale_GivenDifferentValue_GetsSetValue()
        {
            var wageScale1 = Mock.Of<IWageScale>();
            var wageScalePresenter1 = Mock.Of<IWageScalePresenter>(x => x.Object == wageScale1);

            TestGetsSetValue(x => x.WageScale, () => wageScalePresenter1, false);
        }

        [TestMethod]
        public void WageScale_GivenDifferentValue_NotifiesThatWageScalePropertyHasChanged()
        {
            // Arrange
            var wageScale = new Mock<IWageScalePresenter>();
            TestObject.WageScale = wageScale.Object;

            // Act
            wageScale.Raise(x => x.PropertyChanged += null, new PropertyChangedEventArgs("Name"));

            /* Assert, there is expected two notifications, one for the property assignment and another because of the wage 
             * scale Name's property change */
            AssertNotified(x => x.WageScale, 2);
        }

        [TestMethod]
        public void WageScale_GivenFirstRead_AndUnderlyingWorkForceHasAWageScale_ListensForSuchWageScalePropertyChanges()
        {
            // Arrange
            Entity.WageScale = Mock.Of<IWageScale>(x => (int)x.Id == 1);
            
            var wageScalePresenter = new Mock<IWageScalePresenter>();
            wageScalePresenter.SetupGet(x => x.Object).Returns(Entity.WageScale);

            _wageScaleProvider.SetupGet(x => x.WageScales).Returns(new[] { wageScalePresenter.Object });

            Console.WriteLine(TestObject.WageScale);

            // Act
            wageScalePresenter.Raise(x => x.PropertyChanged += null, new PropertyChangedEventArgs("Name"));

            /* Assert, there is expected two notifications, one for the property assignment and another because of the wage 
             * scale Name's property change */
            AssertNotified(x => x.WageScale);
        }

        [TestMethod]
        public void WageScale_GivenDifferentValue_NotifiesThatRetributionPropertyHasChanged()
        {
            // Arrange
            var wageScale = new Mock<IWageScalePresenter>();
            TestObject.WageScale = wageScale.Object;

            // Act
            wageScale.Raise(x => x.PropertyChanged += null, new PropertyChangedEventArgs("Name"));

            /* Assert, there is expected two notifications, one for the property assignment and another because of the wage 
            * scale Name's property change */
            AssertNotified(x => x.Retribution, 2);
        }

        [TestMethod]
        public void WageScale_GivenDifferentValue_AndHadAPreviousValue_DoesNotNotifyWageScalePropertyChangesFromPreviouseWageScalePresenter()
        {
            // Arrange
            var formerWageScale = new Mock<IWageScalePresenter>();
            var wageScale = new Mock<IWageScalePresenter>();
            TestObject.WageScale = formerWageScale.Object;
            TestObject.WageScale = wageScale.Object;

            // Act
            formerWageScale.Raise(x => x.PropertyChanged += null, new PropertyChangedEventArgs("Name"));

            /* Assert, there is expected two notifications, two only for the property assignment there is not expected
             * that when Name property of the previous wage scale changes */
            AssertNotified(x => x.WageScale, 2);
        }

        [TestMethod]
        public void WageScale_GivenDifferentValue_AndHadAPreviousValue_DoesNotNotifyRetributionPropertyChangesFromPreviouseWageScalePresenter()
        {
            // Arrange
            var formerWageScale = new Mock<IWageScalePresenter>();
            var wageScale = new Mock<IWageScalePresenter>();
            TestObject.WageScale = formerWageScale.Object;
            TestObject.WageScale = wageScale.Object;

            // Act
            formerWageScale.Raise(x => x.PropertyChanged += null, new PropertyChangedEventArgs("Name"));

            /* Assert, there is expected two notifications, two only for the property assignment there is not expected
             * that when Name property of the previous wage scale changes */
            AssertNotified(x => x.WageScale, 2);
        }
    }
}