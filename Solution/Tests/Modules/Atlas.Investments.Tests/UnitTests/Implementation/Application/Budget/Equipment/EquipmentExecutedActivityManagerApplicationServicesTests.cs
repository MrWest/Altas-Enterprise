using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.Equipment;
using CompanyName.Atlas.Investments.Implementation.Application.Budget.Equipment;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Equipment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Application.Budget.Equipment
{
    [TestClass, ExcludeFromCodeCoverage]
    public class EquipmentExecutedActivityManagerApplicationServicesTests :
        ItemManagerApplicationServicesTestsBase<EquipmentExecutedActivityManagerApplicationServices, IExecutedActivity, IEquipmentExecutedActivityRepository, IEquipmentExecutedActivityDomainServices, IUnitOfWork>
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
            var plannedActivityRepository = Mock.Of<IEquipmentPlannedActivityRepository>();
            ServiceLocatorMock.Setup(x => x.GetInstance<IEquipmentPlannedActivityRepository>())
                .Returns(plannedActivityRepository);

            // Act
            var actualRepository =
                (IEquipmentPlannedActivityRepository) TestObjectInternals.GetProperty("PlannedItemRepository");

            // Assert
            Assert.AreSame(plannedActivityRepository, actualRepository);
        }
    }
}