using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using Db4objects.Db4o;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.Data.Db4O
{
    [TestClass, ExcludeFromCodeCoverage]
    public class Db4ODatabaseContextTests : TestBase<Db4ODatabaseContext>
    {
        private readonly string _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdb.adb");


        public override void Initialize()
        {
        }

        [TestInitialize]
        [TestCleanup]
        public void Cleanup()
        {
            if (TestObject != null)
                TestObject.Dispose();

            File.Delete(_dbPath);
        }

        /// <summary>
        /// Creates and assigns to the TestObject property a new instance of type <see cref="Db4ODatabaseContext"/>.
        /// </summary>
        /// <returns>A new instance of type <see cref="Db4ODatabaseContext"/>.</returns>
        protected override void CreateInstance()
        {
            TestObject = new Db4ODatabaseContext(_dbPath);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_GivenNullPath_ThrowsException()
        {
            new Db4ODatabaseContext(null);
        }


        [TestMethod]
        public void GetAll_ReturnsAllInstancesOfACertainType()
        {
            var entity1 = new Entity { Name = "E1" };
            var entity2 = new Entity { Name = "E2" };

            #region Arrange

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(entity1);
                db.Store(entity2);
                db.Commit();
            }

            #endregion

            // Act
            CreateInstance();
            Entity[] actualEntities = TestObject.GetAll<Entity>().ToArray();

            // Assert
            CollectionAssert.AreEquivalent(new[] { "E1", "E2" }, actualEntities.Select(x => x.Name).ToArray());
        }


        [TestMethod]
        public void GenerateKey_ReturnsFreshKey()
        {
            // Arrange
            CreateInstance();

            // Act
            object key = TestObject.GenerateKey();

            // Assert
            Assert.IsInstanceOfType(key, typeof(Guid));
            Assert.AreNotEqual(default(Guid).ToString(), key);
        }


        [TestMethod]
        public void Add_AddsTheEntityToTheDatabase()
        {
            var entity1 = new Entity { Name = "E1" };
            var entity2 = new Entity { Name = "E2" };

            #region Arrange

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(entity1);
                db.Store(entity2);
                db.Commit();
            }

            #endregion

            CreateInstance();
            using (TestObject)
            {
                // Act
                TestObject.Add(new Entity { Name = "E3" });
                TestObject.Save();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
                CollectionAssert.AreEquivalent(new[] { "E1", "E2", "E3" }, db.Query<Entity>().Select(x => x.Name).ToArray());
        }

        [TestMethod]
        public void Add_CreatesANewIdForTheNewEntityIfItHasNone()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var databaseContext = new Mock<Db4ODatabaseContext>() { CallBase = true };
            databaseContext.Setup(x => x.GenerateKey()).Returns(id);

            using (databaseContext.Object)
            {
                // Act
                var entity = new Entity { Name = "E3" };
                databaseContext.Object.Add(entity);

                // Assert
                Assert.AreEqual(id, entity.Id);
            }
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Add_GivenNullEntity_ThrowsException()
        {
            CreateInstance();
            TestObject.Add<Entity>(null);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Delete_GivenNullEntity_ThrowsException()
        {
            CreateInstance();
            TestObject.Delete<Entity>(null);
        }

        [TestMethod]
        public void Delete_DeletesTheEntityFromTheDatabase()
        {
            var entity1 = new Entity { Id = 1.ToString(), Name = "E1" };
            var entity2 = new Entity { Id = 2.ToString(), Name = "E2" };

            #region Arrange

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(entity1);
                db.Store(entity2);
                db.Commit();
            }

            #endregion

            CreateInstance();
            using (TestObject)
            {
                // Act
                TestObject.Delete(entity2);
                TestObject.Save();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
                CollectionAssert.AreEquivalent(new[] { "E1" }, db.Query<Entity>().Select(x => x.Name).ToArray());
        }


        [TestMethod]
        public void Rollback_RollsbackTheOperations()
        {
            var entity1 = new Entity { Id = 1.ToString(), Name = "E1" };
            var entity2 = new Entity { Id = 2.ToString(), Name = "E2" };

            #region Arrange

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(entity1);
                db.Store(entity2);
                db.Commit();
            }

            #endregion

            CreateInstance();
            using (TestObject)
            {
                // Act
                var entityToDelete = TestObject.GetAll<Entity>().Single(x => Equals(x.Id, 1));
                TestObject.Delete(entityToDelete);
                TestObject.DropChanges();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
                CollectionAssert.AreEquivalent(new[] { "E1", "E2" }, db.Query<Entity>().Select(x => x.Name).ToArray());
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Update_GiveNullItem_ThrowsException()
        {
            CreateInstance();
            TestObject.Update<Entity>(null);
        }

        [TestMethod]
        public void Update_GivenItem_UpdatesItsChanges()
        {
            // Arrange
            var entity = new Entity { Id = 1.ToString(), Name = "E1" };
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(entity);
                db.Commit();
            }

            // Act
            CreateInstance();
            using (TestObject)
            {
                entity = TestObject.GetAll<Entity>().Single(x => Equals(x.Id, 1));
                entity.Name = "EE1";
                TestObject.Update(entity);
                TestObject.Save();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                Assert.AreEqual(1, db.Query<Entity>().Count);
                Assert.IsNotNull(db.Query<Entity>(x => x.Name == "EE1").SingleOrDefault());
                Assert.IsNull(db.Query<Entity>(x => x.Name == "E1").SingleOrDefault());
            }
        }


        [TestMethod]
        public void Find_GivenIdAndThereIsEntityMatchingIt_ReturnsSuchEntity()
        {
            // Arrange
            Entity entity1 = new Entity { Id = 1.ToString(), Name = "1" }, entity2 = new Entity { Id = 2.ToString(), Name = "2" };
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(entity1);
                db.Store(entity2);
                db.Commit();
            }

            // Act
            CreateInstance();
            using (TestObject)
            {
                var entity = TestObject.Find<Entity>(1);

                // Assert
                Assert.AreEqual(1, entity.Id);
                Assert.AreEqual("1", entity.Name);
            }
        }


        [TestMethod]
        public void Find_GivenSpecification_NoEntityMatchSpecification_ReturnsNull()
        {
            // Arrange
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(new Entity { Name = "A" });
                db.Store(new Entity { Name = "A" });
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var entity = db.Find(new Specification<Entity>(x => x.Name == "B"));

                // Assert
                Assert.IsNull(entity);
            }
        }

        [TestMethod]
        public void Find_GivenSpecification_ASingleEntityMatchesSpecification_ReturnsThatEntity()
        {
            // Arrange
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(new Entity { Name = "A", Id = 2.ToString() });
                db.Store(new Entity { Name = "B", Id = 1.ToString() });
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var entity = db.Find(new Specification<Entity>(x => x.Name == "B"));

                // Assert
                Assert.AreEqual(1, entity.Id);
            }
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Find_GivenSpecification_MoreThanOneEntityMatchSpecification_Throws()
        {
            // Arrange
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(new Entity { Name = "B", Id = 2.ToString() });
                db.Store(new Entity { Name = "B", Id = 1.ToString() });
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var entity = db.Find(new Specification<Entity>(x => x.Name == "B"));

                // Assert
                Assert.AreEqual(1, entity.Id);
            }
        }


        [TestMethod]
        public void Where_GivenPredicate_ReturnsElementsMatchingThatCriteria()
        {
            // Arrange
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(new Entity { Name = "A", Id = 2.ToString() });
                db.Store(new Entity { Name = "B", Id = 1.ToString() });
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                IEnumerable<Entity> entities = db.Where(new Specification<Entity>(x => x.Name == "B")).ToArray();

                // Assert
                Assert.AreEqual(entities.Single().Name, "B");
            }
        }


        [TestMethod]
        public void GetSorted_GivenComparison_ReturnsElementsSortedInTheSoecifiedWay()
        {
            // Arrange
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(new Entity { Name = "A", Id = 2.ToString() });
                db.Store(new Entity { Name = "B", Id = 1.ToString() });
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                IEnumerable<Entity> entities = db.GetSorted<Entity>((x, y) => Convert.ToInt32(x.Id) - Convert.ToInt32(y.Id)).ToArray();

                // Assert
                CollectionAssert.AreEqual(new[] { 1, 2 }, entities.Select(x => x.Id).ToArray());
                CollectionAssert.AreEqual(new[] { "B", "A" }, entities.Select(x => x.Name).ToArray());
            }
        }


        private class Entity : IEntity
        {
            public string Name { get; set; }

            public string Id { get; set; }

            public string FullName { get { return ToString(); } }
        }


        private class Entity2 : IEntity
        {
            public string Name { get; set; }

            public string Id { get; set; }

            public string FullName { get { return ToString(); } }
        }
    }
}
