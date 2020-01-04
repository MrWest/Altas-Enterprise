using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Application.Budget.Equipment;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Implementation.Application;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System;
using System.Reflection;
using System.Linq;
using CompanyName.Atlas.Investments.Application;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Application
{
    //[TestClass, ExcludeFromCodeCoverage]
    //public class InvestmentElementManagerApplicationServicesTests :
    //    ItemManagerApplicationServicesTestsBase<InvestmentElementManagerApplicationServices, IInvestmentElement, IInvestmentElementRepository, IInvestmentElementDomainServices, IUnitOfWork>
    //{
    //    private Mock<IEquipmentPlannedResourceManagerApplicationServices> _equipmentPlannedResources;


    //    [TestInitialize]
    //    public override void Initialize()
    //    {
    //        base.Initialize();

    //        DomainServicesMock.SetupAllProperties();
    //        RepositoryMock.SetupAllProperties();

    //        _equipmentPlannedResources = new Mock<IEquipmentPlannedResourceManagerApplicationServices>();
    //        _equipmentPlannedResources.SetupProperty(x => x.Component);
    //        ServiceLocatorMock
    //            .Setup(x => x.GetInstance<IEquipmentPlannedResourceManagerApplicationServices>())
    //            .Returns(_equipmentPlannedResources.Object);

    //        ServiceLocatorMock
    //            .Setup(x => x.GetInstance<IInvestmentElementManagerApplicationServices>())
    //            .Returns(TestObject);
    //    }


    //    private IInvestmentElementRepository GetRepository()
    //    {
    //        Type type = TestObject.GetType();
    //        PropertyInfo property = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance).First(x => x.Name == "Repository");
    //        return (IInvestmentElementRepository)property.GetValue(TestObject);
    //    }

    //    private IInvestmentElementDomainServices GetDomainServices()
    //    {
    //        Type type = TestObject.GetType();
    //        PropertyInfo property = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance).First(x => x.Name == "DomainServices");
    //        return (IInvestmentElementDomainServices)property.GetValue(TestObject);
    //    }


    //    [TestMethod]
    //    public void Repository_IfParentIsNull_ReturnsNewRepositoryWithNullParent()
    //    {
    //        // Act
    //        IInvestmentElementRepository repository = GetRepository();

    //        // Assert
    //        Assert.IsNull(repository.Parent);
    //    }

    //    [TestMethod]
    //    public void Repository_IfParentIsNotNull_ReturnsNewRepositoryWithSameParentAsApplicationServices()
    //    {
    //        // Arrange
    //        var parent = Mock.Of<IInvestmentElement>();
    //        TestObject.Parent = parent;

    //        // Act
    //        IInvestmentElementRepository repository = GetRepository();

    //        // Assert
    //        Assert.AreSame(parent, repository.Parent);
    //    }


    //    [TestMethod]
    //    public void DomainServices_IfParentIsNull_ReturnsNewDomainServicesWithNullParent()
    //    {
    //        // Act
    //        IInvestmentElementDomainServices domainServices = GetDomainServices();

    //        // Assert
    //        Assert.IsNull(domainServices.Parent);
    //    }

    //    [TestMethod]
    //    public void DomainServices_IfParentIsNotNull_ReturnsNewDomainServicesWithSameParentAsApplicationServices()
    //    {
    //        // Arrange
    //        var parent = Mock.Of<IInvestmentElement>();
    //        TestObject.Parent = parent;

    //        // Act
    //        IInvestmentElementDomainServices domainServices = GetDomainServices();

    //        // Assert
    //        Assert.AreSame(parent, domainServices.Parent);
    //    }


    //    [TestMethod]
    //    public void GetKeyFor_IfParentIsDefined_ReturnsKeyContainingParentId()
    //    {
    //        // Arrange
    //        var parent = Mock.Of<IInvestmentElement>(x => x.Id == 1.ToString());
    //        TestObject.Parent = parent;

    //        // Act
    //        MethodInfo addMethod = TestObject.GetType().GetMethod("Add");
    //        string key = TestObject.GetKeyFor(addMethod, Mock.Of<IInvestmentElement>(x => x.Id == 2.ToString()));

    //        // Assert
    //        Assert.IsTrue(key.StartsWith("{0}->".EasyFormat(parent.Id)));
    //    }


    //    [TestMethod, ExpectedException(typeof(ArgumentNullException))]
    //    public void Delete_GivenNullInvestmentElement_Throws()
    //    {
    //        TestObject.Delete(null);
    //    }

    //    [TestMethod]
    //    public void Delete_GivenInvestmentElement_DeletesIt()
    //    {
    //        // Arrange
    //        var equipment = Mock.Of<IEquipmentComponent>();
    //        var investmentElement = Mock.Of<IInvestmentElement>(x => x.Budget.EquipmentComponent == equipment);

    //        // Act
    //        TestObject.Delete(investmentElement);

    //        // Assert
    //        RepositoryMock.Verify(x => x.Delete(investmentElement));
    //    }
    //}
}
