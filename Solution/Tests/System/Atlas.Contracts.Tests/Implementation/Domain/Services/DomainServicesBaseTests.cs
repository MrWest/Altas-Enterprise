using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Validation;
using CompanyName.Atlas.Contracts.Implementation.Domain.Services;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Domain.Services
{
    [TestClass, ExcludeFromCodeCoverage]
    public class DomainServicesBaseTests : DomainServicesTestsBase<IEntity, DomainServicesBase<IEntity>>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        [TestMethod]
        public void Create_ReturnsANewInstanceOfEntity()
        {
            // Arrange
            var entity = Mock.Of<IEntity>();
            ServiceLocatorMock.Setup(x => x.GetInstance<IEntity>()).Returns(entity);

            // Act
            var createdEntity = TestObject.Create();

            // Assert
            Assert.AreSame(entity, createdEntity);
        }


        [TestMethod]
        public void CanAdd_GivenEntity_ReturnsTrue()
        {
            // Assert
            Assert.IsTrue(TestObject.CanAdd(Mock.Of<IEntity>()));
        }

        [TestMethod]
        public void CanAdd_ReturnsTrue()
        {
            // Assert
            Assert.IsTrue(TestObject.CanAdd());
        }


        [TestMethod]
        public void CanDelete_GivenNullEntity_ReturnsFalse()
        {
            // Assert
            Assert.IsFalse(TestObject.CanDelete(null));
        }

        [TestMethod]
        public void CanDelete_GivenEntity_ReturnsTrue()
        {
            // Assert
            Assert.IsTrue(TestObject.CanDelete(Mock.Of<IEntity>()));
        }


        [TestMethod]
        public void CanUpdate_GivenNullEntity_ReturnFalse()
        {
            // Act
            bool canUpdate = TestObject.CanUpdate(null);

            // Assert
            Assert.IsFalse(canUpdate);
        }

        [TestMethod]
        public void CanUpdate_GivenEntity_ReturnTrue()
        {
            // Arrange
            var entity = Mock.Of<IEntity>();

            // Act
            bool canUpdate = TestObject.CanUpdate(entity);

            // Assert
            Assert.IsTrue(canUpdate);
        }


        [TestMethod]
        public void Validate_ContainsTheCorrectBaseImplementationForValidation()
        {
            // Arrange
            var entity = Mock.Of<IEntity>();
            string[] errors = { "Error" };

            var validator = Mock.Of<IValidator<IEntity>>(x => x.Validate(entity) == errors);
            var factory = Mock.Of<IValidatorFactory>(x => x.CreateValidator<IEntity>() == validator);

            ServiceLocatorMock.Setup(x => x.GetInstance<IValidatorFactory>()).Returns(factory);

            // Act
            var actualErrors = TestObject.Validate(entity);

            // Assert
            Assert.AreSame(errors, actualErrors);
        }
    }
}
