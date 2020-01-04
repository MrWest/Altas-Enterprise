using System;
using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Services
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ExpenseConceptDomainServicesTests : DomainServicesTestsBase<IExpenseConcept, ExpenseConceptDomainServices>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IExpenseConcept>()).Returns(() =>
            {
                var mock = new Mock<IExpenseConcept>();
                mock.SetupAllProperties();

                return mock.Object;
            });
        }


        [TestMethod]
        public void Create_CreatesNewInstanceOfExpenseConcept()
        {
            // Act
            IExpenseConcept expenseConcept = TestObject.Create();

            // Assert
            Assert.IsNotNull(expenseConcept);
        }

        [TestMethod]
        public void Create_CreatesExpenseConceptWithCorrectName()
        {
            // Act
            IExpenseConcept expenseConcept = TestObject.Create();

            // Assert
            Assert.AreEqual(Resources.NewExpenseConcept_Name, expenseConcept.Name);
        }

        [TestMethod]
        public void Create_CreatesExpenseConceptWithCorrectCode()
        {
            // Act
            IExpenseConcept expenseConcept = TestObject.Create();

            // Assert
            Assert.IsNotNull(expenseConcept.Code);
            Assert.AreNotEqual(string.Empty, expenseConcept.Code);
            Assert.AreNotEqual(Guid.Empty.ToString(), expenseConcept.Code);
        }
    }
}