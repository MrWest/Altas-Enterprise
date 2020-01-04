using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Implementation.Application.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.WorkCapital;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Application
{
    //[TestClass, ExcludeFromCodeCoverage]
    //public class WorkCapitalExecutedResourceManagerApplicationServicesTests :
    //    ItemManagerApplicationServicesTestsBase<WorkCapitalExecutedResourceManagerApplicationServices, IExecutedResource, IWorkCapitalExecutedResourceRepository, IWorkCapitalExecutedResourceDomainServices, IUnitOfWork>
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
    //        var plannedResourceRepository = Mock.Of<IWorkCapitalPlannedResourceRepository>();
    //        ServiceLocatorMock.Setup(x => x.GetInstance<IWorkCapitalPlannedResourceRepository>()).Returns(plannedResourceRepository);

    //        // Act
    //        var actualRepository = (IWorkCapitalPlannedResourceRepository)TestObjectInternals.GetProperty("PlannedItemRepository");

    //        // Assert
    //        Assert.AreSame(plannedResourceRepository, actualRepository);
    //    }
    //}
}