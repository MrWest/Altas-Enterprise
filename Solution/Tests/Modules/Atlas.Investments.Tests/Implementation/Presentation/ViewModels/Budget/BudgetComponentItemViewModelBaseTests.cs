using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Presentation.ViewModels.Budget
{
    [TestClass, ExcludeFromCodeCoverage]
    public class BudgetComponentItemViewModelBaseTests :
        CrudViewModelTestsBase<BudgetComponentItemViewModelBaseTests.BudgetComponentItemViewModelStub, IBudgetComponentItem, IBudgetComponentItemPresenter<IBudgetComponentItem>, IBudgetComponentItemManagerApplicationServices<IBudgetComponentItem>>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            //ApplicationServicesMock.SetupProperty(x => x.Component);

            //TestObject.Component = Mock.Of<IBudgetComponent>();

            ServiceLocatorMock.Setup(x => x.GetInstance<IBudgetComponentItemPresenter<IBudgetComponentItem>>()).Returns(() =>
            {
                var mock = new Mock<IBudgetComponentItemPresenter<IBudgetComponentItem>>();
                mock.SetupAllProperties();

                return mock.Object;
            });
        }


        [TestMethod]
        public void Constructor_InitializesFilterCommand()
        {
            Assert.IsNotNull(TestObject.FilterCommand);
        }


        //[TestMethod, ExpectedException(typeof(InvalidOperationException))]
        //public void Component_IsNotSet_Throws()
        //{
        //    // Arrange
        //    CreateMock();
        //    CreateInstance();

        //    // Act
        //    Console.WriteLine(TestObject.Component);
        //}

        //[TestMethod]
        //public void Component_IsSet_ResturnsIt()
        //{
        //    // Arrange
        //    var component = Mock.Of<IBudgetComponent>();
        //    TestObject.Component = component;

        //    // Act
        //    IBudgetComponent actualComponent = TestObject.Component;

        //    // Assert
        //    Assert.AreSame(component, actualComponent);
        //}

        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void Component_IfGivenNot_Throws()
        //{
        //    TestObject.Component = null;
        //}


        //[TestMethod]
        //public void CreateServices_ReturnsServicesWithItsComponentInitialized()
        //{
        //    // Arrange
        //    var component = Mock.Of<IBudgetComponent>();
        //    TestObject.Component = component;

        //    // Act
        //    var services = (IBudgetComponentItemManagerApplicationServices<IBudgetComponentItem, IBudgetComponent>)TestObjectInternals.Invoke("CreateServices");

        //    // Assert
        //    Assert.AreSame(component, services.Component);
        //}


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreatePresenterFor_GivenNullItem_Throws()
        {
            TestObjectInternals.Invoke("CreatePresenterFor", (IBudgetComponentItem)null);
        }

        //[TestMethod]
        //public void CreatePresenterFor_ReturnsPresenterWithComponentCorrectlyInitialized()
        //{
        //    // Arrange
        //    TestObject.Component = Mock.Of<IBudgetComponent>();
        //    ServiceLocatorMock.Setup(x => x.GetInstance<IBudgetComponentItemPresenter<IBudgetComponentItem>>()).Returns(() =>
        //    {
        //        var mock = new Mock<IBudgetComponentItemPresenter<IBudgetComponentItem>>();
        //        mock.SetupAllProperties();

        //        return mock.Object;
        //    });

        //    // Act
        //    var presenter = (IBudgetComponentItemPresenter<IBudgetComponentItem>)TestObjectInternals.Invoke("CreatePresenterFor", Mock.Of<IBudgetComponentItem>());

        //    // Assert
        //    Assert.AreSame(TestObject.Component, presenter.Component);
        //}


        [TestMethod]
        public void FilterCommand_CanExecute_PassesCallToServices()
        {
            // Arrange
            ApplicationServicesMock.Setup(x => x.CanFilter("A")).Returns(true);

            // Act
            bool canFilter = TestObject.FilterCommand.CanExecute("A");

            // Assert
            Assert.IsTrue(canFilter);
        }

        [TestMethod]
        public void FilterCommand_CanExecute_AfterAChangeInABudgetComponentItem_TriggersAFilter()
        {
            // Arrange
            ApplicationServicesMock.Setup(x => x.CanFilter("A")).Returns(true);

            var item = new Mock<IBudgetComponentItemPresenter<IBudgetComponentItem>>();
            TestObject.Add(item.Object);

            // Act
            item.Raise(x => x.PropertyChanged += null, new PropertyChangedEventArgs("Name"));
            TestObject.FilterCommand.CanExecute("A");

            // Assert
            TestMock.Verify(x => x.Load());
        }

        [TestMethod]
        public void FilterCommand_Execute_ReloadsTheViewModel()
        {
            // Act
            TestObject.FilterCommand.Execute("A");

            // Assert
            TestMock.Verify(x => x.Load());
        }

        [TestMethod]
        public void FilterCommand_Execute_GivenCriteria_GetsTheCorrectItems()
        {
            // Arrange
            IBudgetComponentItem[] expectedItems =
            {
                Mock.Of<IBudgetComponentItem>(),
                Mock.Of<IBudgetComponentItem>()
            };
            IBudgetComponentItem[] unexpectedItems =
            {
                Mock.Of<IBudgetComponentItem>()
            };
            ApplicationServicesMock.Setup(x => x.Filter("A")).Returns(expectedItems);
            ApplicationServicesMock.Setup(x => x.Filter("")).Returns(unexpectedItems);

            // Act
            TestObject.FilterCommand.Execute("A");
            IBudgetComponentItem[] actualItems = (from presenter in TestObject.Items select presenter.Object).ToArray();

            // Assert
            CollectionAssert.AreEquivalent(expectedItems, actualItems);
            CollectionAssert.AreNotEquivalent(unexpectedItems, actualItems);
        }

        [TestMethod]
        public void FilterCommand_Execute_GivenEmptyCriteria_GetsTheAllItems()
        {
            // Arrange
            IBudgetComponentItem[] expectedItems =
            {
                Mock.Of<IBudgetComponentItem>(),
                Mock.Of<IBudgetComponentItem>()
            };
            IBudgetComponentItem[] unexpectedItems =
            {
                Mock.Of<IBudgetComponentItem>()
            };
            ApplicationServicesMock.Setup(x => x.Filter("")).Returns(unexpectedItems);
            ApplicationServicesMock.SetupGet(x => x.Items).Returns(expectedItems);

            // Act
            TestObject.FilterCommand.Execute("");
            IBudgetComponentItem[] actualItems = (from presenter in TestObject.Items select presenter.Object).ToArray();

            // Assert
            CollectionAssert.AreEquivalent(expectedItems, actualItems);
            CollectionAssert.AreNotEquivalent(unexpectedItems, actualItems);
        }


        [TestMethod]
        public void Add_LoadsTheViewModel()
        {
            // Act
            TestObject.Add(Mock.Of<IBudgetComponentItemPresenter<IBudgetComponentItem>>());

            // Assert
            TestMock.Verify(x => x.Load());
        }


        public class BudgetComponentItemViewModelStub : BudgetComponentItemViewModelBase<IBudgetComponentItem, IBudgetComponentItemPresenter<IBudgetComponentItem>, IBudgetComponentItemManagerApplicationServices<IBudgetComponentItem>>
        {
        }
    }
}