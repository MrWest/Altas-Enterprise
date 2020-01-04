using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget;
using CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Entities;
using Db4objects.Db4o;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Infrastructure.Data.Db4O.Budget
{
    [TestClass, ExcludeFromCodeCoverage]
    public class BudgetComponentItemRepositoryBaseTests : TestBase
    {
        private readonly string _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdb.db");


        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IPlannedResource>()).Returns(new PlannedResourceStub());
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_dbPath))
                File.Delete(_dbPath);
        }


        [TestMethod]
        public void Entities_AlwaysReturnsArray()
        {
            // Arrange
            var db = new Mock<IDb4ODatabaseContext>();
            db.Setup(x => x.Query(It.IsAny<Predicate<IBudgetComponentItem>>()))
                .Returns(new List<IBudgetComponentItem>());

            var repository = new BudgetComponentItemRepositoryStub(db.Object) { Component = Mock.Of<IBudgetComponent>() };

            // Act
            IEnumerable<IBudgetComponentItem> items = repository.Entities;

            // Assert
            Assert.IsInstanceOfType(items, typeof(Array));
        }

        [TestMethod]
        public void Entities_ReturnsComponentItems()
        {
            #region Arrange

            var budgetComponent1 = new BudgetComponentStub
            {
                Id = Guid.NewGuid()
            };
            var budgetComponent2 = new BudgetComponentStub
            {
                Id = Guid.NewGuid()
            };

            var item1 = new PlannedResourceStub
            {
                Id = Guid.NewGuid(),
                Component = budgetComponent1
            };
            budgetComponent1.PlannedResources.Add(item1);

            var item2 = new PlannedResourceStub
            {
                Component = budgetComponent2
            };
            budgetComponent2.PlannedResources.Add(item2);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent1);
                db.Store(budgetComponent2);
                db.Store(item1);
                db.Store(item2);
                db.Commit();
            }

            #endregion


            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new BudgetComponentItemRepositoryStub(db) { Component = budgetComponent1 };

                // Act
                IEnumerable<IBudgetComponentItem> items = repository.Entities.ToArray();

                // Assert
                Assert.AreEqual(1, items.Count());
                Assert.AreEqual(items.Single().Id, item1.Id);
            }
        }

        [TestMethod]
        public void Entities_ReturnsCloneOfComponentItems()
        {
            #region Arrange

            var budgetComponent1 = new BudgetComponentStub
            {
                Id = Guid.NewGuid()
            };
            var budgetComponent2 = new BudgetComponentStub
            {
                Id = Guid.NewGuid()
            };

            var item1 = new PlannedResourceStub
            {
                Id = Guid.NewGuid(),
                Component = budgetComponent1
            };
            budgetComponent1.PlannedResources.Add(item1);

            var item2 = new PlannedResourceStub
            {
                Id = Guid.NewGuid(),
                Component = budgetComponent2
            };
            budgetComponent2.PlannedResources.Add(item2);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent1);
                db.Store(budgetComponent2);
                db.Store(item1);
                db.Store(item2);
                db.Commit();
            }

            #endregion


            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new BudgetComponentItemRepositoryStub(db) { Component = budgetComponent1 };

                // Act
                IEnumerable<IBudgetComponentItem> items = repository.Entities;

                // Assert
                Assert.AreNotSame(items.Single(), db.Query<IBudgetComponentItem>().Single(x => Equals(x.Id, item1.Id)));
            }
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Add_GivenNullBudgetComponentItem_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new BudgetComponentItemRepositoryStub(db);

            // Act
            repository.Add(null);
        }

        [TestMethod]
        public void Add_GivenBudgetComponentItem_AddsItWithItsDataCorrectly()
        {
            // Arrange
            var budgetComponent = new BudgetComponentStub
            {
                Id = Guid.NewGuid()
            };

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new Mock<BudgetComponentItemRepositoryStub>(db) { CallBase = true };
                repository.Object.Component = budgetComponent;

                var newItem = new PlannedResourceStub
                {
                    Code = "C",
                    Description = "D",
                    Name = "N",
                    Quantity = 100
                };

                repository.Object.Add(newItem);
                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                IBudgetComponentItem item = db.Query<IBudgetComponentItem>().Single();

                Assert.AreNotEqual(default(Guid), item.Id);
                Assert.AreEqual("C", item.Code);
                Assert.AreEqual("D", item.Description);
                Assert.AreEqual("N", item.Name);
                Assert.AreEqual(100, item.Quantity);
            }
        }

        [TestMethod]
        public void Add_GivenBudgetComponentItem_AddsItWithItsReferencesCorrectlySet()
        {
            // Arrange
            var budgetComponent = new BudgetComponentStub
            {
                Id = Guid.NewGuid()
            };

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Commit();
            }

            // Act
            IPlannedResource newItem;
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new BudgetComponentItemRepositoryStub(db);
                repository.Component = budgetComponent;

                newItem = new PlannedResourceStub();

                repository.Add(newItem);
                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                Assert.AreSame(budgetComponent, newItem.Component);
                CollectionAssert.Contains(budgetComponent.PlannedResources.ToArray(), newItem);

                IBudgetComponent component = db.Query<IBudgetComponent>().Single();
                IBudgetComponentItem item = db.Query<IBudgetComponentItem>().Single();

                Assert.AreSame(component, item.Component);
                CollectionAssert.Contains(component.PlannedResources.ToArray(), item);
            }
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Update_GivenNullBudgetComponentItem_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new BudgetComponentItemRepositoryStub(db);

            // Act
            repository.Update(null);
        }

        [TestMethod]
        public void Update_GivenBudgetComponentItem_UpdatesItWithItsDataCorrectly()
        {
            // Arrange
            var budgetComponent = new BudgetComponentStub
            {
                Id = Guid.NewGuid()
            };
            var item = new PlannedResourceStub
            {
                Code = "C",
                Description = "D",
                Name = "N",
                Quantity = 100,
                Id = Guid.NewGuid(),
                Component = budgetComponent
            };
            budgetComponent.PlannedResources.Add(item);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Store(item);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new Mock<BudgetComponentItemRepositoryStub>(db) { CallBase = true };
                repository.Object.Component = budgetComponent;

                item.Code = "CC";
                item.Description = "DD";
                item.Name = "NN";
                item.Quantity = 1000;

                repository.Object.Update(item);
                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                IBudgetComponentItem dbItem = db.Query<IBudgetComponentItem>().Single();

                Assert.AreNotEqual(default(Guid), item.Id);
                Assert.AreEqual("CC", dbItem.Code);
                Assert.AreEqual("DD", dbItem.Description);
                Assert.AreEqual("NN", dbItem.Name);
                Assert.AreEqual(1000, dbItem.Quantity);
            }
        }

        [TestMethod]
        public void Update_GivenBudgetComponentItem_UpdatesItWithItsReferencesCorrectlySet()
        {
            // Arrange
            var budgetComponent = new BudgetComponentStub
            {
                Id = Guid.NewGuid()
            };
            var item = new PlannedResourceStub
            {
                Code = "C",
                Description = "D",
                Name = "N",
                Quantity = 100,
                Id = Guid.NewGuid(),
            };

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Store(item);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new Mock<BudgetComponentItemRepositoryStub>(db) { CallBase = true };
                repository.Object.Component = budgetComponent;

                // Break intentionally the relationships, the repository must restore them
                item.Component = null;
                budgetComponent.PlannedResources.Remove(item);

                repository.Object.Update(item);

                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                Assert.AreSame(budgetComponent, item.Component);
                CollectionAssert.Contains(budgetComponent.PlannedResources.ToArray(), item);

                IBudgetComponent component = db.Query<IBudgetComponent>().Single();
                IBudgetComponentItem dbItem = db.Query<IBudgetComponentItem>().Single();

                Assert.AreSame(component, dbItem.Component);
                CollectionAssert.Contains(component.PlannedResources.ToArray(), dbItem);
            }
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Delete_GivenNullBudgetComponentItem_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new Mock<BudgetComponentItemRepositoryStub>(db) { CallBase = true };

            // Act
            repository.Object.Delete(null);
        }

        [TestMethod]
        public void Delete_GivenBudgetComponentItem_DeletesIt()
        {
            // Arrange
            var budgetComponent = new BudgetComponentStub
            {
                Id = Guid.NewGuid()
            };
            var item = new PlannedResourceStub
            {
                Code = "C",
                Description = "D",
                Name = "N",
                Quantity = 100,
                Id = Guid.NewGuid(),
                Component = budgetComponent
            };
            budgetComponent.PlannedResources.Add(item);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Store(item);
                db.Commit();
            }


            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new Mock<BudgetComponentItemRepositoryStub>(db) { CallBase = true };
                repository.Object.Component = budgetComponent;

                repository.Object.Delete(item);
                db.Commit();
            }


            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                IBudgetComponent component = db.Query<IBudgetComponent>().Single();
                Assert.IsFalse(db.Query<IBudgetComponentItem>().Any());
                Assert.IsFalse(component.PlannedResources.Any());
            }
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Relate_GivenNullPlannedResource_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new BudgetComponentItemRepositoryStub(db);

            // Act
            repository.Relate(null, Mock.Of<IBudgetComponent>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Relate_GivenNullBudgetComponent_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new BudgetComponentItemRepositoryStub(db);

            // Act
            repository.Relate(Mock.Of<IPlannedResource>(), null);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Unrelate_GivenNullPlannedResource_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new BudgetComponentItemRepositoryStub(db);

            // Act
            repository.Unrelate(null, Mock.Of<IBudgetComponent>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Unrelate_GivenNullBudgetComponent_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new BudgetComponentItemRepositoryStub(db);

            // Act
            repository.Unrelate(Mock.Of<IPlannedResource>(), null);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void SaveReference_GivenNullPlannedResource_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new BudgetComponentItemRepositoryStub(db);

            // Act
            repository.SaveReference((IPlannedResource)null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void SaveReference_GivenNullBudgetComponent_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new BudgetComponentItemRepositoryStub(db);

            // Act
            repository.SaveReference((IBudgetComponent)null);
        }


        [TestMethod]
        public void FilterByName_GivenNameSpecification_ReturnsTheItemsOfTheSpecifiedBudgetComponentAndMatchingTheNameSpecification()
        {
            #region Arrange

            IBudgetComponent component1 = new BudgetComponentStub { Id = Guid.NewGuid() };
            IBudgetComponent component2 = new BudgetComponentStub { Id = Guid.NewGuid() };

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                var item1 = new PlannedResourceStub
                {
                    Id = Guid.NewGuid(),
                    Component = component1,
                    Name = "item 1"
                };
                var item2 = new PlannedResourceStub
                {
                    Id = Guid.NewGuid(),
                    Component = component1,
                    Name = "item 2"
                };
                component1.PlannedResources.Add(item1);
                component1.PlannedResources.Add(item2);

                var item3 = new PlannedResourceStub
                {
                    Id = Guid.NewGuid(),
                    Component = component2,
                    Name = "item 1"
                };
                var item4 = new PlannedResourceStub
                {
                    Id = Guid.NewGuid(),
                    Component = component2,
                    Name = "item 2"
                };
                component2.PlannedResources.Add(item3);
                component2.PlannedResources.Add(item4);

                db.Store(component1);
                db.Store(component2);
                db.Store(item1);
                db.Store(item2);
                db.Store(item3);
                db.Store(item4);
                db.Commit();
            }

            #endregion

            #region Act

            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new BudgetComponentItemRepositoryStub(db) { Component = component1 };

                var dbComponent1 = db.Query<IBudgetComponent>().First();

                IPlannedResource[] actualResources = repository.FilterByName("em 1").ToArray();

                Assert.AreEqual(1, actualResources.Length);
                Assert.IsTrue(actualResources.All(x => x.Name.Contains("em 1")));
                Assert.IsTrue(actualResources.All(x => x.Component == dbComponent1));
            }

            #endregion
        }


        [TestMethod]
        public void FilterByName_ReturnsArray()
        {
            var db = new Mock<IDb4ODatabaseContext>();
            var repository = new BudgetComponentItemRepositoryStub(db.Object) { Component = Mock.Of<IBudgetComponent>() };

            // Act
            IEnumerable<IPlannedResource> resources = repository.FilterByName("em 1");

            // Assert
            Assert.IsInstanceOfType(resources, typeof(Array));
        }


        public class BudgetComponentItemRepositoryStub : BudgetComponentItemRepositoryBase<IPlannedResource, IBudgetComponent>
        {
            public BudgetComponentItemRepositoryStub(IDb4ODatabaseContext databaseContext)
                : base(databaseContext)
            {
            }


            protected override Func<IBudgetComponent, IList<IPlannedResource>> GetItemCollection
            {
                get { return x => x.PlannedResources; }
            }
        }
    }
}
