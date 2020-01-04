using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Infrastructure.Data.Db4O.Budget
{
    [TestClass, ExcludeFromCodeCoverage]
    public class PlannedActivityRepositoryBaseTests
    {
        [TestMethod]
        public void GetItemCollection_ReturnsMethodAllowingToObtainCorrectPlannedActivityListOfCurrentBudgetComponent()
        {
            // Arrange
            var expectedList = Mock.Of<IList<IPlannedActivity>>();
            var budgetComponent = Mock.Of<IBudgetComponent>(x => Equals(x.PlannedActivities , expectedList));
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new RepositoryStub(db);
            var internals = new PrivateObject(repository);

            // Act
            var method = (Func<IBudgetComponent, IList<IPlannedActivity>>)internals.GetProperty("GetItemCollection");
            IList<IPlannedActivity> actualList = method(budgetComponent);

            // Assert
            Assert.AreSame(expectedList, actualList);
        }


        public class RepositoryStub : PlannedActivityRepositoryBase
        {
            public RepositoryStub(IDb4ODatabaseContext databaseContext)
                : base(databaseContext)
            {
            }


            //public new Func<IBudgetComponent, IList<IPlannedActivity>> GetItemCollection
            //{
            //    get { return base.GetItemCollection; }
            //}
        }
    }
}
