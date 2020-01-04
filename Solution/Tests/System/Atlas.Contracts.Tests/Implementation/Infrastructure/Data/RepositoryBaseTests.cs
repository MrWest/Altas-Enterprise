using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.Data
{
    [TestClass, ExcludeFromCodeCoverage]
    public class RepositoryBaseTests : MockedTestBase<RepositoryBase<IEntity, IDatabaseContext>>
    {
        private IDatabaseContext _databaseContext;
        private Mock<IDatabaseContext> _databaseContextMock;


        [TestInitialize]
        public override void Initialize()
        {
            _databaseContextMock = new Mock<IDatabaseContext>();
            _databaseContext = _databaseContextMock.Object;

            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IEntity>()).Returns(() =>
            {
                var mock = new Mock<IEntity>();
                mock.SetupAllProperties();
                return mock.Object;
            });
        }


        protected override void CreateMock()
        {
            TestMock = new Mock<RepositoryBase<IEntity, IDatabaseContext>>(_databaseContext) { CallBase = true };
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_GivenNullDatabaseContext_ThrowsException()
        {
            try
            {
                RepositoryBase<IEntity, IDatabaseContext> r = new Mock<RepositoryBase<IEntity, IDatabaseContext>>(null).Object;
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }


        [TestMethod]
        public void Entities_DoesNotReturnsTheObjectsFromTheDatabase()
        {
            // Arrange
            IEntity[] entities = { Mock.Of<IEntity>(), Mock.Of<IEntity>() };
            _databaseContextMock.Setup(x => x.GetAll<IEntity>()).Returns(entities.AsQueryable());

            // Act
            IEntity[] actualEntities = TestObject.Entities.ToArray();

            // Assert
            Assert.IsTrue(actualEntities.All(a => entities.All(e => a != e)));
        }

        [TestMethod]
        public void Entities_ReturnsCopiesWithTheSameDataOfTheObjectsFromTheDatabase()
        {
            // Arrange
            IEntity[] entities = { Mock.Of<IEntity>(x => x.Id == 3.ToString()), Mock.Of<IEntity>(x => x.Id == 4.ToString()) };
            _databaseContextMock.Setup(x => x.GetAll<IEntity>()).Returns(entities.AsQueryable());

            // Act
            IEntity[] actualEntities = TestObject.Entities.ToArray();

            // Assert
            CollectionAssert.AreEqual(new[] { 3, 4 }, actualEntities.Select(x => x.Id).ToArray());
        }

        [TestMethod]
        public void Entities_AlwaysReturnsAnArray()
        {
            // Arrange
            IEntity[] entities = { Mock.Of<IEntity>(), Mock.Of<IEntity>() };
            _databaseContextMock.Setup(x => x.GetAll<IEntity>()).Returns(entities.AsQueryable());

            // Act
            IEnumerable<IEntity> actualEntities = TestObject.Entities;

            // Assert
            Assert.IsInstanceOfType(actualEntities, typeof(Array));
        }

        [TestMethod]
        public void Entities_IsInterfaceOverload_ReturnsImplicitiMethodImplementation()
        {
            // Arrange
            IEntity[] entities = { Mock.Of<IEntity>(), Mock.Of<IEntity>() };
            TestMock.SetupGet(x => x.Entities).Returns(entities);
            IRepository testObject = TestObject;

            // Act
            IEnumerable<IEntity> actualEntities = testObject.Entities;

            // Assert
            CollectionAssert.AreEquivalent(entities, actualEntities.ToArray());
        }

        [TestMethod]
        public void Entities_IsInterfaceOverload_ReturnsArray()
        {
            // Arrange
            IEntity[] entities = { Mock.Of<IEntity>(), Mock.Of<IEntity>() };
            TestMock.SetupGet(x => x.Entities).Returns(entities);
            IRepository testObject = TestObject;

            // Act
            IEnumerable<IEntity> actualEntities = testObject.Entities;

            // Assert
            Assert.IsInstanceOfType(actualEntities, typeof(Array));
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Add_GivenNullItem_ThrowsException()
        {
            TestObject.Add(null);
        }

        [TestMethod]
        public void Add_GivenItem_DoesNotAddItToTheDatabaseContext()
        {
            // Arrange
            var entity = Mock.Of<IEntity>();

            // Act
            TestObject.Add(entity);

            // Assert
            _databaseContextMock.Verify(x => x.Add(entity), Times.Never);
        }

        [TestMethod]
        public void Add_GivenItem_ReturnsTheCopyOfIt()
        {
            // Arrange
            IEntity entity = Mock.Of<IEntity>(x => x.Id == 3.ToString()), copy = Mock.Of<IEntity>();
            Mock<IEntity> copyMock = Mock.Get(copy);
            copyMock.SetupAllProperties();
            ServiceLocatorMock.Setup(x => x.GetInstance<IEntity>()).Returns(copy);

            // Act
            IEntity addedEntity = TestObject.Add(entity);

            // Assert
            Assert.AreSame(copy, addedEntity);
            Assert.AreNotSame(entity, addedEntity);
        }

        [TestMethod]
        public void Add_GivenItem_AddsACopyOfItWithTheSameDataToTheDatabaseContext()
        {
            // Arrange
            IEntity entity = Mock.Of<IEntity>(x => x.Id == 3.ToString()), copy = Mock.Of<IEntity>();
            Mock<IEntity> copyMock = Mock.Get(copy);
            copyMock.SetupAllProperties();
            ServiceLocatorMock.Setup(x => x.GetInstance<IEntity>()).Returns(copy);

            // Act
            TestObject.Add(entity);

            // Assert
            Assert.AreEqual(3, copy.Id);
        }

        [TestMethod]
        public void Add_GivenItem_TheItemGetsTheIdFromTheAddedCloneOfIts()
        {
            // Arrange
            IEntity entity = Mock.Of<IEntity>(), copy = Mock.Of<IEntity>();
            Mock<IEntity> copyMock = Mock.Get(copy);
            Mock<IEntity> entityMock = Mock.Get(entity);
            entityMock.SetupAllProperties();
            copyMock.SetupAllProperties();
            ServiceLocatorMock.Setup(x => x.GetInstance<IEntity>()).Returns(copy);
            _databaseContextMock.Setup(x => x.Add(copy)).Callback<IEntity>(e => e.Id = "A");

            // Act
            TestObject.Add(entity);

            // Assert
            Assert.AreEqual("A", entity.Id);
            entityMock.VerifySet(x => x.Id = "A");
        }

        [TestMethod]
        public void Add_IsInterfaceOverLoad_CallsImplicitMethodImplementation()
        {
            // Arrange
            var entity = new Mock<IEntity>();
            IRepository testObject = TestObject;

            // Act
            testObject.Add(entity.Object);

            // Assert
            TestMock.Verify(x => x.Add(entity.Object));
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Delete_GivenNullItem_ThrowsException()
        {
            TestObject.Delete(null);
        }

        [TestMethod]
        public void Delete_GivenItem_DoesNotDeletesItFromTheDatabaseContext()
        {
            // Arrange
            IEntity entity = Mock.Of<IEntity>(), dbEntity = Mock.Of<IEntity>();
            TestMock.Setup(x => x.Find(entity)).Returns(dbEntity);

            // Act
            TestObject.Delete(entity);

            // Assert
            _databaseContextMock.Verify(x => x.Delete(entity), Times.Never);
        }

        [TestMethod]
        public void Delete_GivenItem_DeletesTheDatabaseObjectRepresentingItFromTheDatabaseContext()
        {
            // Arrange
            IEntity entity = Mock.Of<IEntity>(), dbEntity = Mock.Of<IEntity>();
            TestMock.Setup(x => x.Find(entity.Id)).Returns(dbEntity);

            // Act
            TestObject.Delete(entity);

            // Assert
            _databaseContextMock.Verify(x => x.Delete(dbEntity));
        }

        [TestMethod]
        public void Delete_IsInterfaceOverLoad_CallsImplicitMethodImplementation()
        {
            // Arrange
            var entity = new Mock<IEntity>();
            IRepository testObject = TestObject;

            // Act
            testObject.Delete(entity.Object);

            // Assert
            TestMock.Verify(x => x.Delete(entity.Object));
        }


        [TestMethod]
        public void DeleteAll_DeletesAllTheEntitiesFromTheDatabaseContext()
        {
            // Arrange
            IEntity entity1 = Mock.Of<IEntity>(), entity2 = Mock.Of<IEntity>();
            TestMock.SetupGet(x => x.Entities).Returns(new[] { entity1, entity2 });

            // Act
            TestObject.DeleteAll();

            // Assert
            TestMock.Verify(x => x.Delete(entity1));
            TestMock.Verify(x => x.Delete(entity2));
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Update_GivenNullItem_ThrowsException()
        {
            TestObject.Update(null);
        }

        [TestMethod]
        public void Update_GivenItem_PassesCallToDatabaseContext()
        {
            // Arrange
            var entity = new Mock<IEntity>();
            entity.SetupAllProperties();
            entity.Object.Id = 1.ToString();
            var dbEntity = new Mock<IEntity>();
            dbEntity.SetupAllProperties();
            dbEntity.Object.Id = 90.ToString();
            _databaseContextMock.Setup(x => x.Find<IEntity>(entity.Object.Id)).Returns(dbEntity.Object);

            // Act
            TestObject.Update(entity.Object);

            // Assert
            _databaseContextMock.Verify(x => x.Update(dbEntity.Object));
            Assert.AreEqual(1, dbEntity.Object.Id);
        }

        [TestMethod]
        public void Update_IsInterfaceOverLoad_CallsImplicitMethodImplementation()
        {
            // Arrange
            var entity = new Mock<IEntity>();
            IRepository testObject = TestObject;

            // Act
            testObject.Update(entity.Object);

            // Assert
            TestMock.Verify(x => x.Update(entity.Object));
        }


        [TestMethod]
        public void Find_GivenKey_DoesNotReturnsTheObjectFromTheDatabaseContext()
        {
            // Arrange
            var entity = Mock.Of<IEntity>();
            _databaseContextMock.Setup(x => x.Find<IEntity>(90)).Returns(entity);

            // Arrange
            IEntity dbEntity = TestObject.Find(90);

            // Assert
            Assert.AreNotSame(entity, dbEntity);
        }

        [TestMethod]
        public void Find_GivenKey_ReturnsACopyWithTheSameDataOfTheObjectFromTheDatabaseContext()
        {
            // Arrange
            var entity = Mock.Of<IEntity>(x => x.Id == 90.ToString());
            _databaseContextMock.Setup(x => x.Find<IEntity>(90)).Returns(entity);

            // Arrange
            IEntity actualEntity = TestObject.Find(90);

            // Assert
            Assert.AreEqual(entity.Id, actualEntity.Id);
        }

        [TestMethod]
        public void Find_IsInterfaceOverLoad_CallsImplicitMethodImplementation()
        {
            // Arrange
            var entity = new Mock<IEntity>();
            IRepository testObject = TestObject;

            // Act
            testObject.Find(entity.Object);

            // Assert
            TestMock.Verify(x => x.Find(entity.Object));
        }


        [TestMethod]
        public void Where_GivenSpecification_DoesNotReturnsTheObjectsFromTheDatabaseContext()
        {
            // Arrange
            var entity = Mock.Of<IEntity>();
            var specification = Mock.Of<ISpecification<IEntity>>();
            _databaseContextMock.Setup(x => x.Where(specification)).Returns(new[] { entity });

            // Arrange
            IEnumerable<IEntity> entities = TestObject.Where(specification);

            // Assert
            Assert.IsTrue(new[] { entity }.All(e => entities.All(e1 => e != e1)));
        }

        [TestMethod]
        public void Where_GivenSpecification_ReturnsCopiesWithTheSameDataOfTheObjectsFromTheDatabaseContext()
        {
            // Arrange
            var entity1 = Mock.Of<IEntity>(x => x.Id == 90.ToString());
            var entity2 = Mock.Of<IEntity>(x => x.Id == 100.ToString());
            var specification = Mock.Of<ISpecification<IEntity>>();
            _databaseContextMock.Setup(x => x.Where(specification)).Returns(new[] { entity1, entity2 });

            // Arrange
            IEnumerable<IEntity> entities = TestObject.Where(specification);

            // Assert
            CollectionAssert.AreEqual(new[] { entity1.Id, entity2.Id }, entities.Select(x => x.Id).ToArray());
        }

        [TestMethod]
        public void Where_GivenSpecification_AlwaysReturnsArray()
        {
            // Arrange
            var entity = Mock.Of<IEntity>();
            var specification = Mock.Of<ISpecification<IEntity>>();
            _databaseContextMock.Setup(x => x.Where(specification)).Returns(new List<IEntity> { entity });

            // Arrange
            IEnumerable<IEntity> entities = TestObject.Where(specification);

            // Assert
            Assert.IsInstanceOfType(entities, typeof(Array));
        }

        [TestMethod]
        public void Where_IsInterfaceOverLoad_CallsImplicitMethodImplementation()
        {
            // Arrange
            Expression<Predicate<IEntity>> predicate = x => ((string)x.Id).Contains("A");
            var specification = Mock.Of<ISpecification<IEntity>>(x => x.Predicate == predicate);

            IEntity[] entities =
            {
                Mock.Of<IEntity>(x => (string)x.Id == "A"),
                Mock.Of<IEntity>(x => (string)x.Id == "AA"),
                Mock.Of<IEntity>(x => (string)x.Id == "C")
            };
            _databaseContextMock
                .Setup(x => x.Where(It.IsAny<ISpecification<IEntity>>()))
                .Returns<ISpecification<IEntity>>(spec => entities.Where(x => predicate.Compile()(x)));
            IRepository testObject = TestObject;

            // Act
            IEnumerable<IEntity> actualEntities = testObject.Where(specification);

            // Assert
            CollectionAssert.AreEquivalent(new[] { entities[0].Id, entities[1].Id }, actualEntities.Select(x => x.Id).ToArray());
        }

        [TestMethod]
        public void Where_IsInterfaceOverLoad_ReturnsArray()
        {
            // Arrange
            TestMock.Setup(x => x.Where(It.IsAny<ISpecification<IEntity>>())).Returns(new List<IEntity>());
            IRepository testObject = TestObject;

            // Act
            Expression<Predicate<IEntity>> predicate = x => true;
            var specification = Mock.Of<ISpecification<IEntity>>(x => x.Predicate == predicate);
            IEnumerable<IEntity> actualEntities = testObject.Where(specification);

            // Assert
            Assert.IsInstanceOfType(actualEntities, typeof(Array));
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Merge_GivenNullFormerItems_Throws()
        {
            TestObject.Merge(null, new IEntity[0]);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Merge_GivenNullCurrentItems_Throws()
        {
            TestObject.Merge(new IEntity[0], null);
        }

        [TestMethod]
        public void Merge_GivenRightArguments_PassesCallToDatabaseContext()
        {
            // Arrange
            var formerItems = new IEntity[0];
            var currentItems = new IEntity[0];

            // Act
            TestObject.Merge(formerItems, currentItems);

            // Assert
            _databaseContextMock.Verify(x => x.Merge(formerItems, currentItems, It.IsAny<Action<IEntity>>(), TestObject.Update, TestObject.Delete));
        }

        [TestMethod]
        public void Merge_IsInterfaceOverLoad_CallsImplicitMethodImplementation()
        {
            // Arrange
            IRepository testObject = TestObject;

            // Act
            IEntity[] former = { Mock.Of<IEntity>() }, actual = { Mock.Of<IEntity>() };
            testObject.Merge(former, actual);

            // Assert
            TestMock.Verify(x => x.Merge(former, actual));
        }


        [TestMethod]
        public void GetSorted_DoesNotReturnObjectsFromTheDatabaseContext()
        {
            // Arrange
            IEntity[] entities = { Mock.Of<IEntity>(), Mock.Of<IEntity>() };
            Comparison<IEntity> comparison = (x, y) => 0;
            _databaseContextMock.Setup(x => x.GetSorted(comparison)).Returns(entities);

            // Act
            IEnumerable<IEntity> actualEntities = TestObject.GetSorted(comparison);

            // Assert
            Assert.IsTrue(entities.All(e => actualEntities.All(e1 => e != e1)));
        }

        [TestMethod]
        public void GetSorted_ReturnsCopiesWithTheSameDataOfTheObjectsFromTheDatabaseContext()
        {
            // Arrange
            IEntity[] entities = { Mock.Of<IEntity>(x => x.Id == 2.ToString()), Mock.Of<IEntity>(x => x.Id == 3.ToString()) };
            Comparison<IEntity> comparison = (x, y) => 0;
            _databaseContextMock.Setup(x => x.GetSorted(comparison)).Returns(entities);

            // Act
            IEnumerable<IEntity> actualEntities = TestObject.GetSorted(comparison);

            // Assert
            CollectionAssert.AreEqual(new[] { 2, 3 }, actualEntities.Select(x => x.Id).ToArray());
        }

        [TestMethod]
        public void GetSorted_ReturnsAnArray()
        {
            // Arrange
            IEntity[] entities = { Mock.Of<IEntity>(), Mock.Of<IEntity>() };
            Comparison<IEntity> comparison = (x, y) => 0;
            _databaseContextMock.Setup(x => x.GetSorted(comparison)).Returns(entities);

            // Act
            IEnumerable<IEntity> actualEntities = TestObject.GetSorted(comparison);

            // Assert
            Assert.IsInstanceOfType(actualEntities, typeof(Array));
        }

        [TestMethod]
        public void GetSorted_IsInterfaceOverLoad_CallsImplicitMethodImplementation()
        {
            // Arrange
            IEntity[] entities = { Mock.Of<IEntity>(x => x.Id == 2.ToString()), Mock.Of<IEntity>(x => x.Id == 1.ToString()) };
            Comparison<IEntity> comparison = (entity, entity1) => (Convert.ToInt32(entity.Id )- (Convert.ToInt32(entity1.Id)));

            TestMock
                .Setup(x => x.GetSorted(It.IsAny<Comparison<IEntity>>()))
                .Returns<Comparison<IEntity>>(comp => new List<IEntity> { entities[1], entities[0] });
            IRepository testObject = TestObject;

            // Act
            IEnumerable<IEntity> actualEntities = testObject.GetSorted(comparison);

            // Assert
            CollectionAssert.AreEquivalent(new[] { entities[1], entities[0] }, actualEntities.ToArray());
        }

        [TestMethod]
        public void GetSorted_IsInterfaceOverLoad_ReturnsArray()
        {
            // Arrange
            TestMock.Setup(x => x.GetSorted(It.IsAny<Comparison<IEntity>>())).Returns(new List<IEntity>());
            IRepository testObject = TestObject;


            // Act
            IEnumerable<IEntity> actualEntities = testObject.GetSorted((x, y) => 0);

            // Assert
            Assert.IsInstanceOfType(actualEntities, typeof(Array));
        }


        [TestMethod]
        public void RelevantProperties_ReturnsCorrectProperties()
        {
            var properties = (IEnumerable<string>)TestObjectInternals.GetProperty("RelevantProperties");
            CollectionAssert.AreEquivalent(new[] { "Id" }, properties.ToArray());
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void FindById_GivenNullArray_Throws()
        {
            TestObject.FindById(null);
        }

        [TestMethod]
        public void FindById_GivenIdsArray_ReturnsCorrectQueryResultFromDatabaseContext()
        {
            // Arrange
            IEntity[] expectedResult = { Mock.Of<IEntity>(x => x.Id == 1.ToString()), Mock.Of<IEntity>(x => x.Id == 2.ToString()) };
            _databaseContextMock.Setup(x => x.Where(It.IsAny<EntitiesByIdSpecification<IEntity>>())).Returns<ISpecification<IEntity>>(specification =>
            {
                Predicate<IEntity> predicate = specification.Predicate.Compile();
                return expectedResult.Where(x => predicate(x));
            });

            // Act
            IEnumerable<IEntity> actualResult = TestObject.FindById(1, 2);

            // Assert
            CollectionAssert.AreEquivalent(expectedResult, actualResult.ToArray());
        }

        [TestMethod]
        public void FindById_IsInterfaceOverLoad_CallsImplicitMethodImplementation()
        {
            // Arrange
            IRepository testObject = TestObject;

            // Act
            testObject.FindById(1, 2);

            // Assert
            TestMock.Verify(x => x.FindById(1, 2));
        }

        [TestMethod]
        public void FindById_IsInterfaceOverLoad_ReturnsArray()
        {
            // Arrange
            TestMock.Setup(x => x.FindById(1)).Returns(new List<IEntity>());
            IRepository testObject = TestObject;

            // Act
            IEnumerable<IEntity> actualEntities = testObject.FindById(1, 2);

            // Assert
            Assert.IsInstanceOfType(actualEntities, typeof(Array));
        }
    }
}