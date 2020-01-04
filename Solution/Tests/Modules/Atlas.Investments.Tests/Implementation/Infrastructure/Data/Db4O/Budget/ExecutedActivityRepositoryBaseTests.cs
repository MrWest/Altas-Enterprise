using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget;
using CompanyName.Atlas.Investments.Tests.Implementation.Domain.Entities;
using Db4objects.Db4o;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Infrastructure.Data.Db4O.Budget
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ExecutedActivityRepositoryBaseTests : TestBase
    {
        private readonly string _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdb.db");


        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IExecutedActivity>()).Returns(() => new ExecutedActivityStub());
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_dbPath))
                File.Delete(_dbPath);
        }


        [TestMethod]
        public void GetItemCollection_ReturnsMethodAllowingToObtainCorrectExecutedActivitiesListOfCurrentBudgetComponent()
        {
            // Arrange
            var expectedList = Mock.Of<IList<IExecutedActivity>>();
            var budgetComponent = Mock.Of<IBudgetComponent>(x => x.ExecutedActivities == expectedList);
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new RepositoryStub(db);
            var internals = new PrivateObject(repository);

            // Act
            var method = (Func<IBudgetComponent, IList<IExecutedActivity>>)internals.GetProperty("GetItemCollection");
            IList<IExecutedActivity> actualList = method(budgetComponent);

            // Assert
            Assert.AreSame(expectedList, actualList);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Add_GivenNullExecutedActivity_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new RepositoryStub(db);

            // Act
            repository.Add(null);
        }

        [TestMethod]
        public void Add_GivenExecutedActivityWithoutPlanification_AddsItPersistingItsOwnData()
        {
            // Arrange
            var budgetComponent = new SubSpecialityHolderStub { Id = Guid.NewGuid().ToString() };

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new RepositoryStub(db) { SubSpecialityHolder = budgetComponent };

                var newExecutedActivity = new ExecutedActivityStub
                {
                    Name = "N",
                    Description = "D",
                    Quantity = 1,
                    Code = "C"
                };

                repository.Add(newExecutedActivity);
                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                var component = db.Query<IBudgetComponent>().Single();

                var executedActivity = db.Query<IExecutedActivity>().Single();
                Assert.IsInstanceOfType(executedActivity.Id, typeof(Guid));
                Assert.AreNotEqual(default(Guid).ToString(), executedActivity.Id.ToString());
                Assert.AreEqual("N", executedActivity.Name);
                Assert.AreEqual("D", executedActivity.Description);
                Assert.AreEqual(1, executedActivity.Quantity);
                Assert.AreEqual("C", executedActivity.Code);
            }
        }

        [TestMethod]
        public void Add_GivenExecutedActivityWithPlanification_AddsItPersistingItsPlanificationData()
        {
            // Arrange
            var budgetComponent = new PlannedSubSpecialityHolderStub { Id = Guid.NewGuid().ToString() };
            var plannedActivity = new PlannedActivityStub
            {
                Id = Guid.NewGuid().ToString(),
                SubSpecialityHolder = budgetComponent,
                Name = "N",
                Description = "D",
                Quantity = 1,
                Code = "C"
            };
            budgetComponent.PlannedActivities.Add(plannedActivity);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new RepositoryStub(db) { SubSpecialityHolder = budgetComponent };

                var newExecutedActivity = new ExecutedActivityStub
                {
                    Name = "NN",
                    Description = "DD",
                    Quantity = 11,
                    Code = "CC",
                    Planification = plannedActivity
                };

                repository.Add(newExecutedActivity);
                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                var component = db.Query<IBudgetComponent>().Single();

                var executedActivity = db.Query<IExecutedActivity>().Single();
                Assert.AreEqual("N", executedActivity.Name);
                Assert.AreEqual("D", executedActivity.Description);
                Assert.AreEqual(1, executedActivity.Quantity);
                Assert.AreEqual("C", executedActivity.Code);
            }
        }

        [TestMethod]
        public void Add_GivenExecutedActivityWithPlanification_RelatesItToThatPlanification()
        {
            // Arrange
            IPlannedSubSpecialityHolder budgetComponent = new PlannedSubSpecialityHolderStub { Id = Guid.NewGuid().ToString() };
            IPlannedActivity plannedActivity = new PlannedActivityStub { Id = Guid.NewGuid().ToString() };
            budgetComponent.PlannedActivities.Add(plannedActivity);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Commit();
            }

            // Act
            IExecutedActivity executedActivity;
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new RepositoryStub(db) { SubSpecialityHolder = budgetComponent };

                executedActivity = new ExecutedActivityStub { Planification = plannedActivity };

                repository.Add(executedActivity);
                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                Assert.AreSame(plannedActivity, executedActivity.Planification);
                Assert.AreSame(executedActivity, plannedActivity.Execution);

                budgetComponent = db.Query<IPlannedSubSpecialityHolder>().Single();
                plannedActivity = db.Query<IPlannedActivity>().Single();
                executedActivity = db.Query<IExecutedActivity>().Single();

                Assert.AreSame(plannedActivity, executedActivity.Planification);
                Assert.AreSame(executedActivity, plannedActivity.Execution);
            }
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Update_GivenNullExecutedActivity_Throws()
        {
            // Arrange
            var repository = new Mock<ExecutedActivityRepositoryBase>(Mock.Of<IDb4ODatabaseContext>()) { CallBase = true };

            // Act
            repository.Object.Update(null);
        }

        [TestMethod]
        public void Update_GivenExecutedActivity_UpdatesItsFieldsData()
        {
            // Arrange
            IExecutedSubSpecialityHolder subSpecialityHolder = new ExecutedSubSpecialityHolderStub() { Id = Guid.NewGuid().ToString() };
            IExecutedActivity executedActivity = new ExecutedActivityStub
            {
                Code = "C",
                SubSpecialityHolder = subSpecialityHolder,
                Description = "D",
                Id = Guid.NewGuid().ToString(),
                Name = "N",
                Quantity = 10,
            };
            subSpecialityHolder.ExecutedActivities.Add(executedActivity);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(subSpecialityHolder);
                db.Store(executedActivity);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new RepositoryStub(db) { SubSpecialityHolder = subSpecialityHolder };

                executedActivity.Code = "CC";
                executedActivity.Name = "NN";
                executedActivity.Description = "DD";
                executedActivity.Quantity = 100;

                repository.Update(executedActivity);
                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                var component = db.Query<IBudgetComponent>().Single();

                executedActivity = db.Query<IExecutedActivity>().Single();
                Assert.AreEqual("NN", executedActivity.Name);
                Assert.AreEqual("DD", executedActivity.Description);
                Assert.AreEqual(100, executedActivity.Quantity);
                Assert.AreEqual("CC", executedActivity.Code);
            }
        }

        [TestMethod]
        public void Update_GivenExecutedActivityWithNullPlanification_BreaksTheRelationshipWithItsActualOne()
        {
            // Arrange
            IExecutedSubSpecialityHolder budgetComponent = new ExecutedSubSpecialityHolderStub() { Id = Guid.NewGuid().ToString() };
            IPlannedActivity planification = new PlannedActivityStub { Id = Guid.NewGuid().ToString() };
            IExecutedActivity executedActivity = new ExecutedActivityStub
            {
                Id = Guid.NewGuid().ToString(),
                SubSpecialityHolder = budgetComponent,
                Planification = planification
            };
            planification.Execution = executedActivity;
            budgetComponent.ExecutedActivities.Add(executedActivity);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Store(executedActivity);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new RepositoryStub(db) { SubSpecialityHolder = budgetComponent };

                executedActivity.Planification = null;

                repository.Update(executedActivity);
                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                var component = db.Query<IBudgetComponent>().Single();

                executedActivity = db.Query<IExecutedActivity>().Single();
                planification = db.Query<IPlannedActivity>().Single();

                Assert.IsNull(executedActivity.Planification);
                Assert.IsNull(planification.Execution);
            }
        }

        [TestMethod]
        public void Update_GivenExecutedActivityWithAnotherPlanification_BreaksTheRelationshipWithItsActualOneAndEstablishesARelationshipWithTheCurrentOne()
        {
            // Arrange
            IExecutedSubSpecialityHolder budgetComponent = new ExecutedSubSpecialityHolderStub { Id = Guid.NewGuid().ToString() };
            IPlannedActivity oldPlanification = new PlannedActivityStub { Id = Guid.NewGuid().ToString() };
            IExecutedActivity executedActivity = new ExecutedActivityStub
            {
                SubSpecialityHolder = budgetComponent,
                Id = Guid.NewGuid().ToString(),
                Planification = oldPlanification
            };
            oldPlanification.Execution = executedActivity;
            budgetComponent.ExecutedActivities.Add(executedActivity);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Store(oldPlanification);
                db.Store(executedActivity);
                db.Commit();
            }

            // Act
            IExecutedActivity oldExecution = new ExecutedActivityStub { Id = Guid.NewGuid().ToString() };
            IPlannedActivity newPlanification = new PlannedActivityStub { Id = Guid.NewGuid().ToString(), Execution = oldExecution };
            oldExecution.Planification = newPlanification;
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                db.Store(oldExecution);
                db.Store(newPlanification);
                db.Commit();

                var repository = new RepositoryStub(db) { SubSpecialityHolder = budgetComponent };

                executedActivity.Planification = newPlanification;

                repository.Update(executedActivity);
                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                var component = db.Query<IBudgetComponent>().Single();

                executedActivity = db.Query<IExecutedActivity>().Single(x => Equals(x.Id, executedActivity.Id));
                oldExecution = db.Query<IExecutedActivity>().Single(x => Equals(x.Id, oldExecution.Id));
                newPlanification = db.Query<IPlannedActivity>().Single(x => Equals(x.Id, newPlanification.Id));
                oldPlanification = db.Query<IPlannedActivity>().Single(x => Equals(x.Id, oldPlanification.Id));

                Assert.AreSame(newPlanification.Id.ToString(), executedActivity.Planification.ToString());
                Assert.AreSame(executedActivity.Id.ToString(), newPlanification.Execution.ToString());
                Assert.IsNull(oldPlanification.Execution);
                Assert.IsNull(oldExecution.Planification);
            }
        }

        [TestMethod]
        public void Update_GivenExecutedActivity_RestoresRelationshipWithItsComponentIfSuchRelationshipIsBroken()
        {
            // Arrange
            IExecutedSubSpecialityHolder budgetComponent = new ExecutedSubSpecialityHolderStub { Id = Guid.NewGuid().ToString() };
            IExecutedActivity executedActivity = new ExecutedActivityStub { SubSpecialityHolder = budgetComponent, Id = Guid.NewGuid().ToString() };
            budgetComponent.ExecutedActivities.Add(executedActivity);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Store(executedActivity);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new RepositoryStub(db) { SubSpecialityHolder = budgetComponent };
                budgetComponent.ExecutedActivities.Remove(executedActivity);

                var dbExecutedActivity = db.Query<IExecutedActivity>(x => Equals(x.Id, executedActivity.Id)).Single();
                var dbComponent = db.Query<IBudgetComponent>(x => Equals(x.Id, budgetComponent.Id)).Single();
                dbComponent.ExecutedActivities.Remove(dbExecutedActivity);
                dbExecutedActivity.SubSpecialityHolder = null;

                repository.Update(executedActivity);
                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                var component = db.Query<IBudgetComponent>().Single();
                executedActivity = db.Query<IExecutedActivity>().Single();

                Assert.AreSame(component, executedActivity.SubSpecialityHolder);
                CollectionAssert.Contains(component.ExecutedActivities.ToArray(), executedActivity);
            }
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Delete_GivenNullExecutedActivity_Throws()
        {
            // Arrange
            var repository = new Mock<ExecutedActivityRepositoryBase>(Mock.Of<IDb4ODatabaseContext>()) { CallBase = true };

            // Act
            repository.Object.Delete(null);
        }

        [TestMethod]
        public void Delete_GivenExecutedActivity_DeletesIt()
        {
            // Arrange
            IExecutedSubSpecialityHolder budgetComponent = new ExecutedSubSpecialityHolderStub { Id = Guid.NewGuid().ToString() };
            IExecutedActivity executedActivity = new ExecutedActivityStub { SubSpecialityHolder = budgetComponent, Id = Guid.NewGuid().ToString() };
            budgetComponent.ExecutedActivities.Add(executedActivity);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Store(executedActivity);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new RepositoryStub(db) { SubSpecialityHolder = budgetComponent };

                repository.Delete(executedActivity);
                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                var component = db.Query<IBudgetComponent>().Single();

                Assert.IsFalse(db.Query<IBudgetComponentItem>().Any());
                Assert.IsFalse(component.ExecutedActivities.Any());
            }
        }

        [TestMethod]
        public void Delete_GivenExecutedActivityWithPlanification_PlanificationStopsHavingAnExecution()
        {
            // Arrange
            IExecutedSubSpecialityHolder budgetComponent = new ExecutedSubSpecialityHolderStub { Id = Guid.NewGuid().ToString() };
            IPlannedActivity planification = new PlannedActivityStub { Id = Guid.NewGuid().ToString() };
            IExecutedActivity executedActivity = new ExecutedActivityStub
            {
                SubSpecialityHolder = budgetComponent,
                Id = Guid.NewGuid().ToString(),
                Planification = planification
            };
            planification.Execution = executedActivity;
            budgetComponent.ExecutedActivities.Add(executedActivity);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(budgetComponent);
                db.Store(planification);
                db.Store(executedActivity);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new RepositoryStub(db) { SubSpecialityHolder = budgetComponent };

                repository.Delete(executedActivity);
                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                planification = db.Query<IPlannedActivity>().Single();

                Assert.IsNull(planification.Execution);
            }
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Relate_GivenNullPlannedActivity_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new Mock<ExecutedActivityRepositoryBase>(db) { CallBase = true };

            // Act
            repository.Object.Relate(Mock.Of<IExecutedActivity>(), (ISubSpecialityHolder)null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Relate_GivenNullExecutedActivity_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new Mock<ExecutedActivityRepositoryBase>(db) { CallBase = true };

            // Act
            repository.Object.Relate(null, Mock.Of<ISubSpecialityHolder>());
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Unrelate_GivenNullPlannedActivity_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new Mock<ExecutedActivityRepositoryBase>(db) { CallBase = true };

            // Act
            repository.Object.Unrelate(Mock.Of<IExecutedActivity>(), (ISubSpecialityHolder)null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Unrelate_GivenNullExecutedActivity_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new Mock<ExecutedActivityRepositoryBase>(db) { CallBase = true };

            // Act
            repository.Object.Unrelate(null, Mock.Of<ISubSpecialityHolder>());
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void SaveReference_GivenNullPlannedActivity_Throws()
        {
            // Arrange
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new Mock<ExecutedActivityRepositoryBase>(db) { CallBase = true };

            // Act
            repository.Object.SaveReference((ISubSpecialityHolder)null);
        }


        public class RepositoryStub : ExecutedActivityRepositoryBase
        {
            public RepositoryStub(IDb4ODatabaseContext databaseContext)
                : base(databaseContext)
            {
            }


            //public new Func<IBudgetComponent, IList<IExecutedActivity>> GetItemCollection
            //{
            //    get { return base.GetItemCollection; }
            //}
        }
    }
}
