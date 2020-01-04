using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Implementation.Application.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.WorkCapital;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Application.Budget.WorkCapital
{
    [TestClass, ExcludeFromCodeCoverage]
    public class WorkCapitalExecutedActivityManagerApplicationServicesTests :
        ItemManagerApplicationServicesTestsBase<WorkCapitalExecutedActivityManagerApplicationServices, IExecutedActivity, IWorkCapitalExecutedActivityRepository, IWorkCapitalExecutedActivityDomainServices, IUnitOfWork>
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
            var plannedActivityRepository = Mock.Of<IWorkCapitalPlannedActivityRepository>();
            ServiceLocatorMock.Setup(x => x.GetInstance<IWorkCapitalPlannedActivityRepository>()).Returns(plannedActivityRepository);

            // Act
            var actualRepository = (IWorkCapitalPlannedActivityRepository)TestObjectInternals.GetProperty("PlannedItemRepository");

            // Assert
            Assert.AreSame(plannedActivityRepository, actualRepository);
        }
    }
}