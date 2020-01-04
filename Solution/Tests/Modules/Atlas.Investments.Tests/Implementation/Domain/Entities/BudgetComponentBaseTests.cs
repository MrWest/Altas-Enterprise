using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Domain.Entities
{
    [TestClass, ExcludeFromCodeCoverage]
    public class BudgetComponentBaseTests : MockedTestBase<BudgetComponentBase>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        //[TestMethod]
        //public void Constructor_InitializesPlannedResourcesList()
        //{
        //    Assert.IsNotNull(TestObject.PlannedResources);
        //}

        [TestMethod]
        public void Constructor_InitializesPlannedActivitiesList()
        {
            Assert.IsNotNull(TestObject.PlannedActivities);
        }

        //[TestMethod]
        //public void Constructor_InitializesExecutedResourcesList()
        //{
        //    Assert.IsNotNull(TestObject.ExecutedResources);
        //}

        [TestMethod]
        public void Constructor_InitializesExecutedActivitiesList()
        {
            Assert.IsNotNull(TestObject.ExecutedActivities);
        }
    }
}
