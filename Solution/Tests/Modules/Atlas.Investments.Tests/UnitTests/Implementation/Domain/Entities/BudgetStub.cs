using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Entities
{
    public class BudgetStub : IBudget
    {
        public IEquipmentComponent EquipmentComponent { get; set; }

        public IConstructionComponent ConstructionComponent { get; set; }

        public IOtherExpensesComponent OtherExpensesComponent { get; set; }

        public IWorkCapitalComponent WorkCapitalComponent { get; set; }

        public IInvestmentElement InvestmentElement { get; set; }

        public object Id { get; set; }

        public string FullName { get; set; }
    }
}
