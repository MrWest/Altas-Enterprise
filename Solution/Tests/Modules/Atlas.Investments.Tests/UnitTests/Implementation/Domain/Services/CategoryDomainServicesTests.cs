using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Services
{
    [TestClass, ExcludeFromCodeCoverage]
    public class CategoryDomainServicesTests : DomainServicesTestsBase<ICategory, CategoryDomainServices>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<ICategory>()).Returns(() =>
            {
                var mock = new Mock<ICategory>();
                mock.SetupAllProperties();

                return mock.Object;
            });
        }


        [TestMethod]
        public void Create_CreatesNewInstanceOfCategory()
        {
            // Act
            ICategory oace = TestObject.Create();

            // Assert
            Assert.IsNotNull(oace);
        }

        [TestMethod]
        public void Create_CreatesCategoryWithCorrectName()
        {
            // Act
            ICategory oace = TestObject.Create();

            // Assert
            Assert.AreEqual(Resources.NewCategory_Name, oace.Name);
        }

        [TestMethod]
        public void Create_CreatesCategoryWithCorrectDescription()
        {
            // Act
            ICategory oace = TestObject.Create();

            // Assert
            Assert.AreEqual(Resources.NewCategory_Description, oace.Description);
        }
    }
}