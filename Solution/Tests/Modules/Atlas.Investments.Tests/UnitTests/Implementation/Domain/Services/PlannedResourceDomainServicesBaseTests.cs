using System;
using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Services
{
    [TestClass, ExcludeFromCodeCoverage]
    public class PlannedResourceDomainServicesBaseTests : DomainServicesTestsBase<IPlannedResource, PlannedResourceDomainServicesBase<IBudgetComponent>>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        [TestMethod]
        public void Create_ReturnsPlannedResourceWithCorrectData()
        {
            // Act
            IPlannedResource plannedResource = TestObject.Create();

            // Assert
            Assert.AreEqual(Resources.NewPlannedResourceName, plannedResource.Name);
            Assert.AreEqual(Resources.NewPlannedResourceDescription, plannedResource.Description);
            Assert.AreNotEqual(default(Guid).ToString(), plannedResource.Code.ToString());
        }
    }
}
