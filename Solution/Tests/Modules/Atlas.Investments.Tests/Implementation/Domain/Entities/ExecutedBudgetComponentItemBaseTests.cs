using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Domain.Entities
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ExecutedBudgetComponentItemBaseTests : MockedTestBase<ExecutedBudgetComponentItemBase>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        [TestMethod]
        public void Code_IfNoPlanification_ReturnsOwnValue()
        {
            // Act
            TestObject.Code = "1";

            // Assert
            Assert.AreEqual("1", TestObject.Code);
        }

        [TestMethod]
        public void Code_IfPlanification_ReturnsPlanificationCodeIgnoringItsOwn()
        {
            // Arrange
            TestObject.Planification = Mock.Of<IPlannedBudgetComponentItem>(x => x.Code == "2");
            
            // Act
            TestObject.Code = "1";

            // Assert
            Assert.AreEqual("2", TestObject.Code);
        }


        [TestMethod]
        public void Name_IfNoPlanification_ReturnsOwnValue()
        {
            // Act
            TestObject.Name = "1";

            // Assert
            Assert.AreEqual("1", TestObject.Name);
        }

        [TestMethod]
        public void Name_IfPlanification_ReturnsPlanificationNameIgnoringItsOwn()
        {
            // Arrange
            TestObject.Planification = Mock.Of<IPlannedBudgetComponentItem>(x => x.Name == "2");

            // Act
            TestObject.Name = "1";

            // Assert
            Assert.AreEqual("2", TestObject.Name);
        }


        [TestMethod]
        public void Description_IfNoPlanification_ReturnsOwnValue()
        {
            // Act
            TestObject.Description = "1";

            // Assert
            Assert.AreEqual("1", TestObject.Description);
        }

        [TestMethod]
        public void Description_IfPlanification_ReturnsPlanificationDescriptionIgnoringItsOwn()
        {
            // Arrange
            TestObject.Planification = Mock.Of<IPlannedBudgetComponentItem>(x => x.Description == "2");

            // Act
            TestObject.Description = "1";

            // Assert
            Assert.AreEqual("2", TestObject.Description);
        }


        [TestMethod]
        public void Quantity_IfNoPlanification_ReturnsOwnValue()
        {
            // Act
            TestObject.Quantity = 1;

            // Assert
            Assert.AreEqual(1, TestObject.Quantity);
        }

        [TestMethod]
        public void Quantity_IfPlanification_ReturnsPlanificationQuantityIgnoringItsOwn()
        {
            // Arrange
            TestObject.Planification = Mock.Of<IPlannedBudgetComponentItem>(x => x.Quantity == 2);

            // Act
            TestObject.Quantity = 1;

            // Assert
            Assert.AreEqual(2, TestObject.Quantity);
        }
    }
}
