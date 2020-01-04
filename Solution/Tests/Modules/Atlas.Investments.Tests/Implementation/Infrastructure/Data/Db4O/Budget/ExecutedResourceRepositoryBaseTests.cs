using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget;
using CompanyName.Atlas.Investments.Tests.Implementation.Domain.Entities;
using Db4objects.Db4o;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Infrastructure.Data.Db4O.Budget
{
    //[TestClass, ExcludeFromCodeCoverage]
    //public class ExecutedResourceRepositoryBaseTests : TestBase
    //{
    //    private readonly string _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdb.db");


    //    [TestInitialize]
    //    public override void Initialize()
    //    {
    //        base.Initialize();

    //        ServiceLocatorMock.Setup(x => x.GetInstance<IExecutedResource>()).Returns(() => new ExecutedResourceStub());
    //    }

    //    [TestCleanup]
    //    public void Cleanup()
    //    {
    //        if (File.Exists(_dbPath))
    //            File.Delete(_dbPath);
    //    }


    //    [TestMethod]
    //    public void GetItemCollection_ReturnsMethodAllowingToObtainCorrectExecutedResourceListOfCurrentBudgetComponent()
    //    {
    //        // Arrange
    //        var expectedList = Mock.Of<IList<IExecutedResource>>();
    //        var budgetComponent = Mock.Of<IBudgetComponent>(x => x.ExecutedResources == expectedList);
    //        var db = Mock.Of<IDb4ODatabaseContext>();
    //        var repository = new RepositoryStub(db);
    //        var internals = new PrivateObject(repository);

    //        // Act
    //        var method = (Func<IBudgetComponent, IList<IExecutedResource>>)internals.GetProperty("GetItemCollection");
    //        IList<IExecutedResource> actualList = method(budgetComponent);

    //        // Assert
    //        Assert.AreSame(expectedList, actualList);
    //    }


    //    [TestMethod, ExpectedException(typeof(ArgumentNullException))]
    //    public void Add_GivenNullExecutedResource_Throws()
    //    {
    //        // Arrange
    //        var db = Mock.Of<IDb4ODatabaseContext>();
    //        var repository = new RepositoryStub(db);

    //        // Act
    //        repository.Add(null);
    //    }

    //    [TestMethod]
    //    public void Add_GivenExecutedResourceWithoutPlanification_AddsItPersistingItsOwnData()
    //    {
    //        // Arrange
    //        var budgetComponent = new BudgetComponentStub { Id = Guid.NewGuid() };

    //        using (var db = Db4oEmbedded.OpenFile(_dbPath))
    //        {
    //            db.Store(budgetComponent);
    //            db.Commit();
    //        }

    //        // Act
    //        using (var db = new Db4ODatabaseContext(_dbPath))
    //        {
    //            var repository = new RepositoryStub(db) { Component = budgetComponent };

    //            var newExecutedResource = new ExecutedResourceStub
    //            {
    //                Name = "N",
    //                Description = "D",
    //                Quantity = 1,
    //                Code = "C"
    //            };

    //            repository.Add(newExecutedResource);
    //            db.Commit();
    //        }

    //        // Assert
    //        using (var db = Db4oEmbedded.OpenFile(_dbPath))
    //        {
    //            var component = db.Query<IBudgetComponent>().Single();

    //            var executedResource = db.Query<IExecutedResource>().Single();
    //            Assert.IsInstanceOfType(executedResource.Id, typeof(Guid));
    //            Assert.AreNotEqual(default(Guid).ToString(), executedResource.Id.ToString());
    //            Assert.AreEqual("N", executedResource.Name);
    //            Assert.AreEqual("D", executedResource.Description);
    //            Assert.AreEqual(1, executedResource.Quantity);
    //            Assert.AreEqual("C", executedResource.Code);
    //        }
    //    }

    //    [TestMethod]
    //    public void Add_GivenExecutedResourceWithPlanification_AddsItPersistingItsPlanificationData()
    //    {
    //        // Arrange
    //        var budgetComponent = new BudgetComponentStub { Id = Guid.NewGuid() };
    //        var plannedResource = new PlannedResourceStub
    //        {
    //            Id = Guid.NewGuid().ToString(),
    //            Component = budgetComponent,
    //            Name = "N",
    //            Description = "D",
    //            Quantity = 1,
    //            Code = "C"
    //        };
    //        budgetComponent.PlannedResources.Add(plannedResource);

    //        using (var db = Db4oEmbedded.OpenFile(_dbPath))
    //        {
    //            db.Store(budgetComponent);
    //            db.Commit();
    //        }

    //        // Act
    //        using (var db = new Db4ODatabaseContext(_dbPath))
    //        {
    //            var repository = new RepositoryStub(db) { Component = budgetComponent };

    //            var newExecutedResource = new ExecutedResourceStub
    //            {
    //                Name = "NN",
    //                Description = "DD",
    //                Quantity = 11,
    //                Code = "CC",
    //                Planification = plannedResource
    //            };

    //            repository.Add(newExecutedResource);
    //            db.Commit();
    //        }

    //        // Assert
    //        using (var db = Db4oEmbedded.OpenFile(_dbPath))
    //        {
    //            var component = db.Query<IBudgetComponent>().Single();

    //            var executedResource = db.Query<IExecutedResource>().Single();
    //            Assert.AreEqual("N", executedResource.Name);
    //            Assert.AreEqual("D", executedResource.Description);
    //            Assert.AreEqual(1, executedResource.Quantity);
    //            Assert.AreEqual("C", executedResource.Code);
    //        }
    //    }

    //    [TestMethod]
    //    public void Add_GivenExecutedResourceWithPlanification_RelatesItToThatPlanification()
    //    {
    //        // Arrange
    //        IBudgetComponent budgetComponent = new BudgetComponentStub { Id = Guid.NewGuid() };
    //        IPlannedResource plannedResource = new PlannedResourceStub { Id = Guid.NewGuid() };
    //        budgetComponent.PlannedResources.Add(plannedResource);

    //        using (var db = Db4oEmbedded.OpenFile(_dbPath))
    //        {
    //            db.Store(budgetComponent);
    //            db.Commit();
    //        }

    //        // Act
    //        IExecutedResource executedResource;
    //        using (var db = new Db4ODatabaseContext(_dbPath))
    //        {
    //            var repository = new RepositoryStub(db) { Component = budgetComponent };

    //            executedResource = new ExecutedResourceStub { Planification = plannedResource };

    //            repository.Add(executedResource);
    //            db.Commit();
    //        }

    //        // Assert
    //        using (var db = Db4oEmbedded.OpenFile(_dbPath))
    //        {
    //            Assert.AreSame(plannedResource, executedResource.Planification);
    //            Assert.AreSame(executedResource, plannedResource.Execution);

    //            budgetComponent = db.Query<IBudgetComponent>().Single();
    //            plannedResource = db.Query<IPlannedResource>().Single();
    //            executedResource = db.Query<IExecutedResource>().Single();
                
    //            Assert.AreSame(plannedResource, executedResource.Planification);
    //            Assert.AreSame(executedResource, plannedResource.Execution);
    //        }
    //    }


    //    [TestMethod, ExpectedException(typeof(ArgumentNullException))]
    //    public void Update_GivenNullExecutedResource_Throws()
    //    {
    //        // Arrange
    //        var repository = new Mock<ExecutedResourceRepositoryBase<IBudgetComponent>>(Mock.Of<IDb4ODatabaseContext>()) { CallBase = true };

    //        // Act
    //        repository.Object.Update(null);
    //    }

    //    [TestMethod]
    //    public void Update_GivenExecutedResource_UpdatesItsFieldsData()
    //    {
    //        // Arrange
    //        IBudgetComponent budgetComponent = new BudgetComponentStub { Id = Guid.NewGuid() };
    //        IExecutedResource executedResource = new ExecutedResourceStub
    //        {
    //            Code = "C",
    //            Component = budgetComponent,
    //            Description = "D",
    //            Id = Guid.NewGuid().ToString(),
    //            Name = "N",
    //            Quantity = 10,
    //        };
    //        budgetComponent.ExecutedResources.Add(executedResource);

    //        using (var db = Db4oEmbedded.OpenFile(_dbPath))
    //        {
    //            db.Store(budgetComponent);
    //            db.Store(executedResource);
    //            db.Commit();
    //        }

    //        // Act
    //        using (var db = new Db4ODatabaseContext(_dbPath))
    //        {
    //            var repository = new RepositoryStub(db) { Component = budgetComponent };

    //            executedResource.Code = "CC";
    //            executedResource.Name = "NN";
    //            executedResource.Description = "DD";
    //            executedResource.Quantity = 100;

    //            repository.Update(executedResource);
    //            db.Commit();
    //        }

    //        // Assert
    //        using (var db = Db4oEmbedded.OpenFile(_dbPath))
    //        {
    //            var component = db.Query<IBudgetComponent>().Single();

    //            executedResource = db.Query<IExecutedResource>().Single();
    //            Assert.AreEqual("NN", executedResource.Name);
    //            Assert.AreEqual("DD", executedResource.Description);
    //            Assert.AreEqual(100, executedResource.Quantity);
    //            Assert.AreEqual("CC", executedResource.Code);
    //        }
    //    }

    //    [TestMethod]
    //    public void Update_GivenExecutedResourceWithNullPlanification_BreaksTheRelationshipWithItsActualOne()
    //    {
    //        // Arrange
    //        IBudgetComponent budgetComponent = new BudgetComponentStub { Id = Guid.NewGuid() };
    //        IPlannedResource planification = new PlannedResourceStub { Id = Guid.NewGuid() };
    //        IExecutedResource executedResource = new ExecutedResourceStub
    //        {
    //            Id = Guid.NewGuid().ToString(),
    //            Component = budgetComponent,
    //            Planification = planification
    //        };
    //        planification.Execution = executedResource;
    //        budgetComponent.ExecutedResources.Add(executedResource);

    //        using (var db = Db4oEmbedded.OpenFile(_dbPath))
    //        {
    //            db.Store(budgetComponent);
    //            db.Store(executedResource);
    //            db.Commit();
    //        }

    //        // Act
    //        using (var db = new Db4ODatabaseContext(_dbPath))
    //        {
    //            var repository = new RepositoryStub(db) { Component = budgetComponent };

    //            executedResource.Planification = null;

    //            repository.Update(executedResource);
    //            db.Commit();
    //        }

    //        // Assert
    //        using (var db = Db4oEmbedded.OpenFile(_dbPath))
    //        {
    //            var component = db.Query<IBudgetComponent>().Single();

    //            executedResource = db.Query<IExecutedResource>().Single();
    //            planification = db.Query<IPlannedResource>().Single();

    //            Assert.IsNull(executedResource.Planification);
    //            Assert.IsNull(planification.Execution);
    //        }
    //    }

    //    [TestMethod]
    //    public void Update_GivenExecutedResourceWithAnotherPlanification_BreaksTheRelationshipWithItsActualOneAndEstablishesARelationshipWithTheCurrentOne()
    //    {
    //        // Arrange
    //        IBudgetComponent budgetComponent = new BudgetComponentStub { Id = Guid.NewGuid() };
    //        IPlannedResource oldPlanification = new PlannedResourceStub { Id = Guid.NewGuid() };
    //        IExecutedResource executedResource = new ExecutedResourceStub
    //        {
    //            Component = budgetComponent,
    //            Id = Guid.NewGuid().ToString(),
    //            Planification = oldPlanification
    //        };
    //        oldPlanification.Execution = executedResource;
    //        budgetComponent.ExecutedResources.Add(executedResource);

    //        using (var db = Db4oEmbedded.OpenFile(_dbPath))
    //        {
    //            db.Store(budgetComponent);
    //            db.Store(oldPlanification);
    //            db.Store(executedResource);
    //            db.Commit();
    //        }

    //        // Act
    //        IExecutedResource oldExecution = new ExecutedResourceStub { Id = Guid.NewGuid() };
    //        IPlannedResource newPlanification = new PlannedResourceStub { Id = Guid.NewGuid().ToString(), Execution = oldExecution };
    //        oldExecution.Planification = newPlanification;
    //        using (var db = new Db4ODatabaseContext(_dbPath))
    //        {
    //            db.Store(oldExecution);
    //            db.Store(newPlanification);
    //            db.Commit();

    //            var repository = new RepositoryStub(db) { Component = budgetComponent };

    //            executedResource.Planification = newPlanification;

    //            repository.Update(executedResource);
    //            db.Commit();
    //        }

    //        // Assert
    //        using (var db = Db4oEmbedded.OpenFile(_dbPath))
    //        {
    //            var component = db.Query<IBudgetComponent>().Single();

    //            executedResource = db.Query<IExecutedResource>().Single(x => Equals(x.Id, executedResource.Id));
    //            oldExecution = db.Query<IExecutedResource>().Single(x => Equals(x.Id, oldExecution.Id));
    //            newPlanification = db.Query<IPlannedResource>().Single(x => Equals(x.Id, newPlanification.Id));
    //            oldPlanification = db.Query<IPlannedResource>().Single(x => Equals(x.Id, oldPlanification.Id));

    //            Assert.AreSame(newPlanification, executedResource.Planification);
    //            Assert.AreSame(executedResource, newPlanification.Execution);
    //            Assert.IsNull(oldPlanification.Execution);
    //            Assert.IsNull(oldExecution.Planification);
    //        }
    //    }

    //    [TestMethod]
    //    public void Update_GivenExecutedResource_RestoresRelationshipWithItsComponentIfSuchRelationshipIsBroken()
    //    {
    //        // Arrange
    //        IBudgetComponent budgetComponent = new BudgetComponentStub { Id = Guid.NewGuid() };
    //        IExecutedResource executedResource = new ExecutedResourceStub { Component = budgetComponent, Id = Guid.NewGuid() };
    //        budgetComponent.ExecutedResources.Add(executedResource);

    //        using (var db = Db4oEmbedded.OpenFile(_dbPath))
    //        {
    //            db.Store(budgetComponent);
    //            db.Store(executedResource);
    //            db.Commit();
    //        }

    //        // Act
    //        using (var db = new Db4ODatabaseContext(_dbPath))
    //        {
    //            var repository = new RepositoryStub(db) { Component = budgetComponent };
    //            budgetComponent.ExecutedResources.Remove(executedResource);

    //            var dbExecutedResource = db.Query<IExecutedResource>(x => Equals(x.Id, executedResource.Id)).Single();
    //            var dbComponent = db.Query<IBudgetComponent>(x => Equals(x.Id, budgetComponent.Id)).Single();
    //            dbComponent.ExecutedResources.Remove(dbExecutedResource);
    //            dbExecutedResource.Component = null;

    //            repository.Update(executedResource);
    //            db.Commit();
    //        }

    //        // Assert
    //        using (var db = Db4oEmbedded.OpenFile(_dbPath))
    //        {
    //            var component = db.Query<IBudgetComponent>().Single();
    //            executedResource = db.Query<IExecutedResource>().Single();

    //            Assert.AreSame(component, executedResource.Component);
    //            CollectionAssert.Contains(component.ExecutedResources.ToArray(), executedResource);
    //        }
    //    }


    //    [TestMethod, ExpectedException(typeof(ArgumentNullException))]
    //    public void Delete_GivenNullExecutedResource_Throws()
    //    {
    //        // Arrange
    //        var repository = new Mock<ExecutedResourceRepositoryBase<IBudgetComponent>>(Mock.Of<IDb4ODatabaseContext>()) { CallBase = true };

    //        // Act
    //        repository.Object.Delete(null);
    //    }

    //    [TestMethod]
    //    public void Delete_GivenExecutedResource_DeletesIt()
    //    {
    //        // Arrange
    //        IBudgetComponent budgetComponent = new BudgetComponentStub { Id = Guid.NewGuid() };
    //        IExecutedResource executedResource = new ExecutedResourceStub { Component = budgetComponent, Id = Guid.NewGuid() };
    //        budgetComponent.ExecutedResources.Add(executedResource);

    //        using (var db = Db4oEmbedded.OpenFile(_dbPath))
    //        {
    //            db.Store(budgetComponent);
    //            db.Store(executedResource);
    //            db.Commit();
    //        }

    //        // Act
    //        using (var db = new Db4ODatabaseContext(_dbPath))
    //        {
    //            var repository = new RepositoryStub(db) { Component = budgetComponent };

    //            repository.Delete(executedResource);
    //            db.Commit();
    //        }

    //        // Assert
    //        using (var db = Db4oEmbedded.OpenFile(_dbPath))
    //        {
    //            var component = db.Query<IBudgetComponent>().Single();

    //            Assert.IsFalse(db.Query<IBudgetComponentItem>().Any());
    //            Assert.IsFalse(component.ExecutedResources.Any());
    //        }
    //    }

    //    [TestMethod]
    //    public void Delete_GivenExecutedResourceWithPlanification_PlanificationStopsHavingAnExecution()
    //    {
    //        // Arrange
    //        IBudgetComponent budgetComponent = new BudgetComponentStub { Id = Guid.NewGuid() };
    //        IPlannedResource planification = new PlannedResourceStub { Id = Guid.NewGuid() };
    //        IExecutedResource executedResource = new ExecutedResourceStub
    //        {
    //            Component = budgetComponent,
    //            Id = Guid.NewGuid().ToString(),
    //            Planification = planification
    //        };
    //        planification.Execution = executedResource;
    //        budgetComponent.ExecutedResources.Add(executedResource);

    //        using (var db = Db4oEmbedded.OpenFile(_dbPath))
    //        {
    //            db.Store(budgetComponent);
    //            db.Store(planification);
    //            db.Store(executedResource);
    //            db.Commit();
    //        }

    //        // Act
    //        using (var db = new Db4ODatabaseContext(_dbPath))
    //        {
    //            var repository = new RepositoryStub(db) { Component = budgetComponent };

    //            repository.Delete(executedResource);
    //            db.Commit();
    //        }

    //        // Assert
    //        using (var db = Db4oEmbedded.OpenFile(_dbPath))
    //        {
    //            planification = db.Query<IPlannedResource>().Single();

    //            Assert.IsNull(planification.Execution);
    //        }
    //    }


    //    [TestMethod, ExpectedException(typeof(ArgumentNullException))]
    //    public void Relate_GivenNullPlannedResource_Throws()
    //    {
    //        // Arrange
    //        var db = Mock.Of<IDb4ODatabaseContext>();
    //        var repository = new Mock<ExecutedResourceRepositoryBase<IBudgetComponent>>(db) { CallBase = true };

    //        // Act
    //        repository.Object.Relate(Mock.Of<IExecutedResource>(), (IPlannedResource)null);
    //    }

    //    [TestMethod, ExpectedException(typeof(ArgumentNullException))]
    //    public void Relate_GivenNullExecutedResource_Throws()
    //    {
    //        // Arrange
    //        var db = Mock.Of<IDb4ODatabaseContext>();
    //        var repository = new Mock<ExecutedResourceRepositoryBase<IBudgetComponent>>(db) { CallBase = true };

    //        // Act
    //        repository.Object.Relate(null, Mock.Of<IPlannedResource>());
    //    }


    //    [TestMethod, ExpectedException(typeof(ArgumentNullException))]
    //    public void Unrelate_GivenNullPlannedResource_Throws()
    //    {
    //        // Arrange
    //        var db = Mock.Of<IDb4ODatabaseContext>();
    //        var repository = new Mock<ExecutedResourceRepositoryBase<IBudgetComponent>>(db) { CallBase = true };

    //        // Act
    //        repository.Object.Unrelate(Mock.Of<IExecutedResource>(), (IPlannedResource)null);
    //    }

    //    [TestMethod, ExpectedException(typeof(ArgumentNullException))]
    //    public void Unrelate_GivenNullExecutedResource_Throws()
    //    {
    //        // Arrange
    //        var db = Mock.Of<IDb4ODatabaseContext>();
    //        var repository = new Mock<ExecutedResourceRepositoryBase<IBudgetComponent>>(db) { CallBase = true };

    //        // Act
    //        repository.Object.Unrelate(null, Mock.Of<IPlannedResource>());
    //    }


    //    [TestMethod, ExpectedException(typeof(ArgumentNullException))]
    //    public void SaveReference_GivenNullPlannedResource_Throws()
    //    {
    //        // Arrange
    //        var db = Mock.Of<IDb4ODatabaseContext>();
    //        var repository = new Mock<ExecutedResourceRepositoryBase<IBudgetComponent>>(db) { CallBase = true };

    //        // Act
    //        repository.Object.SaveReference((IPlannedResource)null);
    //    }


    //    public class RepositoryStub : ExecutedResourceRepositoryBase<IBudgetComponent>
    //    {
    //        public RepositoryStub(IDb4ODatabaseContext databaseContext)
    //            : base(databaseContext)
    //        {
    //        }


    //        public new Func<IBudgetComponent, IList<IExecutedResource>> GetItemCollection
    //        {
    //            get { return base.GetItemCollection; }
    //        }
    //    }
    //}
}
