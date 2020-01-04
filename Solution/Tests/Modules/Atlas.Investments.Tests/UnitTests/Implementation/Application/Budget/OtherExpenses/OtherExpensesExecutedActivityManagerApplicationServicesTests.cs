using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Implementation.Application.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.OtherExpenses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Application.Budget.OtherExpenses
{
    [TestClass, ExcludeFromCodeCoverage]
    public class OtherExpensesExecutedActivityManagerApplicationServicesTests :
        ItemManagerApplicationServicesTestsBase<OtherExpensesExecutedActivityManagerApplicationServices, IExecutedActivity, IOtherExpensesExecutedActivityRepository, IOtherExpensesExecutedActivityDomainServices, IUnitOfWork>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        [TestMethod]
        public void PlannedItemRepository_ReturnsCorrectRepositoryInstance()
        {
            // Arrange
            var plannedActivityRepository = Mock.Of<IOtherExpensesPlannedActivityRepository>();
            ServiceLocatorMock.Setup(x => x.GetInstance<IOtherExpensesPlannedActivityRepository>()).Returns(plannedActivityRepository);

            // Act
            var actualRepository = (IOtherExpensesPlannedActivityRepository)TestObjectInternals.GetProperty("PlannedItemRepository");

            // Assert
            Assert.AreSame(plannedActivityRepository, actualRepository);
        }
    }
}