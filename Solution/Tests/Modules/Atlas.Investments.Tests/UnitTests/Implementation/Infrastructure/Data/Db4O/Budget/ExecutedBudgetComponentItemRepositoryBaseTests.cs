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
    public class ExecutedBudgetComponentItemRepositoryBaseTests : TestBase
    {
        private readonly string _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdb.db");


        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IExecutedResource>()).Returns(() => new ExecutedResourceStub());
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_dbPath))
                File.Delete(_dbPath);
        }


        [TestMethod]
        public void GetItemCollection_ReturnsMethodAllowingToObtainCorrectExecutedResourceListOfCurrentBudgetComponent()
        {
            // Arrange
            var expectedList = Mock.Of<IList<IExecutedResource>>();
            var budgetComponent = Mock.Of<IBudgetComponent>(x => x.ExecutedResources == expectedList);
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new RepositoryStub(db);

            // Act
            var method = repository.GetActualItemCollection();
            IList<IExecutedResource> actualList = method(budgetComponent);

            // Assert
            Assert.AreSame(expectedList, actualList);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Add_GivenNullExecutedResource_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new RepositoryStub(db);

            // Act
            repository.Add(null);
        }

        [TestMethod]
        public void Add_GivenExecutedResourceWithoutPlanification_AddsItPersistingItsOwnData()
        {
            // Arrange
            var budgetComponent = new BudgetComponentStub { Id = Guid.NewGuid() };

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new RepositoryStub(db) { Component = budgetComponent };

                var newExecutedResource = new ExecutedResourceStub
                {
                    Name = "N",
                    Description = "D",
                    Quantity = 1,
                    Code = "C"
                };

                repository.Add(newExecutedResource);
                db.Commit();
            }

            // Assert
            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                IBudgetComponent component = db.Query<IBudgetComponent>().Single();

                IExecutedResource executedResource = db.Query<IExecutedResource>().Single();
                Assert.IsInstanceOfType(executedResource.Id, typeof(Guid));
                Assert.AreNotEqual(default(Guid).ToString(), executedResource.Id.ToString());
                Assert.AreEqual("N", executedResource.Name);
                Assert.AreEqual("D", executedResource.Description);
                Assert.AreEqual(1, executedResource.Quantity);
                Assert.AreEqual("C", executedResource.Code);
                Assert.AreSame(component, executedResource.Component);
                CollectionAssert.Contains(component.ExecutedResources.ToArray(), executedResource);
            }
        }

        [TestMethod]
        public void Add_GivenExecutedResourceWithPlanification_RelatesItToThatPlanification()
        {
            // Arrange
            IBudgetComponent budgetComponent = new BudgetComponentStub { Id = Guid.NewGuid() };
            IPlannedResource plannedResource = new PlannedResourceStub { Id = Guid.NewGuid(), Component = budgetComponent };
            budgetComponent.PlannedResources.Add(plannedResource);

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Commit();
            }

            // Act
            IExecutedResource executedResource;
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new RepositoryStub(db) { Component = budgetComponent };

                executedResource = new ExecutedResourceStub { Planification = plannedResource };

                repository.Add(executedResource);
                db.Commit();
            }

            // Assert
            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                Assert.AreSame(plannedResource, executedResource.Planification);
                Assert.AreSame(executedResource, plannedResource.Execution);

                plannedResource = db.Query<IPlannedResource>().Single();
                executedResource = db.Query<IExecutedResource>().Single();

                Assert.AreSame(plannedResource, executedResource.Planification);
                Assert.AreSame(executedResource, plannedResource.Execution);
            }
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Update_GivenNullExecutedResource_Throws()
        {
            // Arrange
            var repository = new Mock<ExecutedResourceRepositoryBase<IBudgetComponent>>(Mock.Of<IDb4ODatabaseContext>())
            {
                CallBase = true
            };

            // Act
            repository.Object.Update(null);
        }

        [TestMethod]
        public void Update_GivenExecutedResource_UpdatesItsFieldsData()
        {
            // Arrange
            IBudgetComponent budgetComponent = new BudgetComponentStub { Id = Guid.NewGuid() };
            IExecutedResource executedResource = new ExecutedResourceStub
            {
                Code = "C",
                Component = budgetComponent,
                Description = "D",
                Id = Guid.NewGuid(),
                Name = "N",
                Quantity = 10,
            };
            budgetComponent.ExecutedResources.Add(executedResource);

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Store(executedResource);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new RepositoryStub(db) { Component = budgetComponent };

                executedResource.Code = "CC";
                executedResource.Name = "NN";
                executedResource.Description = "DD";
                executedResource.Quantity = 100;

                repository.Update(executedResource);
                db.Commit();
            }

            // Assert
            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                executedResource = db.Query<IExecutedResource>().Single();
                Assert.AreEqual("NN", executedResource.Name);
                Assert.AreEqual("DD", executedResource.Description);
                Assert.AreEqual(100, executedResource.Quantity);
                Assert.AreEqual("CC", executedResource.Code);
            }
        }

        [TestMethod]
        public void Update_GivenExecutedResourceWithNullPlanification_BreaksTheRelationshipWithItsActualOne()
        {
            // Arrange
            IBudgetComponent budgetComponent = new BudgetComponentStub { Id = Guid.NewGuid() };
            IPlannedResource planification = new PlannedResourceStub { Id = Guid.NewGuid() };
            IExecutedResource executedResource = new ExecutedResourceStub
            {
                Id = Guid.NewGuid(),
                Component = budgetComponent,
                Planification = planification
            };
            planification.Execution = executedResource;
            budgetComponent.ExecutedResources.Add(executedResource);

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Store(executedResource);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new RepositoryStub(db) { Component = budgetComponent };

                executedResource.Planification = null;

                repository.Update(executedResource);
                db.Commit();
            }

            // Assert
            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                executedResource = db.Query<IExecutedResource>().Single();
                planification = db.Query<IPlannedResource>().Single();

                Assert.IsNull(executedResource.Planification);
                Assert.IsNull(planification.Execution);
            }
        }

        [TestMethod]
        public void Update_GivenExecutedResourceWithAnotherPlanification_BreaksTheRelationshipWithItsActualOneAndEstablishesARelationshipWithTheCurrentOne()
        {
            // Arrange
            IBudgetComponent budgetComponent = new BudgetComponentStub { Id = Guid.NewGuid() };
            IPlannedResource oldPlanification = new PlannedResourceStub { Id = Guid.NewGuid() };
            IExecutedResource executedResource = new ExecutedResourceStub
            {
                Component = budgetComponent,
                Id = Guid.NewGuid(),
                Planification = oldPlanification
            };
            oldPlanification.Execution = executedResource;
            budgetComponent.ExecutedResources.Add(executedResource);

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Store(oldPlanification);
                db.Store(executedResource);
                db.Commit();
            }

            // Act
            IExecutedResource oldExecution = new ExecutedResourceStub { Id = Guid.NewGuid() };
            IPlannedResource newPlanification = new PlannedResourceStub { Id = Guid.NewGuid(), Execution = oldExecution };
            oldExecution.Planification = newPlanification;
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                db.Store(oldExecution);
                db.Store(newPlanification);
                db.Commit();

                var repository = new RepositoryStub(db) { Component = budgetComponent };

                executedResource.Planification = newPlanification;

                repository.Update(executedResource);
                db.Commit();
            }

            // Assert
            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                executedResource = db.Query<IExecutedResource>().Single(x => Equals(x.Id, executedResource.Id));
                oldExecution = db.Query<IExecutedResource>().Single(x => Equals(x.Id, oldExecution.Id));
                newPlanification = db.Query<IPlannedResource>().Single(x => Equals(x.Id, newPlanification.Id));
                oldPlanification = db.Query<IPlannedResource>().Single(x => Equals(x.Id, oldPlanification.Id));

                Assert.AreSame(newPlanification, executedResource.Planification);
                Assert.AreSame(executedResource, newPlanification.Execution);
                Assert.IsNull(oldPlanification.Execution);
                Assert.IsNull(oldExecution.Planification);
            }
        }

        [TestMethod]
        public void Update_GivenExecutedResource_RestoresRelationshipWithItsComponentIfSuchRelationshipIsBroken()
        {
            // Arrange
            IBudgetComponent budgetComponent = new BudgetComponentStub { Id = Guid.NewGuid() };
            IExecutedResource executedResource = new ExecutedResourceStub { Component = budgetComponent, Id = Guid.NewGuid() };
            budgetComponent.ExecutedResources.Add(executedResource);

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Store(executedResource);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new RepositoryStub(db) { Component = budgetComponent };
                budgetComponent.ExecutedResources.Remove(executedResource);

                IExecutedResource dbExecutedResource = db.Query<IExecutedResource>(x => Equals(x.Id, executedResource.Id)).Single();
                IBudgetComponent dbComponent = db.Query<IBudgetComponent>(x => Equals(x.Id, budgetComponent.Id)).Single();
                dbComponent.ExecutedResources.Remove(dbExecutedResource);
                dbExecutedResource.Component = null;

                repository.Update(executedResource);
                db.Commit();
            }

            // Assert
            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                IBudgetComponent component = db.Query<IBudgetComponent>().Single();
                executedResource = db.Query<IExecutedResource>().Single();

                Assert.AreSame(component, executedResource.Component);
                CollectionAssert.Contains(component.ExecutedResources.ToArray(), executedResource);
            }
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Delete_GivenNullExecutedResource_Throws()
        {
            // Arrange
            var repository = new Mock<ExecutedResourceRepositoryBase<IBudgetComponent>>(Mock.Of<IDb4ODatabaseContext>()) { CallBase = true };

            // Act
            repository.Object.Delete(null);
        }

        [TestMethod]
        public void Delete_GivenExecutedResource_DeletesIt()
        {
            // Arrange
            IBudgetComponent budgetComponent = new BudgetComponentStub { Id = Guid.NewGuid() };
            IExecutedResource executedResource = new ExecutedResourceStub { Component = budgetComponent, Id = Guid.NewGuid() };
            budgetComponent.ExecutedResources.Add(executedResource);

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Store(executedResource);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new RepositoryStub(db) { Component = budgetComponent };

                repository.Delete(executedResource);
                db.Commit();
            }

            // Assert
            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                IBudgetComponent component = db.Query<IBudgetComponent>().Single();

                Assert.IsFalse(db.Query<IBudgetComponentItem>().Any());
                Assert.IsFalse(component.ExecutedResources.Any());
            }
        }

        [TestMethod]
        public void Delete_GivenExecutedResourceWithPlanification_PlanificationStopsHavingAnExecution()
        {
            // Arrange
            IBudgetComponent budgetComponent = new BudgetComponentStub { Id = Guid.NewGuid() };
            IPlannedResource planification = new PlannedResourceStub { Id = Guid.NewGuid() };
            IExecutedResource executedResource = new ExecutedResourceStub
            {
                Component = budgetComponent,
                Id = Guid.NewGuid(),
                Planification = planification
            };
            planification.Execution = executedResource;
            budgetComponent.ExecutedResources.Add(executedResource);

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Store(planification);
                db.Store(executedResource);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new RepositoryStub(db) { Component = budgetComponent };

                repository.Delete(executedResource);
                db.Commit();
            }

            // Assert
            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                planification = db.Query<IPlannedResource>().Single();

                Assert.IsNull(planification.Execution);
            }
        }


        [TestMethod]
        public void Delete_GivenExecutedResourceWithPlanificationWhichNoLongerExistsInDatabase_TheyBothAreWipedOut()
        {
            // Arrange
            var budgetComponent = new BudgetComponentStub { Id = Guid.NewGuid() };
            var plannedItem = new PlannedResourceStub { Id = Guid.NewGuid() };
            var executedItem = new ExecutedResourceStub { Id = Guid.NewGuid(), Planification = plannedItem };
            plannedItem.Execution = executedItem;
            budgetComponent.ExecutedResources.Add(executedItem);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Store(plannedItem);
                db.Store(executedItem);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var dbPlannedItem = db.Find<IPlannedResource>(plannedItem.Id);
                db.Delete(dbPlannedItem);
                db.Commit();

                var repository = new RepositoryStub(db) { Component = budgetComponent };
                repository.Delete(executedItem);

                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                Assert.AreEqual(5, db.Query<object>().Count());
                Assert.IsFalse(db.Query<IPlannedResource>().Any());
                Assert.IsFalse(db.Query<IExecutedResource>().Any());
            }
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Relate_GivenNullPlannedResource_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new Mock<ExecutedResourceRepositoryBase<IBudgetComponent>>(db) { CallBase = true };

            // Act
            repository.Object.Relate(Mock.Of<IExecutedResource>(), null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Relate_GivenNullExecutedResource_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new Mock<ExecutedResourceRepositoryBase<IBudgetComponent>>(db) { CallBase = true };

            // Act
            repository.Object.Relate(null, Mock.Of<IPlannedResource>());
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Unrelate_GivenNullPlannedResource_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new Mock<ExecutedResourceRepositoryBase<IBudgetComponent>>(db) { CallBase = true };

            // Act
            repository.Object.Unrelate(Mock.Of<IExecutedResource>(), null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Unrelate_GivenNullExecutedResource_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new Mock<ExecutedResourceRepositoryBase<IBudgetComponent>>(db) { CallBase = true };

            // Act
            repository.Object.Unrelate(null, Mock.Of<IPlannedResource>());
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void SaveReference_GivenNullPlannedResource_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new Mock<ExecutedResourceRepositoryBase<IBudgetComponent>>(db) { CallBase = true };

            // Act
            repository.Object.SaveReference(null);
        }


        public class RepositoryStub : ExecutedBudgetComponentItemRepositoryBase<IExecutedResource, IBudgetComponent>
        {
            public RepositoryStub(IDb4ODatabaseContext databaseContext)
                : base(databaseContext)
            {
            }


            protected override Func<IBudgetComponent, IList<IExecutedResource>> GetItemCollection
            {
                get { return x => x.ExecutedResources; }
            }


            public Func<IBudgetComponent, IList<IExecutedResource>> GetActualItemCollection()
            {
                return GetItemCollection;
            }
        }
    }
}