using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.Construction;
using CompanyName.Atlas.Investments.Implementation.Application.Budget.Construction;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Construction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Application
{
    //[TestClass, ExcludeFromCodeCoverage]
    //public class ConstructionExecutedResourceManagerApplicationServicesTests :
    //    ItemManagerApplicationServicesTestsBase<ConstructionExecutedResourceManagerApplicationServices, IExecutedResource, IConstructionExecutedResourceRepository, IConstructionExecutedResourceDomainServices, IUnitOfWork>
    //{
    //    [TestInitialize]
    //    public override void Initialize()
    //    {
    //        base.Initialize();
    //    }


    //    [TestMethod]
    //    public void PlannedItemRepository_ReturnsCorrectRepositoryInstance()
    //    {
    //        // Arrange
    //        var plannedResourceRepository = Mock.Of<IConstructionPlannedResourceRepository>();
    //        ServiceLocatorMock.Setup(x => x.GetInstance<IConstructionPlannedResourceRepository>())
    //            .Returns(plannedResourceRepository);

    //        // Act
    //        var actualRepository =
    //            (IConstructionPlannedResourceRepository) TestObjectInternals.GetProperty("PlannedItemRepository");

    //        // Assert
    //        Assert.AreSame(plannedResourceRepository, actualRepository);
    //    }
    //}
}