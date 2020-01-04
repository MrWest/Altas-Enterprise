using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.Practices.Prism.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Contracts.Infrastructure.Data;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Application
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ItemManagerApplicationServicesBaseTests :
        ItemManagerApplicationServicesTestsBase<
            ItemManagerApplicationServicesBase<IEntity, IRepository<IEntity>, IDomainServices<IEntity>>,
            IEntity, IRepository<IEntity>, IDomainServices<IEntity>, IUnitOfWork>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        [TestMethod]
        public void Items_ReturnsItemsFromRepository()
        {
            // Arrange
            IEntity[] entitys = { Mock.Of<IEntity>(), Mock.Of<IEntity>() };
            RepositoryMock.Setup(x => x.Entities).Returns(entitys);

            // Act
            IEntity[] actualItems = TestObject.Items.ToArray();

            // Assert
            CollectionAssert.AreEquivalent(entitys, actualItems);
        }


        [TestMethod]
        public void Add_GivenItem_AddsItToTheRepository()
        {
            // Arrange
            var entity = Mock.Of<IEntity>();

            // Act
            TestObject.Add(entity);

            // Assert
            RepositoryMock.Verify(x => x.Add(entity));
        }

        [TestMethod]
        public void Add_GivenItem_LogsCorrectly()
        {
            // Arrange
            var entity = Mock.Of<IEntity>();

            // Act
            TestObject.Add(entity);

            // Assert
            LoggerMock.Verify(x => x.Log("Adding: \"{0}\"".EasyFormat(entity), Category.Debug, Priority.Medium));
            LoggerMock.Verify(x => x.Log("Added: \"{0}\"".EasyFormat(entity), Category.Info, Priority.Medium));
        }


        [TestMethod]
        public void Delete_GivenItem_DeletesItFromTheRepository()
        {
            // Arrange
            var entity = Mock.Of<IEntity>();

            // Act
            TestObject.Delete(entity);

            // Assert
            RepositoryMock.Verify(x => x.Delete(entity));
        }

        [TestMethod]
        public void Delete_GivenItem_LogsCorrectly()
        {
            // Arrange
            var entity = Mock.Of<IEntity>();

            // Act
            TestObject.Delete(entity);

            // Assert
            LoggerMock.Verify(x => x.Log("Deleting: \"{0}\"".EasyFormat(entity), Category.Debug, Priority.Medium));
            LoggerMock.Verify(x => x.Log("Deleted: \"{0}\"".EasyFormat(entity), Category.Info, Priority.Medium));
        }

        [TestMethod]
        public void DeleteAll_PassesCallRepository()
        {
            // Act
            TestObject.DeleteAll();

            // Assert
            RepositoryMock.Verify(x => x.DeleteAll());
        }


        [TestMethod]
        public void Update_GivenItem_CommandsRepositoryToUpdateTheItem()
        {
            // Arrange
            var entity = Mock.Of<IEntity>();

            // Act
            TestObject.Update(entity);

            // Assert
            RepositoryMock.Verify(x => x.Update(entity));
        }

        [TestMethod]
        public void Update_GivenItem_LogsCorrectly()
        {
            // Arrange
            var entity = Mock.Of<IEntity>();

            // Act
            TestObject.Update(entity);

            // Assert
            LoggerMock.Verify(x => x.Log("Updating: \"{0}\"".EasyFormat(entity), Category.Debug, Priority.Medium));
            LoggerMock.Verify(x => x.Log("Updated: \"{0}\"".EasyFormat(entity), Category.Info, Priority.Medium));
        }


        [TestMethod]
        public void Create_ReturnsANewInstanceFromDomainService()
        {
            // Arrange
            var entity = Mock.Of<IEntity>();
            DomainServicesMock.Setup(x => x.Create()).Returns(entity);

            // Act
            IEntity newEntity = TestObject.Create();

            // Assert
            Assert.AreSame(entity, newEntity);
        }


        [TestMethod]
        public void CanAdd_GivenItem_PassesCallToDomainService()
        {
            // Arrange
            var entity = Mock.Of<IEntity>();

            // Act
            TestObject.CanAdd(entity);

            // Assert
            DomainServicesMock.Verify(x => x.CanAdd(entity));
        }


        [TestMethod]
        public void CanAddNew_GivenNoItem_PassesCallToDomainServices()
        {
            // Arrange
            DomainServicesMock.Setup(x => x.CanAdd()).Returns(true);

            // Act
            bool canAdd = TestObject.CanAddNew();

            // Assert
            Assert.IsTrue(canAdd);
        }


        [TestMethod]
        public void CanUpdate_PassesCallToDomainServices()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            DomainServicesMock.Setup(x => x.CanUpdate(item)).Returns(true);

            // Act
            bool canUpdate = TestObject.CanUpdate(item);

            // Assert
            Assert.IsTrue(canUpdate);
        }


        [TestMethod]
        public void CanDelete_GivenItem_PassesCallToDomainServices()
        {
            // Arrange
            var entity = Mock.Of<IEntity>();
            DomainServicesMock.Setup(x => x.CanDelete(entity)).Returns(true);

            // Arrange
            bool canDelete = TestObject.CanDelete(entity);

            // Assert
            Assert.IsTrue(canDelete);
        }


        [TestMethod]
        public void GetKeyFor_GivenMethodAndItsArguments_ReturnsCorrectKey()
        {
            // Arrange
            Type appServiceType = TestObject.GetType();
            MethodInfo method = appServiceType.GetMethod("Delete");
            object[] arguments = { Mock.Of<IEntity>() };

            // Act
            string key = TestObject.GetKeyFor(method, arguments);

            // Assert
            Assert.AreEqual("{0}.{1}({2})".EasyFormat(appServiceType.Name, method.Name, arguments.Single()), key);
        }


        [TestMethod]
        public void Validate_PassesCallToDomainServices()
        {
            // Arrange
            var entity = Mock.Of<IEntity>();

            // Act
            TestObject.Validate(entity);

            // Assert
            DomainServicesMock.Verify(x => x.Validate(entity));
        }
    }
}
