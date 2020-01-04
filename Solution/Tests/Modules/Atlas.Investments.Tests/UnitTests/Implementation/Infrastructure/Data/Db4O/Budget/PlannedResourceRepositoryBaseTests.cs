using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Infrastructure.Data.Db4O.Budget
{
    [TestClass, ExcludeFromCodeCoverage]
    public class PlannedResourceRepositoryBaseTests
    {
        [TestMethod]
        public void GetItemCollection_ReturnsMethodAllowingToObtainCorrectPlannedResourceListOfCurrentBudgetComponent()
        {
            // Arrange
            var expectedList = Mock.Of<IList<IPlannedResource>>();
            var budgetComponent = Mock.Of<IBudgetComponent>(x => x.PlannedResources == expectedList);
            var db = Mock.Of<IDb4ODatabaseContext>();
            var repository = new RepositoryStub(db);
            var internals = new PrivateObject(repository);

            // Act
            var method = (Func<IBudgetComponent, IList<IPlannedResource>>)internals.GetProperty("GetItemCollection");
            IList<IPlannedResource> actualList = method(budgetComponent);

            // Assert
            Assert.AreSame(expectedList, actualList);
        }


        public class RepositoryStub : PlannedResourceRepositoryBase<IBudgetComponent>
        {
            public RepositoryStub(IDb4ODatabaseContext databaseContext)
                : base(databaseContext)
            {
            }


            public new Func<IBudgetComponent, IList<IPlannedResource>> GetItemCollection
            {
                get { return base.GetItemCollection; }
            }
        }
    }
}
