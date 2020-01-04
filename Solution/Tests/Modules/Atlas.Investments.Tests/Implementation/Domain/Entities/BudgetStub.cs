using System;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Domain.Entities
{
    public class BudgetStub : IBudget
    {
        public IEquipmentComponent EquipmentComponent { get; set; }

        public IConstructionComponent ConstructionComponent { get; set; }

        public IOtherExpensesComponent OtherExpensesComponent { get; set; }

        public IWorkCapitalComponent WorkCapitalComponent { get; set; }

        public IInvestmentElement InvestmentElement { get; set; }

        public string Id { get; set; }

        public string FullName { get; set; }
        public DateTime StartDate()
        {
            throw new NotImplementedException();
        }

        public DateTime FinishDate()
        {
            throw new NotImplementedException();
        }

        public bool StartCalculated { get; set; }
        public bool EndCalculated { get; set; }
        public DateTime LastCalculatedFinishDate { get; set; }
        public DateTime LastCalculatedStartDate { get; set; }
        public string EquipmentComponentId { get; set; }
        public string ConstructionComponentId { get; set; }
        public string OtherExpensesComponentId { get; set; }
        public string WorkCapitalComponentId { get; set; }
    }
}
