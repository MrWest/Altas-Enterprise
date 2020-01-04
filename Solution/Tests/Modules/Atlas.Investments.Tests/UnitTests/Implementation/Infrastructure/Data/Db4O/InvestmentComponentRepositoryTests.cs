using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Infrastructure.Data;
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
    public class InvestmentComponentRepositoryTests : TestBase<InvestmentComponentRepository>
    {
        private readonly string _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdb.db");
        private readonly Mock<IDb4ODatabaseContext> _dbContext = new Mock<IDb4ODatabaseContext>();


        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IInvestment>()).Returns(() => new InvestmentStub());
            ServiceLocatorMock.Setup(x => x.GetInstance<IInvestmentComponent>()).Returns(() => new InvestmentComponentStub());
            ServiceLocatorMock.Setup(x => x.GetInstance<IBudget>()).Returns(() => new BudgetStub());
            ServiceLocatorMock.Setup(x => x.GetInstance<IEquipmentComponent>()).Returns(() => new BudgetComponentStub());
            ServiceLocatorMock.Setup(x => x.GetInstance<IConstructionComponent>()).Returns(() => new BudgetComponentStub());
            ServiceLocatorMock.Setup(x => x.GetInstance<IOtherExpensesComponent>()).Returns(() => new BudgetComponentStub());
            ServiceLocatorMock.Setup(x => x.GetInstance<IWorkCapitalComponent>()).Returns(() => new BudgetComponentStub());

            ServiceLocatorMock.Setup(x => x.GetInstance<IEquipmentPlannedResourceRepository>()).Returns(Mock.Of<IEquipmentPlannedResourceRepository>);
            ServiceLocatorMock.Setup(x => x.GetInstance<IEquipmentExecutedResourceRepository>()).Returns(Mock.Of<IEquipmentExecutedResourceRepository>);
            ServiceLocatorMock.Setup(x => x.GetInstance<IEquipmentPlannedActivityRepository>()).Returns(Mock.Of<IEquipmentPlannedActivityRepository>);
            ServiceLocatorMock.Setup(x => x.GetInstance<IEquipmentExecutedActivityRepository>()).Returns(Mock.Of<IEquipmentExecutedActivityRepository>);

            ServiceLocatorMock.Setup(x => x.GetInstance<IConstructionPlannedResourceRepository>()).Returns(Mock.Of<IConstructionPlannedResourceRepository>);
            ServiceLocatorMock.Setup(x => x.GetInstance<IConstructionExecutedResourceRepository>()).Returns(Mock.Of<IConstructionExecutedResourceRepository>);
            ServiceLocatorMock.Setup(x => x.GetInstance<IConstructionPlannedActivityRepository>()).Returns(Mock.Of<IConstructionPlannedActivityRepository>);
            ServiceLocatorMock.Setup(x => x.GetInstance<IConstructionExecutedActivityRepository>()).Returns(Mock.Of<IConstructionExecutedActivityRepository>);

            ServiceLocatorMock.Setup(x => x.GetInstance<IOtherExpensesPlannedResourceRepository>()).Returns(Mock.Of<IOtherExpensesPlannedResourceRepository>);
            ServiceLocatorMock.Setup(x => x.GetInstance<IOtherExpensesExecutedResourceRepository>()).Returns(Mock.Of<IOtherExpensesExecutedResourceRepository>);
            ServiceLocatorMock.Setup(x => x.GetInstance<IOtherExpensesPlannedActivityRepository>()).Returns(Mock.Of<IOtherExpensesPlannedActivityRepository>);
            ServiceLocatorMock.Setup(x => x.GetInstance<IOtherExpensesExecutedActivityRepository>()).Returns(Mock.Of<IOtherExpensesExecutedActivityRepository>);

            ServiceLocatorMock.Setup(x => x.GetInstance<IWorkCapitalPlannedResourceRepository>()).Returns(Mock.Of<IWorkCapitalPlannedResourceRepository>);
            ServiceLocatorMock.Setup(x => x.GetInstance<IWorkCapitalExecutedResourceRepository>()).Returns(Mock.Of<IWorkCapitalExecutedResourceRepository>);
            ServiceLocatorMock.Setup(x => x.GetInstance<IWorkCapitalPlannedActivityRepository>()).Returns(Mock.Of<IWorkCapitalPlannedActivityRepository>);
            ServiceLocatorMock.Setup(x => x.GetInstance<IWorkCapitalExecutedActivityRepository>()).Returns(Mock.Of<IWorkCapitalExecutedActivityRepository>);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_dbPath))
                File.Delete(_dbPath);
        }

        protected override void CreateInstance()
        {
            TestObject = new InvestmentComponentRepository(_dbContext.Object);
        }


        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void Parent_IfReadBeforeAssigned_Throws()
        {
            Console.WriteLine(TestObject.InvestmentElement);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Parent_IfAssignedANull_Throws()
        {
            TestObject.InvestmentElement = null;
        }

        [TestMethod]
        public void Parent_IfMeaningfullValue_SetsIt()
        {
            // Arrange
            var investmentElement = new Mock<IInvestmentElement>();

            // Act
            TestObject.InvestmentElement = investmentElement.Object;

            // Assert
            Assert.AreSame(investmentElement.Object, TestObject.InvestmentElement);
        }


        [TestMethod]
        public void Entities_ReturnsAllTheInvestmentComponentsOfThatParentOne()
        {
            #region Arrange

            var invElem1 = new InvestmentStub
            {
                Id = Guid.NewGuid(),
                Name = "Elem1",
                Description = "Elem desc 1",
                Budget = EntityTestsHelpers.CreateBudget()
            };
            var invElem2 = new InvestmentComponentStub
            {
                Id = Guid.NewGuid(),
                Parent = invElem1,
                Name = "Elem2",
                Description = "Elem desc 2",
                Budget = EntityTestsHelpers.CreateBudget()
            };
            invElem1.Elements.Add(invElem2);
            var invElem3 = new InvestmentComponentStub
            {
                Id = Guid.NewGuid(),
                Parent = invElem1,
                Name = "Elem3",
                Description = "Elem desc 3",
                Budget = EntityTestsHelpers.CreateBudget()
            };
            invElem1.Elements.Add(invElem3);
            var invElem4 = new InvestmentComponentStub
            {
                Id = Guid.NewGuid(),
                Parent = invElem2,
                Name = "Elem4",
                Description = "Elem desc 4",
                Budget = EntityTestsHelpers.CreateBudget()
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
                var repository = new InvestmentComponentRepository(db) { InvestmentElement = invElem1 };
                actualInvestmentElements = repository.Entities.ToArray();
            }

            #endregion

            // Assert
            Assert.AreEqual(2, actualInvestmentElements.Count());
            Assert.IsTrue(actualInvestmentElements.Any(x => x.Name == "Elem2"));
            Assert.IsTrue(actualInvestmentElements.Any(x => x.Name == "Elem3"));
        }

        [TestMethod]
        public void Entities_ReturnsAlwaysClones()
        {
            #region Arrange

            var invElem1 = new InvestmentStub
            {
                Id = Guid.NewGuid(),
                Name = "Elem1",
                Description = "Elem desc 1",
                Budget = EntityTestsHelpers.CreateBudget()
            };
            var invElem2 = new InvestmentComponentStub
            {
                Id = Guid.NewGuid(),
                Parent = invElem1,
                Name = "Elem2",
                Description = "Elem desc 2",
                Budget = EntityTestsHelpers.CreateBudget()
            };
            invElem1.Elements.Add(invElem2);
            var invElem3 = new InvestmentComponentStub
            {
                Id = Guid.NewGuid(),
                Parent = invElem1,
                Name = "Elem3",
                Description = "Elem desc 3",
                Budget = EntityTestsHelpers.CreateBudget()
            };
            invElem1.Elements.Add(invElem3);
            var invElem4 = new InvestmentComponentStub
            {
                Id = Guid.NewGuid(),
                Parent = invElem2,
                Name = "Elem4",
                Description = "Elem desc 4",
                Budget = EntityTestsHelpers.CreateBudget()
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
                var repository = new InvestmentComponentRepository(db) { InvestmentElement = invElem1 };
                IEnumerable<IInvestmentElement> actualInvestmentElements = repository.Entities;

                // Assert
                IInvestmentElement[] dbInvestmentElements = db.Query<IInvestmentElement>().ToArray();
                foreach (IInvestmentElement invElem in actualInvestmentElements.ToArray())
                    foreach (IInvestmentElement dbInvElem in dbInvestmentElements)
                    {
                        Assert.AreNotSame(invElem, dbInvElem);

                        Assert.AreNotSame(invElem.Budget, dbInvElem.Budget);

                        Assert.AreNotSame(invElem.Budget.EquipmentComponent, dbInvElem.Budget.EquipmentComponent);
                        Assert.AreNotSame(invElem.Budget.EquipmentComponent.PlannedResources, dbInvElem.Budget.EquipmentComponent.PlannedResources);
                        Assert.AreNotSame(invElem.Budget.EquipmentComponent.PlannedActivities, dbInvElem.Budget.EquipmentComponent.PlannedActivities);
                        Assert.AreNotSame(invElem.Budget.EquipmentComponent.ExecutedResources, dbInvElem.Budget.EquipmentComponent.ExecutedResources);
                        Assert.AreNotSame(invElem.Budget.EquipmentComponent.ExecutedActivities, dbInvElem.Budget.EquipmentComponent.ExecutedActivities);

                        Assert.AreNotSame(invElem.Budget.ConstructionComponent, dbInvElem.Budget.ConstructionComponent);
                        Assert.AreNotSame(invElem.Budget.ConstructionComponent.PlannedResources, dbInvElem.Budget.ConstructionComponent.PlannedResources);
                        Assert.AreNotSame(invElem.Budget.ConstructionComponent.PlannedActivities, dbInvElem.Budget.ConstructionComponent.PlannedActivities);
                        Assert.AreNotSame(invElem.Budget.ConstructionComponent.ExecutedResources, dbInvElem.Budget.ConstructionComponent.ExecutedResources);
                        Assert.AreNotSame(invElem.Budget.ConstructionComponent.ExecutedActivities, dbInvElem.Budget.ConstructionComponent.ExecutedActivities);

                        Assert.AreNotSame(invElem.Budget.OtherExpensesComponent, dbInvElem.Budget.OtherExpensesComponent);
                        Assert.AreNotSame(invElem.Budget.OtherExpensesComponent.PlannedResources, dbInvElem.Budget.OtherExpensesComponent.PlannedResources);
                        Assert.AreNotSame(invElem.Budget.OtherExpensesComponent.PlannedActivities, dbInvElem.Budget.OtherExpensesComponent.PlannedActivities);
                        Assert.AreNotSame(invElem.Budget.OtherExpensesComponent.ExecutedResources, dbInvElem.Budget.OtherExpensesComponent.ExecutedResources);
                        Assert.AreNotSame(invElem.Budget.OtherExpensesComponent.ExecutedActivities, dbInvElem.Budget.OtherExpensesComponent.ExecutedActivities);

                        Assert.AreNotSame(invElem.Budget.WorkCapitalComponent, dbInvElem.Budget.WorkCapitalComponent);
                        Assert.AreNotSame(invElem.Budget.WorkCapitalComponent.PlannedResources, dbInvElem.Budget.WorkCapitalComponent.PlannedResources);
                        Assert.AreNotSame(invElem.Budget.WorkCapitalComponent.PlannedActivities, dbInvElem.Budget.WorkCapitalComponent.PlannedActivities);
                        Assert.AreNotSame(invElem.Budget.WorkCapitalComponent.ExecutedResources, dbInvElem.Budget.WorkCapitalComponent.ExecutedResources);
                        Assert.AreNotSame(invElem.Budget.WorkCapitalComponent.ExecutedActivities, dbInvElem.Budget.WorkCapitalComponent.ExecutedActivities);
                    }
            }
        }

        [TestMethod]
        public void Entities_IfInvestmentComponentsInTheDatabase_AlwaysReturnsArray()
        {
            // Arrange
            var investment = new Mock<IInvestment>();
            investment.Object.Budget = EntityTestsHelpers.CreateBudget();
            var investmentComponent = new Mock<IInvestmentComponent>();
            investmentComponent.Setup(x => x.Parent).Returns(investment.Object);

            var databaseContext = new Mock<IDb4ODatabaseContext>();
            databaseContext
                .Setup(x => x.Query(It.IsAny<Predicate<IInvestmentElement>>()))
                .Returns(new IInvestmentElement[] { investment.Object, investmentComponent.Object });
            TestObject = new InvestmentComponentRepository(databaseContext.Object) { InvestmentElement = investment.Object };

            // Act
            IEnumerable<IInvestmentElement> actualInvestmentElements = TestObject.Entities;

            // Assert
            Assert.IsInstanceOfType(actualInvestmentElements, typeof(Array));
        }

        [TestMethod]
        public void Entities_IfNoInvestmentElementInTheDatabase_ReturnsEmptyArray()
        {
            // Arrange
            TestObject.InvestmentElement = Mock.Of<IInvestmentElement>();

            // Act
            IEnumerable<IInvestmentElement> actualInvestmentElements = TestObject.Entities;

            // Assert
            Assert.IsFalse(actualInvestmentElements.Any());
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Add_GivenNullInvestmentComponent_Throws()
        {
            TestObject.Add(null);
        }

        [TestMethod]
        public void Add_GivenInvestmentComponent_AddTheGivenInvestmentComponentAsAChildOfParentInvestmentElement()
        {
            // Arrange
            var invElem1 = new InvestmentElementStub
            {
                Id = Guid.NewGuid(),
                Name = "Elem1",
                Budget = EntityTestsHelpers.CreateBudget()
            };

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(invElem1);
                db.Commit();
            }

            // Act
            IInvestmentComponent investmentComponent;
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new InvestmentComponentRepository(db) { InvestmentElement = invElem1 };

                investmentComponent = new InvestmentComponentStub
                {
                    Id = Guid.NewGuid(),
                    Name = "Elem2",
                    Budget = EntityTestsHelpers.CreateBudget()
                };

                repository.Add(investmentComponent);
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

                CollectionAssert.Contains(invElem1.Elements.ToArray(), investmentComponent);
                Assert.AreSame(invElem1, investmentComponent.Parent);
            }
        }


        [TestMethod]
        public void Delete_GivenAnInvestmentComponentWithChilds_RemovesItAndAllItsChilds()
        {
            // Arrange
            IInvestment investment = new InvestmentStub
            {
                Id = Guid.NewGuid(),
                Budget = EntityTestsHelpers.CreateBudget()
            };

            IInvestmentComponent component1 = new InvestmentComponentStub
            {
                Id = Guid.NewGuid(),
                Budget = EntityTestsHelpers.CreateBudget(),
                Parent = investment
            };
            investment.Elements.Add(component1);

            IInvestmentComponent component2 = new InvestmentComponentStub
            {
                Id = Guid.NewGuid(),
                Budget = EntityTestsHelpers.CreateBudget(),
                Parent = component1
            };
            component1.Elements.Add(component2);

            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(investment);
                db.Store(component1);
                db.Store(component2);

                db.Commit();
            }

            // Act
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                ServiceLocatorMock
                    .Setup(x => x.GetInstance<IInvestmentComponentRepository>())
                    .Returns(() => new InvestmentComponentRepository(db));

                var repository = new InvestmentComponentRepository(db) { InvestmentElement = investment };

                repository.Delete(component1);

                db.Commit();
            }

            // Assert
            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                Assert.AreEqual(1, db.Query<IInvestment>().Count());
                Assert.AreEqual(1, db.Query<IList<IInvestmentComponent>>().Count());
                Assert.AreEqual(1, db.Query<IBudget>().Count());
            }
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Relate_GivenNullInvestmentComponent_Throws()
        {
            TestObject.Relate(null, Mock.Of<IInvestmentElement>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Relate_GivenNullInvestment_Throws()
        {
            TestObject.Relate(Mock.Of<IInvestmentComponent>(), null);
        }

        [TestMethod]
        public void Relate_GivenBothArguments_RelatesThem()
        {
            // Arrange
            var investment = new Mock<IInvestment>();
            investment.SetupGet(x => x.Elements).Returns(new List<IInvestmentComponent>());
            var investmentComponent = new Mock<IInvestmentComponent>();
            investmentComponent.SetupGet(x => x.Id).Returns(1);

            // Act
            TestObject.Relate(investmentComponent.Object, investment.Object);

            // Assert
            investmentComponent.VerifySet(x => x.Parent = investment.Object);
            CollectionAssert.Contains(investment.Object.Elements.ToArray(), investmentComponent.Object);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Unrelate_GivenNullInvestmentComponent_Throws()
        {
            TestObject.Unrelate(null, Mock.Of<IInvestmentElement>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Unrelate_GivenNullInvestment_Throws()
        {
            TestObject.Unrelate(Mock.Of<IInvestmentComponent>(), null);
        }

        [TestMethod]
        public void Unrelate_GivenBothArguments_UnrelatesThem()
        {
            // Arrange
            var investment = new Mock<IInvestment>();
            investment.SetupGet(x => x.Elements).Returns(new List<IInvestmentComponent>());

            var investmentComponent = new Mock<IInvestmentComponent>();
            investmentComponent.SetupGet(x => x.Id).Returns(1);
            investmentComponent.Object.Parent = investment.Object;
            investment.Object.Elements.Add(investmentComponent.Object);

            // Act
            TestObject.Unrelate(investmentComponent.Object, investment.Object);

            // Assert
            investmentComponent.VerifySet(x => x.Parent = null);
            CollectionAssert.DoesNotContain(investment.Object.Elements.ToArray(), investmentComponent.Object);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void SaveReferences_GivenNullInvestmentElement_Throws()
        {
            TestObject.SaveReference(null);
        }

        [TestMethod]
        public void SaveReferences_GivenInvestmentElement_SavesItAndItsElementsCollection()
        {
            // Arrange
            var investmentElement = new Mock<IInvestmentElement>();
            investmentElement.SetupGet(x => x.Elements).Returns(new List<IInvestmentComponent>());

            // Act
            TestObject.SaveReference(investmentElement.Object);

            // Assert
            _dbContext.Verify(x => x.Store(investmentElement.Object));
            _dbContext.Verify(x => x.Store(investmentElement.Object.Elements));
        }
    }
}