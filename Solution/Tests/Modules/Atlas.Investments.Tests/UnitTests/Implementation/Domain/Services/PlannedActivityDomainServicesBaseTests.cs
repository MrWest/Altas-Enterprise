using System;
using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Services
{
    [TestClass, ExcludeFromCodeCoverage]
    public class PlannedActivityDomainServicesBaseTests : DomainServicesTestsBase<IPlannedActivity, PlannedActivityDomainServicesBase<IBudgetComponent>>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        [TestMethod]
        public void Create_ReturnsPlannedActivityWithCorrectData()
        {
            // Act
            IPlannedActivity plannedActivity = TestObject.Create();

            // Assert
            Assert.AreEqual(Resources.NewPlannedActivityName, plannedActivity.Name);
            Assert.AreEqual(Resources.NewPlannedActivityDescription, plannedActivity.Description);
            Assert.AreNotEqual(default(Guid).ToString(), plannedActivity.Code.ToString());
        }
    }
}
