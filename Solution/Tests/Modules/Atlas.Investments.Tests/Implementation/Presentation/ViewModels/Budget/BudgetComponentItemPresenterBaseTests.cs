using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Presentation.ViewModels.Budget
{
    [TestClass, ExcludeFromCodeCoverage]
    public class BudgetComponentItemPresenterBaseTests :
        PresenterTestsBase<IBudgetComponentItem, BudgetComponentItemPresenterBaseTests.BudgetComponentItemPresenterStub, IBudgetComponentItemManagerApplicationServices<IBudgetComponentItem>>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            //ApplicationServicesMock.SetupProperty(x => x.Component);
            //TestObject.Component = Mock.Of<IBudgetComponent>();
        }


        [TestMethod]
        public void Quantity_CallsCorrectSetter()
        {
            // Act
            TestObject.Quantity = 90m;

            // Assert
            Assert.IsTrue(TestObject.CalledSetProperty);
        }

        [TestMethod]
        public void Quantity_GetsSetValue()
        {
            // Act
            TestObject.Quantity = 90m;

            // Assert
            Assert.AreEqual(90m, TestObject.Quantity);
        }


        [TestMethod]
        public void Code_CallsCorrectSetter()
        {
            // Act
            TestObject.Code = "1";

            // Assert
            Assert.IsTrue(TestObject.CalledSetProperty);
        }

        [TestMethod]
        public void Code_GetsSetValue()
        {
            // Act
            TestObject.Code = "1";

            // Assert
            Assert.AreEqual("1", TestObject.Code);
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
        //    var actualComponent = TestObject.Component;

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


        public class BudgetComponentItemPresenterStub : BudgetComponentItemPresenterBase<IBudgetComponentItem, IBudgetComponentItemManagerApplicationServices<IBudgetComponentItem>>
        {
            

            public bool CalledSetProperty { get; private set; }

            protected override bool SetProperty<TValue>(Action<TValue> setter, TValue value, [CallerMemberName] string propertyName = null)
            {
                CalledSetProperty = true;
                return base.SetProperty(setter, value, propertyName);
            }

            public override void NotifyUp()
            {
                throw new NotImplementedException();
            }
        }
    }
}
