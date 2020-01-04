using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Entities
{
    public class PlannedResourceStub : IPlannedResource
    {
        public IBudgetComponent Component { get; set; }

        public IExecutedBudgetComponentItem Execution { get; set; }

        public decimal Quantity { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public object Id { get; set; }

        public string FullName { get; set; }
    }
}
