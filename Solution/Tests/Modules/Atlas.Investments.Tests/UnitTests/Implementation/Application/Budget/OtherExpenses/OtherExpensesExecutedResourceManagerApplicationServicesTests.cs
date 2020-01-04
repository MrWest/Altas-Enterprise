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
    public class OtherExpensesExecutedResourceManagerApplicationServicesTests :
        ItemManagerApplicationServicesTestsBase<OtherExpensesExecutedResourceManagerApplicationServices, IExecutedResource, IOtherExpensesExecutedResourceRepository, IOtherExpensesExecutedResourceDomainServices, IUnitOfWork>
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
            var plannedResourceRepository = Mock.Of<IOtherExpensesPlannedResourceRepository>();
            ServiceLocatorMock.Setup(x => x.GetInstance<IOtherExpensesPlannedResourceRepository>()).Returns(plannedResourceRepository);

            // Act
            var actualRepository = (IOtherExpensesPlannedResourceRepository)TestObjectInternals.GetProperty("PlannedItemRepository");

            // Assert
            Assert.AreSame(plannedResourceRepository, actualRepository);
        }
    }
}