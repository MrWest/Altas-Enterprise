using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.Construction;
using CompanyName.Atlas.Investments.Implementation.Application.Budget.Construction;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Construction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Application.Budget.Construction
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ConstructionExecutedActivityManagerApplicationServicesTests :
        ItemManagerApplicationServicesTestsBase<ConstructionExecutedActivityManagerApplicationServices, IExecutedActivity, IConstructionExecutedActivityRepository, IConstructionExecutedActivityDomainServices, IUnitOfWork>
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
            var plannedActivityRepository = Mock.Of<IConstructionPlannedActivityRepository>();
            ServiceLocatorMock.Setup(x => x.GetInstance<IConstructionPlannedActivityRepository>()).Returns(plannedActivityRepository);

            // Act
            var actualRepository = (IConstructionPlannedActivityRepository)TestObjectInternals.GetProperty("PlannedItemRepository");

            // Assert
            Assert.AreSame(plannedActivityRepository, actualRepository);
        }
    }
}