using System;
using System.Collections.Generic;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Entities
{
    /// <summary>
    ///     This class provides helpers for test classes using Budget domain entities in their test methods.
    /// </summary>
    public static class EntityTestsHelpers
    {
        /// <summary>
        ///     Creates a new budget with all its components up to the component items, those are not created.
        /// </summary>
        /// <returns></returns>
        public static IBudget CreateBudget()
        {
            return new BudgetStub
            {
                Id = Guid.NewGuid(),
                EquipmentComponent = new BudgetComponentStub
                {
                    Id = Guid.NewGuid(),
                    PlannedResources = new List<IPlannedResource>(),
                    PlannedActivities = new List<IPlannedActivity>()
                },
                ConstructionComponent = new BudgetComponentStub
                {
                    Id = Guid.NewGuid(),
                    PlannedResources = new List<IPlannedResource>(),
                    PlannedActivities = new List<IPlannedActivity>()
                },
                OtherExpensesComponent = new BudgetComponentStub
                {
                    Id = Guid.NewGuid(),
                    PlannedResources = new List<IPlannedResource>(),
                    PlannedActivities = new List<IPlannedActivity>()
                },
                WorkCapitalComponent = new BudgetComponentStub
                {
                    Id = Guid.NewGuid(),
                    PlannedResources = new List<IPlannedResource>(),
                    PlannedActivities = new List<IPlannedActivity>()
                },
            };
        }
    }
}