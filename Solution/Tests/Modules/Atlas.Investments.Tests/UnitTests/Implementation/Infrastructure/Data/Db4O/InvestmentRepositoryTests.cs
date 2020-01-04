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
    public class InvestmentRepositoryTests : TestBase<InvestmentRepository>
    {
        private readonly string _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdb.db");


        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IInvestment>()).Returns(() => new InvestmentStub());
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
            TestObject = new InvestmentRepository(Mock.Of<IDb4ODatabaseContext>());
        }


        [TestMethod]
        public void RelevanProperties_ReturnsCorrectProperties()
        {
            // Act
            var actualProperties = (string[])TestObjectInternals.GetProperty("RelevantProperties");

            // Assert
            string[] expectedProperties =
            {
                ExtractPropertyName<IInvestment, string>(x => x.Name),
                ExtractPropertyName<IInvestment, string>(x => x.Description),
                ExtractPropertyName<IInvestment, object>(x => x.Id),
                ExtractPropertyName<IInvestment, object>(x => x.Code),
                ExtractPropertyName<IInvestment, object>(x => x.AuthorOrEmitter),
                ExtractPropertyName<IInvestment, object>(x => x.Entity),
                ExtractPropertyName<IInvestment, object>(x => x.RelatedPrograms),
                ExtractPropertyName<IInvestment, object>(x => x.Location),
                ExtractPropertyName<IInvestment, object>(x => x.Constructor),
                ExtractPropertyName<IInvestment, object>(x => x.Objective),
                ExtractPropertyName<IInvestment, object>(x => x.Scope),
                ExtractPropertyName<IInvestment, object>(x => x.Capacity),
                ExtractPropertyName<IInvestment, object>(x => x.InducedDoings),
            };
            CollectionAssert.AreEquivalent(expectedProperties, actualProperties);
        }


        [TestMethod]
        public void Entities_ReturnsAllInvestments()
        {
            // Arrange
            var investment = new InvestmentStub
            {
                Id = Guid.NewGuid(),
                Budget = EntityTestsHelpers.CreateBudget()
            };

            var investmentComponent1 = new InvestmentComponentStub
            {
                Id = Guid.NewGuid(),
                Budget = EntityTestsHelpers.CreateBudget(),
                Parent = investment
            };
            investment.Elements.Add(investmentComponent1);

            var investmentComponent2 = new InvestmentComponentStub
            {
                Id = Guid.NewGuid(),
                Budget = EntityTestsHelpers.CreateBudget(),
                Parent = investment
            };
            investment.Elements.Add(investmentComponent2);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(investment);
                db.Store(investmentComponent1);
                db.Store(investmentComponent2);

                db.Commit();
            }

            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new InvestmentRepository(db);

                // Act
                IEnumerable<IInvestmentElement> entities = repository.Entities.ToArray();

                // Assert
                Assert.IsTrue(entities.All(x => x is IInvestment));
                Assert.AreEqual(1, entities.Count());
            }
        }

        [TestMethod]
        public void Entities_ReturnsArrayOfInvestments()
        {
            // Arrange
            var investment = new InvestmentStub
            {
                Id = Guid.NewGuid(),
                Budget = EntityTestsHelpers.CreateBudget()
            };

            var investmentComponent1 = new InvestmentComponentStub
            {
                Id = Guid.NewGuid(),
                Budget = EntityTestsHelpers.CreateBudget(),
                Parent = investment
            };
            investment.Elements.Add(investmentComponent1);

            var investmentComponent2 = new InvestmentComponentStub
            {
                Id = Guid.NewGuid(),
                Budget = EntityTestsHelpers.CreateBudget(),
                Parent = investment
            };
            investment.Elements.Add(investmentComponent2);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(investment);
                db.Store(investmentComponent1);
                db.Store(investmentComponent2);

                db.Commit();
            }

            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new InvestmentRepository(db);

                // Act
                IEnumerable<IInvestmentElement> entities = repository.Entities;

                // Assert
                Assert.IsInstanceOfType(entities, typeof(Array));
            }
        }

        [TestMethod]
        public void Entities_ReturnsClonesOfActualDatabaseInvestments()
        {
            // Arrange
            var investment = new InvestmentStub
            {
                Id = Guid.NewGuid(),
                Budget = EntityTestsHelpers.CreateBudget()
            };
            var investment2 = new InvestmentStub
            {
                Id = Guid.NewGuid(),
                Budget = EntityTestsHelpers.CreateBudget()
            };

            var investmentComponent1 = new InvestmentComponentStub
            {
                Id = Guid.NewGuid(),
                Budget = EntityTestsHelpers.CreateBudget(),
                Parent = investment
            };
            investment.Elements.Add(investmentComponent1);

            var investmentComponent2 = new InvestmentComponentStub
            {
                Id = Guid.NewGuid(),
                Budget = EntityTestsHelpers.CreateBudget(),
                Parent = investment
            };
            investment.Elements.Add(investmentComponent2);

            using (var db = Db4oEmbedded.OpenFile(_dbPath))
            {
                db.Store(investment);
                db.Store(investment2);
                db.Store(investmentComponent1);
                db.Store(investmentComponent2);

                db.Commit();
            }

            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new InvestmentRepository(db);

                // Act
                IEnumerable<IInvestment> entities = repository.Entities.ToArray();

                // Assert
                IInvestment dbInvestment = db.Query<IInvestment>().Single(x => Equals(x.Id, investment.Id));
                IInvestment dbInvestment2 = db.Query<IInvestment>().Single(x => Equals(x.Id, investment2.Id));
                Assert.AreNotSame(dbInvestment, entities.Single(x => Equals(x.Id, investment.Id)));
                Assert.AreNotSame(dbInvestment2, entities.Single(x => Equals(x.Id, investment2.Id)));
            }
        }


        [TestMethod]
        public void Delete_GivenAnInvestmentElementWithChilds_RemovesItAndAllItsChilds()
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
            var investmentComponentRepository = new Mock<IInvestmentComponentRepository>();
            ServiceLocatorMock.Setup(x => x.GetInstance<IInvestmentComponentRepository>()).Returns(investmentComponentRepository.Object);
            using (var db = new Db4ODatabaseContext(_dbPath))
            {
                var repository = new InvestmentRepository(db);

                repository.Delete(investment);

                db.Commit();
            }

            // Assert
            using (IEmbeddedObjectContainer db = Db4oEmbedded.OpenFile(_dbPath))
            {
                Assert.IsFalse(db.Query<IInvestment>().Any());
                Assert.AreEqual(2, db.Query<IList<IInvestmentComponent>>().Count());
                Assert.AreEqual(2, db.Query<IBudget>().Count());

                investmentComponentRepository.VerifySet(x => x.InvestmentElement = investment);
                investmentComponentRepository.Verify(x => x.DeleteAll());
            }
        }
    }
}