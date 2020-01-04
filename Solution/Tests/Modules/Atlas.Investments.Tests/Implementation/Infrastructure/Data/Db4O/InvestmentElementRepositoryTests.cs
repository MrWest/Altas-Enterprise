using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Construction;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Equipment;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Tests.Implementation.Domain.Entities;
using Db4objects.Db4o;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Infrastructure.Data.Db4O
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InvestmentElementRepositoryTests : TestBase<InvestmentElementRepository>
    {
        private readonly string _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdb.db");

        //private Mock<IEquipmentPlannedResourceRepository> _equipmentPlannedResourceRepository;
        private Mock<IPlannedSubSpecialityHolderRepository> _equipmentPlannedActivityRepository;
        //private Mock<IEquipmentExecutedResourceRepository> _equipmentExecutedResourceRepository;
        private Mock<IExecutedSubSpecialityHolderRepository> _equipmentExecutedActivityRepository;

        //private Mock<IConstructionPlannedResourceRepository> _constructionPlannedResourceRepository;
        private Mock<IPlannedSubSpecialityHolderRepository> _constructionPlannedActivityRepository;
        //private Mock<IConstructionExecutedResourceRepository> _constructionExecutedResourceRepository;
        private Mock<IExecutedSubSpecialityHolderRepository> _constructionExecutedActivityRepository;

        //private Mock<IOtherExpensesPlannedResourceRepository> _otherExpensesPlannedResourceRepository;
        //private Mock<IOtherExpensesExecutedResourceRepository> _otherExpensesExecutedResourceRepository;
        private Mock<IPlannedSubSpecialityHolderRepository> _otherExpensesPlannedActivityRepository;
        private Mock<IExecutedSubSpecialityHolderRepository> _otherExpensesExecutedActivityRepository;

        //private Mock<IWorkCapitalPlannedResourceRepository> _workCapitalPlannedResourceRepository;
        //private Mock<IWorkCapitalExecutedResourceRepository> _workCapitalExecutedResourceRepository;
        private Mock<IPlannedSubSpecialityHolderRepository> _workCapitalPlannedActivityRepository;
        private Mock<IExecutedSubSpecialityHolderRepository> _workCapitalExecutedActivityRepository;


        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IInvestmentElement>()).Returns(() => new InvestmentElementStub());

            //SetupRepository<IPlannedResource, IEquipmentComponent, IEquipmentPlannedResourceRepository>(ref _equipmentPlannedResourceRepository);
            SetupRepository<IPlannedSubSpecialityHolder, IEquipmentComponent, IPlannedSubSpecialityHolderRepository>(ref _equipmentPlannedActivityRepository);
            //SetupRepository<IExecutedResource, IEquipmentComponent, IEquipmentExecutedResourceRepository>(ref _equipmentExecutedResourceRepository);
            SetupRepository<IExecutedSubSpecialityHolder, IEquipmentComponent, IExecutedSubSpecialityHolderRepository>(ref _equipmentExecutedActivityRepository);
            
            //SetupRepository<IPlannedResource, IConstructionComponent, IConstructionPlannedResourceRepository>(ref _constructionPlannedResourceRepository);
            SetupRepository<IPlannedSubSpecialityHolder, IConstructionComponent, IPlannedSubSpecialityHolderRepository>(ref _constructionPlannedActivityRepository);
            //SetupRepository<IExecutedResource, IConstructionComponent, IConstructionExecutedResourceRepository>(ref _constructionExecutedResourceRepository);
            SetupRepository<IExecutedSubSpecialityHolder, IConstructionComponent, IExecutedSubSpecialityHolderRepository>(ref _constructionExecutedActivityRepository);

            //SetupRepository<IPlannedResource, IOtherExpensesComponent, IOtherExpensesPlannedResourceRepository>(ref _otherExpensesPlannedResourceRepository);
            //SetupRepository<IExecutedResource, IOtherExpensesComponent, IOtherExpensesExecutedResourceRepository>(ref _otherExpensesExecutedResourceRepository);
            SetupRepository<IPlannedSubSpecialityHolder, IOtherExpensesComponent, IPlannedSubSpecialityHolderRepository>(ref _otherExpensesPlannedActivityRepository);
            SetupRepository<IExecutedSubSpecialityHolder, IOtherExpensesComponent, IExecutedSubSpecialityHolderRepository>(ref _otherExpensesExecutedActivityRepository);

            //SetupRepository<IPlannedResource, IWorkCapitalComponent, IWorkCapitalPlannedResourceRepository>(ref _workCapitalPlannedResourceRepository);
            //SetupRepository<IExecutedResource, IWorkCapitalComponent, IWorkCapitalExecutedResourceRepository>(ref _workCapitalExecutedResourceRepository);
            SetupRepository<IPlannedSubSpecialityHolder, IWorkCapitalComponent, IPlannedSubSpecialityHolderRepository>(ref _workCapitalPlannedActivityRepository);
            SetupRepository<IExecutedSubSpecialityHolder, IWorkCapitalComponent, IExecutedSubSpecialityHolderRepository>(ref _workCapitalExecutedActivityRepository);

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
            TestObject = new InvestmentElementRepository(Mock.Of<IDb4ODatabaseContext>());
        }

        private void SetupRepository<THolder, TComponent, TRepository>(ref Mock<TRepository> repository)
            where THolder : class, ISubSpecialityHolder
            where TComponent : class, IBudgetComponent
            where TRepository : class, ISubSpecialityHolderRepository<THolder>
        {
            repository = new Mock<TRepository>();
            repository.SetupProperty(x => x.BudgetComponent);
            ServiceLocatorMock.Setup(x => x.GetInstance<TRepository>()).Returns(repository.Object);
        }


        [TestMethod]
        public void RelevanProperties_ReturnsCorrectProperties()
        {
            // Act
            var actualProperties = (string[])TestObjectInternals.GetProperty("RelevantProperties");

            // Assert
            string[] expectedProperties = {
                ExtractPropertyName<IInvestmentElement, string>(x => x.Name),
                ExtractPropertyName<IInvestmentElement, string>(x => x.Description),
                ExtractPropertyName<IInvestmentElement, object>(x => x.Id),
            };
            CollectionAssert.AreEquivalent(expectedProperties, actualProperties);
        }


        [TestMethod]
        public void Entities_IfParentIsNotDefined_ReturnsAllTheIndependentInvestmentElements()
        {
            #region Arrange

            var invElem1 = new InvestmentElementStub
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Elem1",
                Description = "Elem desc 1",
                Budget = CreateBudget()
            };
            var invElem2 = new InvestmentElementStub
            {
                Id = Guid.NewGuid().ToString(),
                Parent = invElem1,
                Name = "Elem2",
                Description = "Elem desc 2",
                Budget = CreateBudget()
            };
            invElem1.Elements.Add(invElem2);
            var invElem3 = new InvestmentElementStub
            {
                Id = Guid.NewGuid().ToString(),
                Parent = invElem1,
                Name = "Elem3",
                Description = "Elem desc 3",
                Budget = CreateBudget()
            };
            invElem1.Elements.Add(invElem3);
            var invElem4 = new InvestmentElementStub
            {
                Id = Guid.NewGuid().ToString(),
                Parent = invElem2,
                Name = "Elem4",
                Description = "Elem desc 4",
                Budget = CreateBudget()
            };
            invElem2.Elements.Add(invElem4);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem1);
                db.Store(invElem2);
                db.Store(invElem3);
                db.Store(invElem4);
                db.Commit();
            }

            #endregion

            #region Act

            IEnumerable<IInvestmentElement> actualInvestmentElements;
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new InvestmentElementRepository(db);
                actualInvestmentElements = repository.Entities;
            }

            #endregion

            // Assert
            Assert.AreEqual(1, actualInvestmentElements.Count());
            Assert.AreEqual("Elem1", actualInvestmentElements.Single().Name);
        }

        [TestMethod]
        public void Entities_IfParentIsDefined_ReturnsAllTheInvestmentElementsOfThatParentOne()
        {
            #region Arrange

            var invElem1 = new InvestmentElementStub
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Elem1",
                Description = "Elem desc 1",
                Budget = CreateBudget()
            };
            var invElem2 = new InvestmentElementStub
            {
                Id = Guid.NewGuid().ToString(),
                Parent = invElem1,
                Name = "Elem2",
                Description = "Elem desc 2",
                Budget = CreateBudget()
            };
            invElem1.Elements.Add(invElem2);
            var invElem3 = new InvestmentElementStub
            {
                Id = Guid.NewGuid().ToString(),
                Parent = invElem1,
                Name = "Elem3",
                Description = "Elem desc 3",
                Budget = CreateBudget()
            };
            invElem1.Elements.Add(invElem3);
            var invElem4 = new InvestmentElementStub
            {
                Id = Guid.NewGuid().ToString(),
                Parent = invElem2,
                Name = "Elem4",
                Description = "Elem desc 4",
                Budget = CreateBudget()
            };
            invElem2.Elements.Add(invElem4);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem1);
                db.Store(invElem2);
                db.Store(invElem3);
                db.Store(invElem4);
                db.Commit();
            }

            #endregion

            #region Act

            IEnumerable<IInvestmentElement> actualInvestmentElements;
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new InvestmentElementRepository(db) { Parent = invElem1 };
                actualInvestmentElements = repository.Entities;
            }

            #endregion

            // Assert
            Assert.AreEqual(2, actualInvestmentElements.Count());
            Assert.AreEqual("Elem2", actualInvestmentElements.First().Name);
            Assert.AreEqual("Elem3", actualInvestmentElements.Last().Name);
        }

        [TestMethod]
        public void Entities_IfParentIsDefined_ReturnsAlwaysClones()
        {
            #region Arrange

            var invElem1 = new InvestmentElementStub
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Elem1",
                Description = "Elem desc 1",
                Budget = CreateBudget()
            };
            var invElem2 = new InvestmentElementStub
            {
                Id = Guid.NewGuid().ToString(),
                Parent = invElem1,
                Name = "Elem2",
                Description = "Elem desc 2",
                Budget = CreateBudget()
            };
            invElem1.Elements.Add(invElem2);
            var invElem3 = new InvestmentElementStub
            {
                Id = Guid.NewGuid().ToString(),
                Parent = invElem1,
                Name = "Elem3",
                Description = "Elem desc 3",
                Budget = CreateBudget()
            };
            invElem1.Elements.Add(invElem3);
            var invElem4 = new InvestmentElementStub
            {
                Id = Guid.NewGuid().ToString(),
                Parent = invElem2,
                Name = "Elem4",
                Description = "Elem desc 4",
                Budget = CreateBudget()
            };
            invElem2.Elements.Add(invElem4);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem1);
                db.Store(invElem2);
                db.Store(invElem3);
                db.Store(invElem4);
                db.Commit();
            }

            #endregion

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new InvestmentElementRepository(db) { Parent = invElem1 };
                IEnumerable<IInvestmentElement> actualInvestmentElements = repository.Entities;

                // Assert
                IInvestmentElement[] dbInvestmentElements = db.Query<IInvestmentElement>().ToArray();
                foreach (IInvestmentElement invElem in actualInvestmentElements.ToArray())
                    foreach (IInvestmentElement dbInvElem in dbInvestmentElements)
                    {
                        Assert.AreNotSame(invElem, dbInvElem);

                        Assert.AreNotSame(invElem.Budget, dbInvElem.Budget);

                        Assert.AreNotSame(invElem.Budget.EquipmentComponent, dbInvElem.Budget.EquipmentComponent);
                        //Assert.AreNotSame(invElem.Budget.EquipmentComponent.PlannedActivities, dbInvElem.Budget.EquipmentComponent.PlannedResources);
                        Assert.AreNotSame(invElem.Budget.EquipmentComponent.PlannedActivities, dbInvElem.Budget.EquipmentComponent.PlannedActivities);
                        //Assert.AreNotSame(invElem.Budget.EquipmentComponent.ExecutedResources, dbInvElem.Budget.EquipmentComponent.ExecutedResources);
                        Assert.AreNotSame(invElem.Budget.EquipmentComponent.ExecutedActivities, dbInvElem.Budget.EquipmentComponent.ExecutedActivities);

                        Assert.AreNotSame(invElem.Budget.ConstructionComponent, dbInvElem.Budget.ConstructionComponent);
                        //Assert.AreNotSame(invElem.Budget.ConstructionComponent.PlannedResources, dbInvElem.Budget.ConstructionComponent.PlannedResources);
                        Assert.AreNotSame(invElem.Budget.ConstructionComponent.PlannedActivities, dbInvElem.Budget.ConstructionComponent.PlannedActivities);
                        //Assert.AreNotSame(invElem.Budget.ConstructionComponent.ExecutedResources, dbInvElem.Budget.ConstructionComponent.ExecutedResources);
                        Assert.AreNotSame(invElem.Budget.ConstructionComponent.ExecutedActivities, dbInvElem.Budget.ConstructionComponent.ExecutedActivities);

                        Assert.AreNotSame(invElem.Budget.OtherExpensesComponent, dbInvElem.Budget.OtherExpensesComponent);
                        //Assert.AreNotSame(invElem.Budget.OtherExpensesComponent.PlannedResources, dbInvElem.Budget.OtherExpensesComponent.PlannedResources);
                        Assert.AreNotSame(invElem.Budget.OtherExpensesComponent.PlannedActivities, dbInvElem.Budget.OtherExpensesComponent.PlannedActivities);
                        //Assert.AreNotSame(invElem.Budget.OtherExpensesComponent.ExecutedResources, dbInvElem.Budget.OtherExpensesComponent.ExecutedResources);
                        Assert.AreNotSame(invElem.Budget.OtherExpensesComponent.ExecutedActivities, dbInvElem.Budget.OtherExpensesComponent.ExecutedActivities);

                        Assert.AreNotSame(invElem.Budget.WorkCapitalComponent, dbInvElem.Budget.WorkCapitalComponent);
                        //Assert.AreNotSame(invElem.Budget.WorkCapitalComponent.PlannedResources, dbInvElem.Budget.WorkCapitalComponent.PlannedResources);
                        Assert.AreNotSame(invElem.Budget.WorkCapitalComponent.PlannedActivities, dbInvElem.Budget.WorkCapitalComponent.PlannedActivities);
                        //Assert.AreNotSame(invElem.Budget.WorkCapitalComponent.ExecutedResources, dbInvElem.Budget.WorkCapitalComponent.ExecutedResources);
                        Assert.AreNotSame(invElem.Budget.WorkCapitalComponent.ExecutedActivities, dbInvElem.Budget.WorkCapitalComponent.ExecutedActivities);
                    }
            }
        }

        [TestMethod]
        public void Entities_IfInvestmentElementsInTheDatabase_AlwaysReturnsArray()
        {
            // Arrange
            var databaseContext = new Mock<IDb4ODatabaseContext>();
            databaseContext
                .Setup(x => x.Query(It.IsAny<Predicate<IInvestmentElement>>()))
                .Returns(new[] { Mock.Of<IInvestmentElement>(x => x.Budget == CreateBudget()) });
            TestObject = new InvestmentElementRepository(databaseContext.Object);

            // Act
            IEnumerable<IInvestmentElement> actualInvestmentElements = TestObject.Entities;

            // Assert
            Assert.IsInstanceOfType(actualInvestmentElements, typeof(Array));
        }

        [TestMethod]
        public void Entities_IfNoInvestmentElementsInTheDatabase_ReturnsEmptyArray()
        {
            // Act
            IEnumerable<IInvestmentElement> actualInvestmentElements = TestObject.Entities;

            // Assert
            Assert.IsFalse(actualInvestmentElements.Any());
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Add_GivenNullInvestmentElement_Throws()
        {
            TestObject.Add(null);
        }

        [TestMethod]
        public void Add_GivenInvestmentElement_ThereIsNoParentDefinedInTheRepository_AddTheGivenInvestmentElementAsAnIndependentOne()
        {
            // Arrange
            var invElem1 = new InvestmentElementStub
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Elem1",
                Budget = CreateBudget()
            };

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem1);
                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new InvestmentElementRepository(db);

                var invElem2 = new InvestmentElementStub
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Elem2",
                    Budget = CreateBudget()
                };
                repository.Add(invElem2);
                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                Assert.AreEqual(2, db.Query<IList<IInvestmentElement>>().Count());

                IEnumerable<IInvestmentElement> elements = db.Query<IInvestmentElement>();

                Assert.AreEqual(2, elements.Count());
                Assert.AreEqual("Elem2", elements.Last().Name);
            }
        }

        [TestMethod]
        public void Add_GivenInvestmentElement_ThereIsAParentDefinedInTheRepository_AddTheGivenInvestmentElementAsAChildOfSuchParent()
        {
            // Arrange
            var invElem1 = new InvestmentElementStub
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Elem1",
                Budget = CreateBudget()
            };

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem1);
                db.Commit();
            }

            // Act
            IInvestmentComponent invElem2;
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new InvestmentElementRepository(db) { Parent = invElem1 };

                invElem2 = new InvestmentComponentStub()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Elem2",
                    Budget = CreateBudget()
                };

                repository.Add(invElem2);
                db.Commit();
            }

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                IEnumerable<IInvestmentElement> elements = db.Query<IInvestmentElement>();
                Assert.AreEqual(2, elements.Count());

                IInvestmentElement elem1 = db.Query<IInvestmentElement>(x => x.Name == "Elem1").Single();
                IInvestmentComponent elem2 = db.Query<IInvestmentComponent>(x => x.Name == "Elem2").Single();
                Assert.AreSame(elem1, elem2.Parent);
                CollectionAssert.Contains(elem1.Elements.ToArray(), elem2);

                CollectionAssert.Contains(invElem1.Elements.ToArray(), invElem2);
                Assert.AreSame(invElem1, invElem2.Parent);
            }
        }

        [TestMethod]
        public void Add_GivenInvestmentElement_AddsItsBudgetAsWell()
        {
            // Arrange
            IInvestmentElement invElem = new InvestmentElementStub
            {
                Budget = CreateBudget()
            };
            invElem.Budget.Id = null;
            invElem.Budget.EquipmentComponent.Id = null;
            invElem.Budget.ConstructionComponent.Id = null;
            invElem.Budget.OtherExpensesComponent.Id = null;
            invElem.Budget.WorkCapitalComponent.Id = null;


            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new InvestmentElementRepository(db);
                repository.Add(invElem);

                db.Commit();
            }


            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                Assert.AreSame(invElem, invElem.Budget.InvestmentElement);

                var dbInvElem = db.Query<IInvestmentElement>().Single();

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
                Budget = CreateBudget()
            };
            invElem.Budget.Id = null;
            invElem.Budget.EquipmentComponent.Id = null;
            invElem.Budget.ConstructionComponent.Id = null;
            invElem.Budget.OtherExpensesComponent.Id = null;
            invElem.Budget.WorkCapitalComponent.Id = null;


            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new InvestmentElementRepository(db);

                var oldBudgetReference = invElem.Budget;

                repository.Add(invElem);

                var addedInvElem = db.Query<IInvestmentElement>().Single();
                var addedBudget = db.Query<IBudget>().Single();
                var addedEquipmentComponent = db.Query<BudgetComponentStub>().ElementAt(0);
                var addedConstructionComponent = db.Query<BudgetComponentStub>().ElementAt(1);
                var addedOtherExpensesComponent = db.Query<BudgetComponentStub>().ElementAt(2);
                var addedWorkCapitalComponent = db.Query<BudgetComponentStub>().ElementAt(3);

                Assert.AreNotSame(invElem, addedInvElem);
                Assert.AreNotSame(oldBudgetReference, addedBudget);
                Assert.AreNotSame(oldBudgetReference.EquipmentComponent, addedBudget.EquipmentComponent);
                Assert.AreNotSame(oldBudgetReference.ConstructionComponent, addedBudget.ConstructionComponent);
                Assert.AreNotSame(oldBudgetReference.OtherExpensesComponent, addedBudget.OtherExpensesComponent);
                Assert.AreNotSame(oldBudgetReference.WorkCapitalComponent, addedBudget.WorkCapitalComponent);

                //Assert.AreNotSame(oldBudgetReference.EquipmentComponent.PlannedResources, invElem.Budget.EquipmentComponent.PlannedResources);
                Assert.AreNotSame(oldBudgetReference.EquipmentComponent.PlannedActivities, invElem.Budget.EquipmentComponent.PlannedActivities);
                //Assert.AreNotSame(oldBudgetReference.EquipmentComponent.ExecutedResources, invElem.Budget.EquipmentComponent.ExecutedResources);
                Assert.AreNotSame(oldBudgetReference.EquipmentComponent.ExecutedActivities, invElem.Budget.EquipmentComponent.ExecutedActivities);

                //Assert.AreNotSame(oldBudgetReference.ConstructionComponent.PlannedResources, invElem.Budget.ConstructionComponent.PlannedResources);
                Assert.AreNotSame(oldBudgetReference.ConstructionComponent.PlannedActivities, invElem.Budget.ConstructionComponent.PlannedActivities);
                //Assert.AreNotSame(oldBudgetReference.ConstructionComponent.ExecutedResources, invElem.Budget.ConstructionComponent.ExecutedResources);
                Assert.AreNotSame(oldBudgetReference.ConstructionComponent.ExecutedActivities, invElem.Budget.ConstructionComponent.ExecutedActivities);

                //Assert.AreNotSame(oldBudgetReference.OtherExpensesComponent.PlannedResources, invElem.Budget.OtherExpensesComponent.PlannedResources);
                Assert.AreNotSame(oldBudgetReference.OtherExpensesComponent.PlannedActivities, invElem.Budget.OtherExpensesComponent.PlannedActivities);
                //Assert.AreNotSame(oldBudgetReference.OtherExpensesComponent.ExecutedResources, invElem.Budget.OtherExpensesComponent.ExecutedResources);
                Assert.AreNotSame(oldBudgetReference.OtherExpensesComponent.ExecutedActivities, invElem.Budget.OtherExpensesComponent.ExecutedActivities);

                //Assert.AreNotSame(oldBudgetReference.WorkCapitalComponent.PlannedResources, invElem.Budget.WorkCapitalComponent.PlannedResources);
                Assert.AreNotSame(oldBudgetReference.WorkCapitalComponent.PlannedActivities, invElem.Budget.WorkCapitalComponent.PlannedActivities);
                //Assert.AreNotSame(oldBudgetReference.WorkCapitalComponent.ExecutedResources, invElem.Budget.WorkCapitalComponent.ExecutedResources);
                Assert.AreNotSame(oldBudgetReference.WorkCapitalComponent.ExecutedActivities, invElem.Budget.WorkCapitalComponent.ExecutedActivities);

                db.Commit();
            }
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void Add_GivenInvestmentElementWithoutBudget_Throws()
        {
            TestObject.Add(new InvestmentComponentStub() { Budget = null });
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
                Id = Guid.NewGuid().ToString(),
                Name = "Elem1",
                Description = "desc",
                Budget = CreateBudget()
            };
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }


            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new InvestmentElementRepository(db);

                invElem.Name = "Elem 2";
                invElem.Description = "desc 123";

                repository.Update(invElem);
                db.Commit();
            }


            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                Assert.AreEqual(1, db.Query<IList<IInvestmentElement>>().Count());

                invElem = db.Query<IInvestmentElement>().Single();

                Assert.AreEqual("Elem 2", invElem.Name);
                Assert.AreEqual("desc 123", invElem.Description);
            }
        }

        [TestMethod]
        public void Update_GivenInvestmentElement_SavesCorrectlyTheirReferences()
        {
            #region Arrange

            IInvestmentElement parentInvElem = new InvestmentElementStub
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Parent",
                Budget = CreateBudget()
            };

            IInvestmentComponent invElem = new InvestmentComponentStub()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Elem",
                Description = "desc",
                Parent = parentInvElem,
                Budget = CreateBudget()
            };
            parentInvElem.Elements.Add(invElem);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(parentInvElem);
                db.Store(invElem);
                db.Commit();
            }

            #endregion

            #region Act

            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new InvestmentElementRepository(db) { Parent = parentInvElem };

                /* Intentionally will break the references from the updatee to its parent and viceversa, the
                 * repository must be able to restore them */
                parentInvElem.Elements.Remove(invElem);
                invElem.Parent = null;

                // Modify the attributes of the updatee
                invElem.Name = "Elem 2";
                invElem.Description = "desc 123";

                repository.Update(invElem);
                db.Commit();
            }

            #endregion

            // Assert
            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                Assert.AreEqual(2, db.Query<IList<IInvestmentElement>>().Count());

                Assert.AreEqual(2, db.Query<IBudget>().Count());

                IEnumerable<IInvestmentElement> elements = db.Query<IInvestmentElement>().ToArray();
                Assert.AreEqual(2, elements.Count());

                parentInvElem = elements.Single(x => x.Name == "Parent");
                invElem = (IInvestmentComponent) elements.Single(x => x.Name == "Elem 2");

                Assert.AreEqual("desc 123", invElem.Description);

                Assert.AreSame(parentInvElem, invElem.Parent);
                CollectionAssert.Contains(parentInvElem.Elements.ToArray(), invElem);
            }
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void Update_GivenInvestmentElementWithoutBudget_Throws()
        {
            TestObject.Update(new InvestmentElementStub() { Budget = null });
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

            IInvestmentElement parentInvElem = new InvestmentElementStub
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Parent",
                Budget = CreateBudget()
            };
            parentInvElem.Budget.InvestmentElement = parentInvElem;

            IInvestmentElement parentInvElement = new InvestmentElementStub
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Elem",
                Description = "desc",
                Parent = parentInvElem,
                Budget = CreateBudget()
            };
            parentInvElem.Elements.Add(parentInvElement);

            IInvestmentElement childElem1 = new InvestmentElementStub
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Elem 21",
                Description = "desc 21",
                Parent = parentInvElement,
                Budget = CreateBudget()
            };
            parentInvElement.Elements.Add(childElem1);
            IInvestmentElement childElem2 = new InvestmentElementStub
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Elem 22",
                Description = "desc 22",
                Parent = parentInvElement,
                Budget = CreateBudget()
            };
            parentInvElement.Elements.Add(childElem2);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(parentInvElem);
                db.Store(parentInvElement);
                db.Store(childElem1);
                db.Store(childElem2);
                db.Commit();
            }

            #endregion

            #region Act

            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new InvestmentElementRepository(db) { Parent = parentInvElem };
                repository.Delete(parentInvElement);
                db.Commit();
            }

            #endregion

            #region Assert

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                Assert.AreEqual(1, db.Query<IList<IInvestmentElement>>().Count());
                Assert.AreEqual(1, db.Query<IInvestmentElement>().Count());
                Assert.AreEqual(1, db.Query<IInvestmentElement>().Count());
                Assert.AreEqual(4, db.Query<IBudgetComponent>().Count());
                Assert.AreEqual(4, db.Query<IList<IPlannedResource>>().Count());

                Assert.AreEqual("Parent", db.Query<IInvestmentElement>().Single().Name);

                parentInvElement = db.Query<IInvestmentElement>().Single();
                var budget = db.Query<IBudget>().Single();

                Assert.AreSame(parentInvElement.Budget, budget);
                Assert.AreSame(budget.InvestmentElement, parentInvElement);
                CollectionAssert.Contains(db.Query<IBudgetComponent>().ToArray(), budget.EquipmentComponent);
                CollectionAssert.Contains(db.Query<IBudgetComponent>().ToArray(), budget.ConstructionComponent);
                CollectionAssert.Contains(db.Query<IBudgetComponent>().ToArray(), budget.OtherExpensesComponent);
                CollectionAssert.Contains(db.Query<IBudgetComponent>().ToArray(), budget.WorkCapitalComponent);
            }

            #endregion
        }

        //[TestMethod]
        //public void Delete_GivenInvestmentElement_RemoveTheEquipmentPlannedResourcesFromTheDeleteInvestmentElements()
        //{
        //    // Arrange
        //    var budget = CreateBudget();
        //    var invElem = new InvestmentElementStub { Budget = budget };

        //    using (var db = Db4oEmbedded.OpenFile(_dbPath))
        //    {
        //        db.Store(invElem);
        //        db.Commit();
        //    }

        //    // Arrange
        //    var database = new Mock<IDb4ODatabaseContext>();
        //    database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
        //    database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
        //    var repository = new InvestmentElementRepository(database.Object);
        //    repository.Delete(invElem);

        //    // Assert
        //    _equipmentPlannedResourceRepository.VerifySet(x => x.Component = budget.EquipmentComponent);
        //    _equipmentPlannedResourceRepository.Verify(x => x.DeleteAll());

        //    database.Verify(x => x.Delete((object)budget.EquipmentComponent.PlannedResources));
        //    database.Verify(x => x.Delete<IBudgetComponent>(budget.EquipmentComponent));
        //}

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheEquipmentPlannedActivitiesFromTheDeleteInvestmentElements()
        {
            // Arrange
            var budget = CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepository(database.Object);
            repository.Delete(invElem);

            // Assert
            _equipmentPlannedActivityRepository.VerifySet(x => x.BudgetComponent = budget.EquipmentComponent);
            _equipmentPlannedActivityRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.EquipmentComponent.PlannedActivities));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.EquipmentComponent));
        }

        //[TestMethod]
        //public void Delete_GivenInvestmentElement_RemoveTheEquipmentExecutedResourcesFromTheDeleteInvestmentElements()
        //{
        //    // Arrange
        //    var budget = CreateBudget();
        //    var invElem = new InvestmentElementStub { Budget = budget };

        //    using (var db = Db4oEmbedded.OpenFile(_dbPath))
        //    {
        //        db.Store(invElem);
        //        db.Commit();
        //    }

        //    // Arrange
        //    var database = new Mock<IDb4ODatabaseContext>();
        //    database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
        //    database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
        //    var repository = new InvestmentElementRepository(database.Object);
        //    repository.Delete(invElem);

        //    // Assert
        //    _equipmentExecutedResourceRepository.VerifySet(x => x.Component = budget.EquipmentComponent);
        //    _equipmentExecutedResourceRepository.Verify(x => x.DeleteAll());

        //    database.Verify(x => x.Delete((object)budget.EquipmentComponent.ExecutedResources));
        //    database.Verify(x => x.Delete<IBudgetComponent>(budget.EquipmentComponent));
        //}

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheEquipmentExecutedActivitiesFromTheDeleteInvestmentElements()
        {
            // Arrange
            var budget = CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepository(database.Object);
            repository.Delete(invElem);

            // Assert
            _equipmentExecutedActivityRepository.VerifySet(x => x.BudgetComponent = budget.EquipmentComponent);
            _equipmentExecutedActivityRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.EquipmentComponent.ExecutedActivities));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.EquipmentComponent));
        }

        //[TestMethod]
        //public void Delete_GivenInvestmentElement_RemoveTheConstructionPlannedResourcesFromTheDeleteInvestmentElements()
        //{
        //    // Arrange
        //    var budget = CreateBudget();
        //    var invElem = new InvestmentElementStub { Budget = budget };

        //    using (var db = Db4oEmbedded.OpenFile(_dbPath))
        //    {
        //        db.Store(invElem);
        //        db.Commit();
        //    }

        //    // Arrange
        //    var database = new Mock<IDb4ODatabaseContext>();
        //    database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
        //    database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
        //    var repository = new InvestmentElementRepository(database.Object);
        //    repository.Delete(invElem);

        //    // Assert
        //    _constructionPlannedResourceRepository.VerifySet(x => x.Component = budget.ConstructionComponent);
        //    _constructionPlannedResourceRepository.Verify(x => x.DeleteAll());

        //    database.Verify(x => x.Delete((object)budget.ConstructionComponent.PlannedResources));
        //    database.Verify(x => x.Delete<IBudgetComponent>(budget.ConstructionComponent));
        //}

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheConstructionPlannedActivitiesFromTheDeleteInvestmentElements()
        {
            // Arrange
            var budget = CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepository(database.Object);
            repository.Delete(invElem);

            // Assert
            _constructionPlannedActivityRepository.VerifySet(x => x.BudgetComponent = budget.ConstructionComponent);
            _constructionPlannedActivityRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.ConstructionComponent.PlannedActivities));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.ConstructionComponent));
        }

        //[TestMethod]
        //public void Delete_GivenInvestmentElement_RemoveTheConstructionExecutedResourcesFromTheDeleteInvestmentElements()
        //{
        //    // Arrange
        //    var budget = CreateBudget();
        //    var invElem = new InvestmentElementStub { Budget = budget };

        //    using (var db = Db4oEmbedded.OpenFile(_dbPath))
        //    {
        //        db.Store(invElem);
        //        db.Commit();
        //    }

        //    // Arrange
        //    var database = new Mock<IDb4ODatabaseContext>();
        //    database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
        //    database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
        //    var repository = new InvestmentElementRepository(database.Object);
        //    repository.Delete(invElem);

        //    // Assert
        //    _constructionExecutedResourceRepository.VerifySet(x => x.Component = budget.ConstructionComponent);
        //    _constructionExecutedResourceRepository.Verify(x => x.DeleteAll());

        //    database.Verify(x => x.Delete((object)budget.ConstructionComponent.ExecutedResources));
        //    database.Verify(x => x.Delete<IBudgetComponent>(budget.ConstructionComponent));
        //}

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheConstructionExecutedActivitiesFromTheDeleteInvestmentElements()
        {
            // Arrange
            var budget = CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepository(database.Object);
            repository.Delete(invElem);

            // Assert
            _constructionExecutedActivityRepository.VerifySet(x => x.BudgetComponent = budget.ConstructionComponent);
            _constructionExecutedActivityRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.ConstructionComponent.ExecutedActivities));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.ConstructionComponent));
        }

        //[TestMethod]
        //public void Delete_GivenInvestmentElement_RemoveTheOtherExpensesPlannedResourcesFromTheDeleteInvestmentElements()
        //{
        //    // Arrange
        //    var budget = CreateBudget();
        //    var invElem = new InvestmentElementStub { Budget = budget };

        //    using (var db = Db4oEmbedded.OpenFile(_dbPath))
        //    {
        //        db.Store(invElem);
        //        db.Commit();
        //    }

        //    // Arrange
        //    var database = new Mock<IDb4ODatabaseContext>();
        //    database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
        //    database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
        //    var repository = new InvestmentElementRepository(database.Object);
        //    repository.Delete(invElem);

        //    // Assert
        //    _otherExpensesPlannedResourceRepository.VerifySet(x => x.Component = budget.OtherExpensesComponent);
        //    _otherExpensesPlannedResourceRepository.Verify(x => x.DeleteAll());

        //    database.Verify(x => x.Delete((object)budget.OtherExpensesComponent.PlannedResources));
        //    database.Verify(x => x.Delete((object)budget.OtherExpensesComponent.PlannedActivities));
        //    database.Verify(x => x.Delete<IBudgetComponent>(budget.OtherExpensesComponent));
        //}

        //[TestMethod]
        //public void Delete_GivenInvestmentElement_RemoveTheOtherExpensesExecutedResourcesFromTheDeleteInvestmentElements()
        //{
        //    // Arrange
        //    var budget = CreateBudget();
        //    var invElem = new InvestmentElementStub { Budget = budget };

        //    using (var db = Db4oEmbedded.OpenFile(_dbPath))
        //    {
        //        db.Store(invElem);
        //        db.Commit();
        //    }

        //    // Arrange
        //    var database = new Mock<IDb4ODatabaseContext>();
        //    database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
        //    database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
        //    var repository = new InvestmentElementRepository(database.Object);
        //    repository.Delete(invElem);

        //    // Assert
        //    _otherExpensesExecutedResourceRepository.VerifySet(x => x.Component = budget.OtherExpensesComponent);
        //    _otherExpensesExecutedResourceRepository.Verify(x => x.DeleteAll());

        //    database.Verify(x => x.Delete((object)budget.OtherExpensesComponent.ExecutedResources));
        //    database.Verify(x => x.Delete((object)budget.OtherExpensesComponent.ExecutedActivities));
        //    database.Verify(x => x.Delete<IBudgetComponent>(budget.OtherExpensesComponent));
        //}

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheOtherExpensesPlannedActivitiesFromTheDeleteInvestmentElements()
        {
            // Arrange
            var budget = CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepository(database.Object);
            repository.Delete(invElem);

            // Assert
            _otherExpensesPlannedActivityRepository.VerifySet(x => x.BudgetComponent = budget.OtherExpensesComponent);
            _otherExpensesPlannedActivityRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.OtherExpensesComponent.PlannedActivities));
            database.Verify(x => x.Delete((object)budget.OtherExpensesComponent.PlannedActivities));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.OtherExpensesComponent));
        }

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheOtherExpensesExecutedActivitiesFromTheDeleteInvestmentElements()
        {
            // Arrange
            var budget = CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepository(database.Object);
            repository.Delete(invElem);

            // Assert
            _otherExpensesExecutedActivityRepository.VerifySet(x => x.BudgetComponent = budget.OtherExpensesComponent);
            _otherExpensesExecutedActivityRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.OtherExpensesComponent.ExecutedActivities));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.OtherExpensesComponent));
        }

        //[TestMethod]
        //public void Delete_GivenInvestmentElement_RemoveTheWorkCapitalPlannedResourcesFromTheDeleteInvestmentElements()
        //{
        //    // Arrange
        //    var budget = CreateBudget();
        //    var invElem = new InvestmentElementStub { Budget = budget };

        //    using (var db = Db4oEmbedded.OpenFile(_dbPath))
        //    {
        //        db.Store(invElem);
        //        db.Commit();
        //    }

        //    // Arrange
        //    var database = new Mock<IDb4ODatabaseContext>();
        //    database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
        //    database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
        //    var repository = new InvestmentElementRepository(database.Object);
        //    repository.Delete(invElem);

        //    // Assert
        //    _workCapitalPlannedResourceRepository.VerifySet(x => x.Component = budget.WorkCapitalComponent);
        //    _workCapitalPlannedResourceRepository.Verify(x => x.DeleteAll());

        //    database.Verify(x => x.Delete((object)budget.WorkCapitalComponent.PlannedResources));
        //    database.Verify(x => x.Delete((object)budget.WorkCapitalComponent.PlannedActivities));
        //    database.Verify(x => x.Delete<IBudgetComponent>(budget.WorkCapitalComponent));
        //}

        //[TestMethod]
        //public void Delete_GivenInvestmentElement_RemoveTheWorkCapitalExecutedResourcesFromTheDeleteInvestmentElements()
        //{
        //    // Arrange
        //    var budget = CreateBudget();
        //    var invElem = new InvestmentElementStub { Budget = budget };

        //    using (var db = Db4oEmbedded.OpenFile(_dbPath))
        //    {
        //        db.Store(invElem);
        //        db.Commit();
        //    }

        //    // Arrange
        //    var database = new Mock<IDb4ODatabaseContext>();
        //    database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
        //    database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
        //    var repository = new InvestmentElementRepository(database.Object);
        //    repository.Delete(invElem);

        //    // Assert
        //    _workCapitalExecutedResourceRepository.VerifySet(x => x.Component = budget.WorkCapitalComponent);
        //    _workCapitalExecutedResourceRepository.Verify(x => x.DeleteAll());

        //    database.Verify(x => x.Delete((object)budget.WorkCapitalComponent.ExecutedResources));
        //    database.Verify(x => x.Delete((object)budget.WorkCapitalComponent.ExecutedActivities));
        //    database.Verify(x => x.Delete<IBudgetComponent>(budget.WorkCapitalComponent));
        //}

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheWorkCapitalPlannedActivitiesFromTheDeleteInvestmentElements()
        {
            // Arrange
            var budget = CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepository(database.Object);
            repository.Delete(invElem);

            // Assert
            _workCapitalPlannedActivityRepository.VerifySet(x => x.BudgetComponent = budget.WorkCapitalComponent);
            _workCapitalPlannedActivityRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.WorkCapitalComponent.PlannedActivities));
            database.Verify(x => x.Delete((object)budget.WorkCapitalComponent.PlannedActivities));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.WorkCapitalComponent));
        }

        [TestMethod]
        public void Delete_GivenInvestmentElement_RemoveTheWorkCapitalExecutedActivitiesFromTheDeleteInvestmentElements()
        {
            // Arrange
            var budget = CreateBudget();
            var invElem = new InvestmentElementStub { Budget = budget };

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem);
                db.Commit();
            }

            // Arrange
            var database = new Mock<IDb4ODatabaseContext>();
            database.Setup(x => x.Find<IInvestmentElement>(invElem.Id)).Returns(invElem);
            database.Setup(x => x.Find<IBudget>(invElem.Budget.Id)).Returns(invElem.Budget);
            var repository = new InvestmentElementRepository(database.Object);
            repository.Delete(invElem);

            // Assert
            _workCapitalExecutedActivityRepository.VerifySet(x => x.BudgetComponent = budget.WorkCapitalComponent);
            _workCapitalExecutedActivityRepository.Verify(x => x.DeleteAll());

            database.Verify(x => x.Delete((object)budget.WorkCapitalComponent.ExecutedActivities));
            database.Verify(x => x.Delete<IBudgetComponent>(budget.WorkCapitalComponent));
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Relate_GivenNullFirstInvestmentElementButNotTheSecond_Throws()
        {
            TestObject.Relate(null, Mock.Of<IInvestmentElement>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Relate_GivenNullSecondInvestmentElementButNotTheFirst_Throws()
        {
            TestObject.Relate(Mock.Of<IInvestmentElement>(), (IInvestmentElement)null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Relate_GivenNullFirstInvestmentElement_Throws()
        {
            TestObject.Relate(null, Mock.Of<IBudget>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Relate_GivenNullSecondInvestmentElement_Throws()
        {
            TestObject.Relate(Mock.Of<IInvestmentElement>(), (IBudget)null);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Unrelate_GivenNullFirstInvestmentElementButNotTheSecond_Throws()
        {
            TestObject.Unrelate(null, Mock.Of<IInvestmentElement>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Unrelate_GivenNullSecondInvestmentElementButNotTheFirst_Throws()
        {
            TestObject.Unrelate(Mock.Of<IInvestmentElement>(), (IInvestmentElement)null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Unrelate_GivenNullFirstInvestmentElement_Throws()
        {
            TestObject.Unrelate(null, Mock.Of<IBudget>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Unrelate_GivenNullSecondInvestmentElement_Throws()
        {
            TestObject.Unrelate(Mock.Of<IInvestmentElement>(), (IBudget)null);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void SaveReferences_GivenNullInvestmentElement_Throws()
        {
            TestObject.SaveReference((IInvestmentElement)null);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Clone_GivenNullInvestmentElement_Throws()
        {
            new InvestmentElementReppositoryStub(Mock.Of<IDb4ODatabaseContext>()).Clone(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void Clone_GivenNullBudgetInProvidedInvestmentElement_Throws()
        {
            new InvestmentElementReppositoryStub(Mock.Of<IDb4ODatabaseContext>()).Clone(Mock.Of<IInvestmentElement>());
        }

        [TestMethod]
        public void Clone_GivenInvestmentElement_ClonesIt()
        {
            // Arrange
            IBudget originalBudget = CreateBudget();
            var investmentElement = Mock.Of<IInvestmentElement>(x => x.Budget == originalBudget);

            // Act
            var clone = (IInvestmentElement)TestObjectInternals.Invoke("Clone", investmentElement);

            var budget = clone.Budget;
            var equipmentComponent = budget.EquipmentComponent;
            var constructionComponent = budget.ConstructionComponent;
            var otherExpensesComponent = budget.OtherExpensesComponent;
            var workCapitalComponent = budget.WorkCapitalComponent;

            Assert.AreNotSame(investmentElement, clone);
            Assert.AreNotSame(originalBudget, budget);
            Assert.AreNotSame(originalBudget.EquipmentComponent, budget.EquipmentComponent);
            Assert.AreNotSame(originalBudget.ConstructionComponent, budget.ConstructionComponent);
            Assert.AreNotSame(originalBudget.OtherExpensesComponent, budget.OtherExpensesComponent);
            Assert.AreNotSame(originalBudget.WorkCapitalComponent, budget.WorkCapitalComponent);

            //Assert.AreNotSame(originalBudget.EquipmentComponent.PlannedResources, budget.EquipmentComponent.PlannedResources);
            Assert.AreNotSame(originalBudget.EquipmentComponent.PlannedActivities, budget.EquipmentComponent.PlannedActivities);
            //Assert.AreNotSame(originalBudget.EquipmentComponent.ExecutedResources, budget.EquipmentComponent.ExecutedResources);
            Assert.AreNotSame(originalBudget.EquipmentComponent.ExecutedActivities, budget.EquipmentComponent.ExecutedActivities);

            //Assert.AreNotSame(originalBudget.ConstructionComponent.PlannedResources, budget.ConstructionComponent.PlannedResources);
            Assert.AreNotSame(originalBudget.ConstructionComponent.PlannedActivities, budget.ConstructionComponent.PlannedActivities);
            //Assert.AreNotSame(originalBudget.ConstructionComponent.ExecutedResources, budget.ConstructionComponent.ExecutedResources);
            Assert.AreNotSame(originalBudget.ConstructionComponent.ExecutedActivities, budget.ConstructionComponent.ExecutedActivities);

            //Assert.AreNotSame(originalBudget.OtherExpensesComponent.PlannedResources, budget.OtherExpensesComponent.PlannedResources);
            Assert.AreNotSame(originalBudget.OtherExpensesComponent.PlannedActivities, budget.OtherExpensesComponent.PlannedActivities);
            //Assert.AreNotSame(originalBudget.OtherExpensesComponent.ExecutedResources, budget.OtherExpensesComponent.ExecutedResources);
            Assert.AreNotSame(originalBudget.OtherExpensesComponent.ExecutedActivities, budget.OtherExpensesComponent.ExecutedActivities);

            //Assert.AreNotSame(originalBudget.WorkCapitalComponent.PlannedResources, budget.WorkCapitalComponent.PlannedResources);
            Assert.AreNotSame(originalBudget.WorkCapitalComponent.PlannedActivities, budget.WorkCapitalComponent.PlannedActivities);
            //Assert.AreNotSame(originalBudget.WorkCapitalComponent.ExecutedResources, budget.WorkCapitalComponent.ExecutedResources);
            Assert.AreNotSame(originalBudget.WorkCapitalComponent.ExecutedActivities, budget.WorkCapitalComponent.ExecutedActivities);
        }


        private IBudget CreateBudget()
        {
            return new BudgetStub
            {
                Id = Guid.NewGuid().ToString(),
                EquipmentComponent = new BudgetComponentStub
                {
                    Id = Guid.NewGuid().ToString(),
                    PlannedResources = new List<IPlannedResource>(),
                    PlannedActivities = new List<IPlannedActivity>()
                },
                ConstructionComponent = new BudgetComponentStub
                {
                    Id = Guid.NewGuid().ToString(),
                    PlannedResources = new List<IPlannedResource>(),
                    PlannedActivities = new List<IPlannedActivity>()
                },
                OtherExpensesComponent = new BudgetComponentStub
                {
                    Id = Guid.NewGuid().ToString(),
                    PlannedResources = new List<IPlannedResource>(),
                    PlannedActivities = new List<IPlannedActivity>()
                },
                WorkCapitalComponent = new BudgetComponentStub
                {
                    Id = Guid.NewGuid().ToString(),
                    PlannedResources = new List<IPlannedResource>(),
                    PlannedActivities = new List<IPlannedActivity>()
                },
            };
        }


        private class InvestmentElementReppositoryStub : InvestmentElementRepository
        {
            public InvestmentElementReppositoryStub(IDb4ODatabaseContext databaseContext)
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
