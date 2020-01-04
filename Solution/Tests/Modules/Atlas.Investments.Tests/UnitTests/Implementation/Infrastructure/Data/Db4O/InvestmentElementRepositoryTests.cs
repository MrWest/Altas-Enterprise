using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Construction;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Equipment;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Entities;
using Db4objects.Db4o;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Infrastructure.Data.Db4O
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InvestmentElementRepositoryTests : TestBase<InvestmentElementRepositoryTests.InvestmentElementRepositoryStub>
    {
        private readonly string _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdb.db");

        private Mock<IConstructionExecutedActivityRepository> _constructionExecutedActivityRepository;
        private Mock<IConstructionExecutedResourceRepository> _constructionExecutedResourceRepository;
        private Mock<IConstructionPlannedActivityRepository> _constructionPlannedActivityRepository;
        private Mock<IConstructionPlannedResourceRepository> _constructionPlannedResourceRepository;

        private Mock<IEquipmentExecutedActivityRepository> _equipmentExecutedActivityRepository;
        private Mock<IEquipmentExecutedResourceRepository> _equipmentExecutedResourceRepository;
        private Mock<IEquipmentPlannedActivityRepository> _equipmentPlannedActivityRepository;
        private Mock<IEquipmentPlannedResourceRepository> _equipmentPlannedResourceRepository;

        private Mock<IOtherExpensesExecutedActivityRepository> _otherExpensesExecutedActivityRepository;
        private Mock<IOtherExpensesExecutedResourceRepository> _otherExpensesExecutedResourceRepository;
        private Mock<IOtherExpensesPlannedActivityRepository> _otherExpensesPlannedActivityRepository;
        private Mock<IOtherExpensesPlannedResourceRepository> _otherExpensesPlannedResourceRepository;

        private Mock<IWorkCapitalExecutedActivityRepository> _workCapitalExecutedActivityRepository;
        private Mock<IWorkCapitalExecutedResourceRepository> _workCapitalExecutedResourceRepository;
        private Mock<IWorkCapitalPlannedActivityRepository> _workCapitalPlannedActivityRepository;
        private Mock<IWorkCapitalPlannedResourceRepository> _workCapitalPlannedResourceRepository;


        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IInvestmentElement>()).Returns(() => new InvestmentElementStub());

            SetupRepository<IPlannedResource, IEquipmentComponent, IEquipmentPlannedResourceRepository>(ref _equipmentPlannedResourceRepository);
            SetupRepository<IPlannedActivity, IEquipmentComponent, IEquipmentPlannedActivityRepository>(ref _equipmentPlannedActivityRepository);
            SetupRepository<IExecutedResource, IEquipmentComponent, IEquipmentExecutedResourceRepository>(ref _equipmentExecutedResourceRepository);
            SetupRepository<IExecutedActivity, IEquipmentComponent, IEquipmentExecutedActivityRepository>(ref _equipmentExecutedActivityRepository);

            SetupRepository<IPlannedResource, IConstructionComponent, IConstructionPlannedResourceRepository>(ref _constructionPlannedResourceRepository);
            SetupRepository<IPlannedActivity, IConstructionComponent, IConstructionPlannedActivityRepository>(ref _constructionPlannedActivityRepository);
            SetupRepository<IExecutedResource, IConstructionComponent, IConstructionExecutedResourceRepository>(ref _constructionExecutedResourceRepository);
            SetupRepository<IExecutedActivity, IConstructionComponent, IConstructionExecutedActivityRepository>(ref _constructionExecutedActivityRepository);

            SetupRepository<IPlannedResource, IOtherExpensesComponent, IOtherExpensesPlannedResourceRepository>(ref _otherExpensesPlannedResourceRepository);
            SetupRepository<IExecutedResource, IOtherExpensesComponent, IOtherExpensesExecutedResourceRepository>(ref _otherExpensesExecutedResourceRepository);
            SetupRepository<IPlannedActivity, IOtherExpensesComponent, IOtherExpensesPlannedActivityRepository>(ref _otherExpensesPlannedActivityRepository);
            SetupRepository<IExecutedActivity, IOtherExpensesComponent, IOtherExpensesExecutedActivityRepository>(ref _otherExpensesExecutedActivityRepository);

            SetupRepository<IPlannedResource, IWorkCapitalComponent, IWorkCapitalPlannedResourceRepository>(ref _workCapitalPlannedResourceRepository);
            SetupRepository<IExecutedResource, IWorkCapitalComponent, IWorkCapitalExecutedResourceRepository>(ref _workCapitalExecutedResourceRepository);
            SetupRepository<IPlannedActivity, IWorkCapitalComponent, IWorkCapitalPlannedActivityRepository>(ref _workCapitalPlannedActivityRepository);
            SetupRepository<IExecutedActivity, IWorkCapitalComponent, IWorkCapitalExecutedActivityRepository>(ref _workCapitalExecutedActivityRepository);

            ServiceLocatorMock.Setup(x => x.GetInstance<IBudget>()).Returns(() => new BudgetStub());
            ServiceLocatorMock.Setup(x => x.GetInstance<IEquipmentComponent>()).Returns(() => new BudgetComponentStub());
            ServiceLocatorMock.Setup(x => x.GetInstance<IConstructionComponent>()).Returns(() => new BudgetComponentStub());
            ServiceLocatorMock.Setup(x => x.GetInstance<IOtherExpensesComponent>()).Returns(() => new BudgetComponentStub());
            ServiceLocatorMock.Setup(x => x.GetInstance<IWorkCapitalComponent>()).Returns(() => new BudgetComponentStub());
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_dbPath))
                File.Delete(_dbPath);
        }

        protected override void CreateInstance()
        {
            TestObject = new InvestmentElementRepositoryStub(Mock.Of<IDb4ODatabaseContext>());
        }

        private void SetupRepository<T, TComponent, TRepository>(ref Mock<TRepository> repository)
            where T : class, IBudgetComponentItem
            where TComponent : class, IBudgetComponent
            where TRepository : class, IBudgetComponentItemRepository<T, TComponent>
        {
            repository = new Mock<TRepository>();
            repository.SetupProperty(x => x.Component);
            ServiceLocatorMock.Setup(x => x.GetInstance<TRepository>()).Returns(repository.Object);
        }


        [TestMethod]
        public void RelevanProperties_ReturnsCorrectProperties()
        {
            // Act
            var actualProperties = (string[])TestObjectInternals.GetProperty("RelevantProperties");

            // Assert
            string[] expectedProperties =
            {
                ExtractPropertyName<IInvestmentElement, string>(x => x.Name),
                ExtractPropertyName<IInvestmentElement, string>(x => x.Description),
                ExtractPropertyName<IInvestmentElement, object>(x => x.Id),
                ExtractPropertyName<IInvestmentElement, object>(x => x.Code),
                ExtractPropertyName<IInvestmentElement, object>(x => x.Location),
                ExtractPropertyName<IInvestmentElement, object>(x => x.Constructor),
                ExtractPropertyName<IInvestmentElement, object>(x => x.Objective),
                ExtractPropertyName<IInvestmentElement, object>(x => x.Scope)
            };
            CollectionAssert.AreEquivalent(expectedProperties, actualProperties);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Add_GivenNullInvestmentElement_Throws()
        {
            TestObject.Add(null);
        }

        [TestMethod]
        public void Add_GivenInvestmentElement_AddTheGivenInvestmentElementAsAnIndependentOne()
        {
            // Arrange
            var invElem1 = new InvestmentElementStub
            {
                Id = Guid.NewGuid(),
                Name = "Elem1",
                Budget = EntityTestsHelpers.CreateBudget()
            };

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem1);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new InvestmentElementRepositoryStub(db);

                var invElem2 = new InvestmentStub
                {
                    Id = Guid.NewGuid(),
                    Name = "Elem2",
                    Budget = EntityTestsHelpers.CreateBudget()
                };
                repository.Add(invElem2);
                db.Commit();
            }

            // Assert
            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                Assert.AreEqual(2, db.Query<IList<IInvestmentComponent>>().Count());

                IEnumerable<IInvestmentElement> elements = db.Query<IInvestmentElement>();

                Assert.AreEqual(2, elements.Count());
                Assert.AreEqual("Elem2", elements.Last().Name);
            }
        }

        [TestMethod]
        public void Add_GivenInvestmentElement_AddsItsBudgetAsWell()
        {
            // Arrange
            IInvestmentElement invElem = new InvestmentElementStub
            {
                Budget = EntityTestsHelpers.CreateBudget()
            };
            invElem.Budget.Id = null;
            invElem.Budget.EquipmentComponent.Id = null;
            invElem.Budget.ConstructionComponent.Id = null;
            invElem.Budget.OtherExpensesComponent.Id = null;
            invElem.Budget.WorkCapitalComponent.Id = null;


            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new InvestmentElementRepositoryStub(db);
                repository.Add(invElem);

                db.Commit();
            }


            // Assert
            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                Assert.AreSame(invElem, invElem.Budget.InvestmentElement);

                IInvestmentElement dbInvElem = db.Query<IInvestmentElement>().Single();

                Assert.IsNotNull(dbInvElem.Budget);
                Assert.IsNotNull(dbInvElem.Budget.EquipmentComponent);
                Assert.IsNotNull(dbInvElem.Budget.ConstructionComponent);
                Assert.IsNotNull(dbInvElem.Budget.OtherExpensesComponent);
                Assert.IsNotNull(dbInvElem.Budget.WorkCapitalComponent);

                Assert.IsInstanceOfType(dbInvElem.Budget.Id, typeof(Guid));
                Assert.AreNotEqual(default(Guid), dbInvElem.Budget.Id);
                Assert.IsInstanceOfType(dbInvElem.Budget.EquipmentComponent.Id, typeof(Guid));
                Assert.AreNotEqual(default(Guid), dbInvElem.Budget.EquipmentComponent.Id);
                Assert.IsInstanceOfType(dbInvElem.Budget.ConstructionComponent.Id, typeof(Guid));
                Assert.AreNotEqual(default(Guid), dbInvElem.Budget.ConstructionComponent.Id);
                Assert.IsInstanceOfType(dbInvElem.Budget.OtherExpensesComponent.Id, typeof(Guid));
                Assert.AreNotEqual(default(Guid), dbInvElem.Budget.OtherExpensesComponent.Id);
                Assert.IsInstanceOfType(dbInvElem.Budget.WorkCapitalComponent.Id, typeof(Guid));
                Assert.AreNotEqual(default(Guid).ToString(), dbInvElem.Budget.WorkCapitalComponent.Id.ToString());
            }
        }

        [TestMethod]
        public void Add_GivenInvestmentElement_NoneOfTheAddedObjectsAreKnownToTheDatabaseContextTheyAllAreClones()
        {
            // Arrange
            IInvestmentElement invElem = new InvestmentElementStub
            {
                Budget = EntityTestsHelpers.CreateBudget()
            };
            invElem.Budget.Id = null;
            invElem.Budget.EquipmentComponent.Id = null;
            invElem.Budget.ConstructionComponent.Id = null;
            invElem.Budget.OtherExpensesComponent.Id = null;
            invElem.Budget.WorkCapitalComponent.Id = null;


            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new InvestmentElementRepositoryStub(db);

                IBudget oldBudgetReference = invElem.Budget;

                repository.Add(invElem);

                IInvestmentElement addedInvElem = db.Query<IInvestmentElement>().Single();
                IBudget addedBudget = db.Query<IBudget>().Single();

                // Assert
                Assert.AreNotSame(invElem, addedInvElem);
                Assert.AreNotSame(oldBudgetReference, addedBudget);
                Assert.AreNotSame(oldBudgetReference.EquipmentComponent, addedBudget.EquipmentComponent);
                Assert.AreNotSame(oldBudgetReference.ConstructionComponent, addedBudget.ConstructionComponent);
                Assert.AreNotSame(oldBudgetReference.OtherExpensesComponent, addedBudget.OtherExpensesComponent);
                Assert.AreNotSame(oldBudgetReference.WorkCapitalComponent, addedBudget.WorkCapitalComponent);

                Assert.AreNotSame(oldBudgetReference.EquipmentComponent.PlannedResources, invElem.Budget.EquipmentComponent.PlannedResources);
                Assert.AreNotSame(oldBudgetReference.EquipmentComponent.PlannedActivities, invElem.Budget.EquipmentComponent.PlannedActivities);
                Assert.AreNotSame(oldBudgetReference.EquipmentComponent.ExecutedResources, invElem.Budget.EquipmentComponent.ExecutedResources);
                Assert.AreNotSame(oldBudgetReference.EquipmentComponent.ExecutedActivities, invElem.Budget.EquipmentComponent.ExecutedActivities);

                Assert.AreNotSame(oldBudgetReference.ConstructionComponent.PlannedResources, invElem.Budget.ConstructionComponent.PlannedResources);
                Assert.AreNotSame(oldBudgetReference.ConstructionComponent.PlannedActivities, invElem.Budget.ConstructionComponent.PlannedActivities);
                Assert.AreNotSame(oldBudgetReference.ConstructionComponent.ExecutedResources, invElem.Budget.ConstructionComponent.ExecutedResources);
                Assert.AreNotSame(oldBudgetReference.ConstructionComponent.ExecutedActivities, invElem.Budget.ConstructionComponent.ExecutedActivities);

                Assert.AreNotSame(oldBudgetReference.OtherExpensesComponent.PlannedResources, invElem.Budget.OtherExpensesComponent.PlannedResources);
                Assert.AreNotSame(oldBudgetReference.OtherExpensesComponent.PlannedActivities, invElem.Budget.OtherExpensesComponent.PlannedActivities);
                Assert.AreNotSame(oldBudgetReference.OtherExpensesComponent.ExecutedResources, invElem.Budget.OtherExpensesComponent.ExecutedResources);
                Assert.AreNotSame(oldBudgetReference.OtherExpensesComponent.ExecutedActivities, invElem.Budget.OtherExpensesComponent.ExecutedActivities);

                Assert.AreNotSame(oldBudgetReference.WorkCapitalComponent.PlannedResources, invElem.Budget.WorkCapitalComponent.PlannedResources);
                Assert.AreNotSame(oldBudgetReference.WorkCapitalComponent.PlannedActivities, invElem.Budget.WorkCapitalComponent.PlannedActivities);
                Assert.AreNotSame(oldBudgetReference.WorkCapitalComponent.ExecutedResources, invElem.Budget.WorkCapitalComponent.ExecutedResources);
                Assert.AreNotSame(oldBudgetReference.WorkCapitalComponent.ExecutedActivities, invElem.Budget.WorkCapitalComponent.ExecutedActivities);
            }
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void Add_GivenInvestmentElementWithoutBudget_Throws()
        {
            TestObject.Add(new InvestmentElementStub { Budget = null });
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Update_GivenNullInvestmentElement_Throws()
        {
            TestObject.Update(null);
        }

        [TestMethod]
        public void Update_GivenInvestmentElement_SavesCorrectlyTheirData()
        {
            // Arrange
            IInvestmentElement invElem = new InvestmentElementStub
            {
                Id = Guid.NewGuid(),
                Name = "Elem1",
                Description = "desc",
                Budget = EntityTestsHelpers.CreateBudget()
            };
            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }


            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new InvestmentElementRepositoryStub(db);

                invElem.Name = "Elem 2";
                invElem.Description = "desc 123";

                repository.Update(invElem);
                db.Commit();
            }


            // Assert
            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                Assert.AreEqual(1, db.Query<IList<IInvestmentComponent>>().Count());

                invElem = db.Query<IInvestmentElement>().Single();

                Assert.AreEqual("Elem 2", invElem.Name);
                Assert.AreEqual("desc 123", invElem.Description);
            }
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void Update_GivenInvestmentElementWithoutBudget_Throws()
        {
            TestObject.Update(new InvestmentElementStub { Budget = null });
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Delete_GivenNullInvestmentElement_Throws()
        {
            TestObject.Delete(null);
        }

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemovesItFromDatabaseAndBreaksItsReferences()
        {
            #region Arrange

            IInvestmentElement invElem = new InvestmentElementStub
            {
                Id = Guid.NewGuid(),
                Name = "InvestmentElement",
                Budget = EntityTestsHelpers.CreateBudget()
            };
            invElem.Budget.InvestmentElement = invElem;

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            #endregion

            #region Act

            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new InvestmentElementRepositoryStub(db);
                repository.Delete(invElem);
                db.Commit();
            }

            #endregion

            #region Assert

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                Assert.IsFalse(db.Query<IInvestmentElement>().Any());
                Assert.IsFalse(db.Query<IEquipmentComponent>().Any());
                Assert.IsFalse(db.Query<IConstructionComponent>().Any());
                Assert.IsFalse(db.Query<IOtherExpensesComponent>().Any());
                Assert.IsFalse(db.Query<IWorkCapitalComponent>().Any());
                Assert.IsFalse(db.Query<IList<IPlannedResource>>().Any());
                Assert.IsFalse(db.Query<IList<IExecutedResource>>().Any());
                Assert.IsFalse(db.Query<IList<IPlannedActivity>>().Any());
                Assert.IsFalse(db.Query<IList<IExecutedActivity>>().Any());
            }

            #endregion
        }

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheEquipmentPlannedResourcesFromTheDeleteInvestmentElements()
        {
            // Arrange
            IBudget budget = EntityTestsHelpers.CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepositoryStub(database.Object);
            repository.Delete(invElem);

            // Assert
            _equipmentPlannedResourceRepository.VerifySet(x => x.Component = budget.EquipmentComponent);
            _equipmentPlannedResourceRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.EquipmentComponent.PlannedResources));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.EquipmentComponent));
        }

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheEquipmentPlannedActivitiesFromTheDeleteInvestmentElements()
        {
            // Arrange
            IBudget budget = EntityTestsHelpers.CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepositoryStub(database.Object);
            repository.Delete(invElem);

            // Assert
            _equipmentPlannedActivityRepository.VerifySet(x => x.Component = budget.EquipmentComponent);
            _equipmentPlannedActivityRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.EquipmentComponent.PlannedActivities));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.EquipmentComponent));
        }

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheEquipmentExecutedResourcesFromTheDeleteInvestmentElements()
        {
            // Arrange
            IBudget budget = EntityTestsHelpers.CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepositoryStub(database.Object);
            repository.Delete(invElem);

            // Assert
            _equipmentExecutedResourceRepository.VerifySet(x => x.Component = budget.EquipmentComponent);
            _equipmentExecutedResourceRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.EquipmentComponent.ExecutedResources));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.EquipmentComponent));
        }

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheEquipmentExecutedActivitiesFromTheDeleteInvestmentElements()
        {
            // Arrange
            IBudget budget = EntityTestsHelpers.CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepositoryStub(database.Object);
            repository.Delete(invElem);

            // Assert
            _equipmentExecutedActivityRepository.VerifySet(x => x.Component = budget.EquipmentComponent);
            _equipmentExecutedActivityRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.EquipmentComponent.ExecutedActivities));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.EquipmentComponent));
        }

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheConstructionPlannedResourcesFromTheDeleteInvestmentElements()
        {
            // Arrange
            IBudget budget = EntityTestsHelpers.CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepositoryStub(database.Object);
            repository.Delete(invElem);

            // Assert
            _constructionPlannedResourceRepository.VerifySet(x => x.Component = budget.ConstructionComponent);
            _constructionPlannedResourceRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.ConstructionComponent.PlannedResources));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.ConstructionComponent));
        }

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheConstructionPlannedActivitiesFromTheDeleteInvestmentElements()
        {
            // Arrange
            IBudget budget = EntityTestsHelpers.CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepositoryStub(database.Object);
            repository.Delete(invElem);

            // Assert
            _constructionPlannedActivityRepository.VerifySet(x => x.Component = budget.ConstructionComponent);
            _constructionPlannedActivityRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.ConstructionComponent.PlannedActivities));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.ConstructionComponent));
        }

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheConstructionExecutedResourcesFromTheDeleteInvestmentElements()
        {
            // Arrange
            IBudget budget = EntityTestsHelpers.CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepositoryStub(database.Object);
            repository.Delete(invElem);

            // Assert
            _constructionExecutedResourceRepository.VerifySet(x => x.Component = budget.ConstructionComponent);
            _constructionExecutedResourceRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.ConstructionComponent.ExecutedResources));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.ConstructionComponent));
        }

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheConstructionExecutedActivitiesFromTheDeleteInvestmentElements()
        {
            // Arrange
            IBudget budget = EntityTestsHelpers.CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepositoryStub(database.Object);
            repository.Delete(invElem);

            // Assert
            _constructionExecutedActivityRepository.VerifySet(x => x.Component = budget.ConstructionComponent);
            _constructionExecutedActivityRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.ConstructionComponent.ExecutedActivities));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.ConstructionComponent));
        }

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheOtherExpensesPlannedResourcesFromTheDeleteInvestmentElements()
        {
            // Arrange
            IBudget budget = EntityTestsHelpers.CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepositoryStub(database.Object);
            repository.Delete(invElem);

            // Assert
            _otherExpensesPlannedResourceRepository.VerifySet(x => x.Component = budget.OtherExpensesComponent);
            _otherExpensesPlannedResourceRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.OtherExpensesComponent.PlannedResources));
            database.Verify(x => x.Delete((object)budget.OtherExpensesComponent.PlannedActivities));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.OtherExpensesComponent));
        }

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheOtherExpensesExecutedResourcesFromTheDeleteInvestmentElements()
        {
            // Arrange
            IBudget budget = EntityTestsHelpers.CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepositoryStub(database.Object);
            repository.Delete(invElem);

            // Assert
            _otherExpensesExecutedResourceRepository.VerifySet(x => x.Component = budget.OtherExpensesComponent);
            _otherExpensesExecutedResourceRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.OtherExpensesComponent.ExecutedResources));
            database.Verify(x => x.Delete((object)budget.OtherExpensesComponent.ExecutedActivities));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.OtherExpensesComponent));
        }

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheOtherExpensesPlannedActivitiesFromTheDeleteInvestmentElements()
        {
            // Arrange
            IBudget budget = EntityTestsHelpers.CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepositoryStub(database.Object);
            repository.Delete(invElem);

            // Assert
            _otherExpensesPlannedActivityRepository.VerifySet(x => x.Component = budget.OtherExpensesComponent);
            _otherExpensesPlannedActivityRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.OtherExpensesComponent.PlannedActivities));
            database.Verify(x => x.Delete((object)budget.OtherExpensesComponent.PlannedActivities));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.OtherExpensesComponent));
        }

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheOtherExpensesExecutedActivitiesFromTheDeleteInvestmentElements()
        {
            // Arrange
            IBudget budget = EntityTestsHelpers.CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepositoryStub(database.Object);
            repository.Delete(invElem);

            // Assert
            _otherExpensesExecutedActivityRepository.VerifySet(x => x.Component = budget.OtherExpensesComponent);
            _otherExpensesExecutedActivityRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.OtherExpensesComponent.ExecutedActivities));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.OtherExpensesComponent));
        }

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheWorkCapitalPlannedResourcesFromTheDeleteInvestmentElements()
        {
            // Arrange
            IBudget budget = EntityTestsHelpers.CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepositoryStub(database.Object);
            repository.Delete(invElem);

            // Assert
            _workCapitalPlannedResourceRepository.VerifySet(x => x.Component = budget.WorkCapitalComponent);
            _workCapitalPlannedResourceRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.WorkCapitalComponent.PlannedResources));
            database.Verify(x => x.Delete((object)budget.WorkCapitalComponent.PlannedActivities));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.WorkCapitalComponent));
        }

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheWorkCapitalExecutedResourcesFromTheDeleteInvestmentElements()
        {
            // Arrange
            IBudget budget = EntityTestsHelpers.CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepositoryStub(database.Object);
            repository.Delete(invElem);

            // Assert
            _workCapitalExecutedResourceRepository.VerifySet(x => x.Component = budget.WorkCapitalComponent);
            _workCapitalExecutedResourceRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.WorkCapitalComponent.ExecutedResources));
            database.Verify(x => x.Delete((object)budget.WorkCapitalComponent.ExecutedActivities));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.WorkCapitalComponent));
        }

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheWorkCapitalPlannedActivitiesFromTheDeleteInvestmentElements()
        {
            // Arrange
            IBudget budget = EntityTestsHelpers.CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepositoryStub(database.Object);
            repository.Delete(invElem);

            // Assert
            _workCapitalPlannedActivityRepository.VerifySet(x => x.Component = budget.WorkCapitalComponent);
            _workCapitalPlannedActivityRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.WorkCapitalComponent.PlannedActivities));
            database.Verify(x => x.Delete((object)budget.WorkCapitalComponent.PlannedActivities));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.WorkCapitalComponent));
        }

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheWorkCapitalExecutedActivitiesFromTheDeleteInvestmentElements()
        {
            // Arrange
            IBudget budget = EntityTestsHelpers.CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepositoryStub(database.Object);
            repository.Delete(invElem);

            // Assert
            _workCapitalExecutedActivityRepository.VerifySet(x => x.Component = budget.WorkCapitalComponent);
            _workCapitalExecutedActivityRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.WorkCapitalComponent.ExecutedActivities));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.WorkCapitalComponent));
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Relate_GivenNullFirstInvestmentElementButNotTheSecond_Throws()
        {
            TestObject.Relate(null, Mock.Of<IBudget>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Relate_GivenNullSecondInvestmentElementButNotTheFirst_Throws()
        {
            TestObject.Relate(Mock.Of<IInvestmentElement>(), null);
        }

        [TestMethod]
        public void Relate_GivenBudgetAndInvestmentElement_RelatesThem()
        {
            // Arrange
            var budget = new Mock<IBudget>();
            var investmentElement = new Mock<IInvestmentElement>();
            var repository = new InvestmentElementRepositoryStub(Mock.Of<IDb4ODatabaseContext>());

            // Act
            repository.Relate(investmentElement.Object, budget.Object);

            // Assert
            budget.VerifySet(x => x.InvestmentElement = investmentElement.Object);
            investmentElement.VerifySet(x => x.Budget = budget.Object);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Unrelate_GivenNullInvestmentElement_Throws()
        {
            TestObject.Unrelate(null, Mock.Of<IBudget>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Unrelate_GivenNullBudget_Throws()
        {
            TestObject.Unrelate(Mock.Of<IInvestmentElement>(), null);
        }

        [TestMethod]
        public void Unrelate_GivenBudgetAndInvestmentElement_BreaksRelationshipBetweenThem()
        {
            // Arrange
            var budget = new Mock<IBudget>();
            budget.Setup(x => x.EquipmentComponent).Returns(Mock.Of<IEquipmentComponent>);
            budget.Setup(x => x.ConstructionComponent).Returns(Mock.Of<IConstructionComponent>);
            budget.Setup(x => x.OtherExpensesComponent).Returns(Mock.Of<IOtherExpensesComponent>);
            budget.Setup(x => x.WorkCapitalComponent).Returns(Mock.Of<IWorkCapitalComponent>);
            var investmentElement = new Mock<IInvestmentElement>();
            var repository = new InvestmentElementRepositoryStub(Mock.Of<IDb4ODatabaseContext>());

            // Act
            repository.Unrelate(investmentElement.Object, budget.Object);

            // Assert
            budget.VerifySet(x => x.InvestmentElement = null);
            investmentElement.VerifySet(x => x.Budget = null);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void SaveReferences_GivenNullInvestmentElement_Throws()
        {
            TestObject.SaveReference((IInvestmentElement)null);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Clone_GivenNullInvestmentElement_Throws()
        {
            new InvestmentElementRepositoryStub(Mock.Of<IDb4ODatabaseContext>()).Clone(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void Clone_GivenNullBudgetInProvidedInvestmentElement_Throws()
        {
            new InvestmentElementRepositoryStub(Mock.Of<IDb4ODatabaseContext>()).Clone(Mock.Of<IInvestmentElement>());
        }

        [TestMethod]
        public void Clone_GivenInvestmentElement_ClonesIt()
        {
            // Arrange
            IBudget originalBudget = EntityTestsHelpers.CreateBudget();
            var investmentElement = Mock.Of<IInvestmentElement>(x => x.Budget == originalBudget);

            // Act
            var clone = (IInvestmentElement)TestObjectInternals.Invoke("Clone", investmentElement);

            IBudget budget = clone.Budget;

            Assert.AreNotSame(investmentElement, clone);
            Assert.AreNotSame(originalBudget, budget);
            Assert.AreNotSame(originalBudget.EquipmentComponent, budget.EquipmentComponent);
            Assert.AreNotSame(originalBudget.ConstructionComponent, budget.ConstructionComponent);
            Assert.AreNotSame(originalBudget.OtherExpensesComponent, budget.OtherExpensesComponent);
            Assert.AreNotSame(originalBudget.WorkCapitalComponent, budget.WorkCapitalComponent);

            Assert.AreNotSame(originalBudget.EquipmentComponent.PlannedResources, budget.EquipmentComponent.PlannedResources);
            Assert.AreNotSame(originalBudget.EquipmentComponent.PlannedActivities, budget.EquipmentComponent.PlannedActivities);
            Assert.AreNotSame(originalBudget.EquipmentComponent.ExecutedResources, budget.EquipmentComponent.ExecutedResources);
            Assert.AreNotSame(originalBudget.EquipmentComponent.ExecutedActivities, budget.EquipmentComponent.ExecutedActivities);

            Assert.AreNotSame(originalBudget.ConstructionComponent.PlannedResources, budget.ConstructionComponent.PlannedResources);
            Assert.AreNotSame(originalBudget.ConstructionComponent.PlannedActivities, budget.ConstructionComponent.PlannedActivities);
            Assert.AreNotSame(originalBudget.ConstructionComponent.ExecutedResources, budget.ConstructionComponent.ExecutedResources);
            Assert.AreNotSame(originalBudget.ConstructionComponent.ExecutedActivities, budget.ConstructionComponent.ExecutedActivities);

            Assert.AreNotSame(originalBudget.OtherExpensesComponent.PlannedResources, budget.OtherExpensesComponent.PlannedResources);
            Assert.AreNotSame(originalBudget.OtherExpensesComponent.PlannedActivities, budget.OtherExpensesComponent.PlannedActivities);
            Assert.AreNotSame(originalBudget.OtherExpensesComponent.ExecutedResources, budget.OtherExpensesComponent.ExecutedResources);
            Assert.AreNotSame(originalBudget.OtherExpensesComponent.ExecutedActivities, budget.OtherExpensesComponent.ExecutedActivities);

            Assert.AreNotSame(originalBudget.WorkCapitalComponent.PlannedResources, budget.WorkCapitalComponent.PlannedResources);
            Assert.AreNotSame(originalBudget.WorkCapitalComponent.PlannedActivities, budget.WorkCapitalComponent.PlannedActivities);
            Assert.AreNotSame(originalBudget.WorkCapitalComponent.ExecutedResources, budget.WorkCapitalComponent.ExecutedResources);
            Assert.AreNotSame(originalBudget.WorkCapitalComponent.ExecutedActivities, budget.WorkCapitalComponent.ExecutedActivities);
        }


        public class InvestmentElementRepositoryStub : InvestmentElementRepositoryBase<IInvestmentElement>
        {
            public InvestmentElementRepositoryStub(IDb4ODatabaseContext databaseContext)
                : base(databaseContext)
            {
            }


            public new IInvestmentElement Clone(IInvestmentElement investmentElement)
            {
                return base.Clone(investmentElement);
            }
        }
    }
}